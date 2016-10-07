using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using NUnit.Core;
using NUnit.Framework;
using NUnit.Util;
using System.IO;
namespace SayedHashimi.Util.MSBuild
{
    /// <summary>
    /// This is a class that can be used by MSBuild to invoke NUnit tests on
    /// assemblies passed into it.
    /// 
    /// Inputs:
    ///  Assemblies
    ///  CacheDirPath
    ///  LogFile
    /// 
    /// Outputs:
    ///  NumFailedTests
    ///  NUnitLogFile
    ///  NumExecutedTests
    /// 
    /// Author: Sayed Ibrahim Hashimi (sayed.hashimi@gmail.com)
    /// This task has not been throughly tested and is provided with no warranty.
    /// 
    /// Portions of the class were based on code that can be found in the NUnit framework at nunit.org.
    /// NUnit License stuff:
    /// Portions Copyright © 2002-2004 James W. Newkirk, Michael C. Two, Alexei A. Vorontsov, Charlie Poole or 
    /// Copyright © 2000-2004 Philip A. Craig
    /// </summary>
    public class NUnitTask : Task
    {
        #region Private members
        private ITaskItem[] _assemblies;
        private string _cacheDirPath;
        private string _previousCacheDir;
        private string _logFile;

        private int _numTestsExecuted;
        private int _numTestsIgnored;
        private int _numTestsFailed;

        private bool _continueAfterError;

        /// <summary>
        /// StringBuilder that NUnit will write to.
        /// If you are building assemblies with many test cases you may want to consider
        /// an EventCollector that writes directly to a file or to a database depending on your
        /// scenario.
        /// </summary>
        private StringBuilder _builder;
        /// <summary>
        /// This will be passed to NUnit to process testing events
        /// </summary>
        private EventListener _eventListener;
        #endregion

        public NUnitTask()
        {
            this._numTestsExecuted = 0;
            this._numTestsIgnored = 0;
            this._numTestsFailed = 0;
            this._previousCacheDir = null;
            this._builder = new StringBuilder();
        }

        #region MSBuild input properties
        /// <summary>
        /// These assemblies contain the NUnit test cases that will be executed
        /// </summary>
        [Required]
        public ITaskItem[] Assemblies
        {
            get
            {
                return this._assemblies;
            }
            set
            {
                this._assemblies = value;
            }
        }
        /// <summary>
        /// Location for NUnit to store its temp files
        /// </summary>
        [Required]
        public string CacheDirPath
        {
            get
            {
                return this._cacheDirPath;
            }
            set
            {
                if (value == null || value.Length <= 0)
                {
                    throw new ApplicationException("CacheDirPath must not be empty");
                }
                this._cacheDirPath = value;
                if (this._previousCacheDir == null)
                {
                    this._previousCacheDir = value;
                }
            }
        }
        /// <summary>
        /// Location where the log file should be stored
        /// </summary>
        [Required]
        public string LogFile
        {
            get
            {
                return this._logFile;
            }
            set
            {
                if (value == null || value.Length <= 0)
                {
                    throw new ApplicationException("LogFile must not be empty");
                }
                this._logFile = value;
            }
        }
        [Required]
        public bool ContinueAfterError
        {
            get
            {
                return this._continueAfterError;
            }
            set
            {
                this._continueAfterError = value;
            }
        }
        
        #endregion

        #region MSBuild output properties
        /// <summary>
        /// Reports the number of tests executed
        /// </summary>
        [Output]
        public int NumExecutedTests
        {
            get
            {
                return this._numTestsExecuted;
            }
        }
        [Output]
        public int NumIgnoredTests
        {
            get
            {
                return this._numTestsIgnored;
            }
        }
        [Output]
        public int NumFailedTests
        {
            get
            {
                return this._numTestsFailed;
            }
        }
        #endregion

        private SBEventCollector SBEventCollector
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        /// <summary>
        /// This will be called by MSBuild to invoke the NUnit tests.
        /// </summary>
        /// <returns>true if all test successful, false if otherwise</returns>
        public override bool Execute()
        {
            Log.LogMessage("NUnitTask starting");

            if (this.Assemblies == null || this.Assemblies.Length <= 0)
            {
                Log.LogMessageFromText("No Assemblies to test", MessageImportance.High);
                return true;
            }

            PreNUnitExecute();

            TestDomain td = new TestDomain();
            bool failed = false;
            //execute tests contained in all assemblies
            foreach (ITaskItem pathItem in Assemblies)
            {
                string path = pathItem.GetMetadata("FullPath");
                if (path == null || path.Length <= 0)
                {
                    //throw new ApplicationException("Assembly path is null");
                    Log.LogMessageFromText("Found empty path entry in Assemblies, skipping", MessageImportance.High);
                    continue;
                }

                TestResult result = ExecuteTestsIn(path,td);
                if (result.IsFailure)
                {
                    failed = true;
                    System.Collections.Specialized.StringCollection failMessages = ((SBEventCollector)_eventListener).FailMessages;
                    if (failMessages.Count <= 0)
                    {
                        break;
                    }

                    StringBuilder sb = new StringBuilder();
                    foreach(string msg in failMessages )
                    {
                        sb.AppendLine(msg);
                    }

                    Log.LogError(sb.ToString(), null);

                    if ( !ContinueAfterError )
                    {
                        break;
                    }
                }
            }

            PostNUnitExecute();

            return !failed;
        }

        /// <summary>
        /// Will execute all tests in the assembly located at <code>assemblyPath</code>
        /// </summary>
        /// <param name="assemblyPath"></param>
        /// <param name="td"></param>
        /// <returns></returns>
        private TestResult ExecuteTestsIn(string assemblyPath,TestDomain td)
        {
            NUnitProject project = NUnitProject.LoadProject(assemblyPath);
            Test test = td.Load(project);
            TestResult result = td.Run(_eventListener);
            td.Unload();

            return result;
        }
        /// <summary>
        /// This will execute before the actual NUnit test is executed
        /// </summary>
        private void PreNUnitExecute()
        {
            //Set this so the NUnit knows where to store its temp files
            System.Configuration.ConfigurationManager.AppSettings["shadowfiles.path"] = CacheDirPath;
            Log.LogMessageFromText("Set NUnit cache dir to: " + CacheDirPath, MessageImportance.Low);

            this._builder = new StringBuilder();
            this._eventListener = new SBEventCollector(_builder, this.Log);
        }
        /// <summary>
        /// This will execute just after the actual NUnit test has executed
        /// </summary>
        private void PostNUnitExecute()
        {
            //Restore the previous cache dir if it exists
            if (this._previousCacheDir != null)
            {
                System.Configuration.ConfigurationManager.AppSettings["shadowfiles.path"] = this._previousCacheDir;
            }

            this._numTestsExecuted = ((SBEventCollector)_eventListener).AllTestRunCount;
            this._numTestsIgnored = ((SBEventCollector)_eventListener).AllTestIgnoreCount;
            this._numTestsFailed = ((SBEventCollector)_eventListener).AllTestFailureCount;

            Log.LogMessageFromText("Num tests executed= " + _numTestsExecuted, MessageImportance.Low);
            Log.LogMessageFromText("Num tests ignored= " + _numTestsIgnored, MessageImportance.Low);
            Log.LogMessageFromText("Num tests failed= " + _numTestsFailed, MessageImportance.High);

            //write the contents of the builder out to a file
            string logFilePath = this.LogFile;
            if (File.Exists(logFilePath))
            {
                Log.LogMessageFromText("Deleting existing NUnit log file", MessageImportance.Low);
                File.Delete(logFilePath);
            }

            File.WriteAllText(logFilePath, _builder.ToString());
            Log.LogMessageFromText("Finished writing log file",MessageImportance.Low);
        }
    }
}