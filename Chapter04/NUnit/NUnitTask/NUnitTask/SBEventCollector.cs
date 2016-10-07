using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Core;
using NUnit.Util;
using System.Text.RegularExpressions;
using Microsoft.Build.Utilities;
namespace SayedHashimi.Util.MSBuild
{
    /// <summary>
    /// This is a StringBuilder EventCollector for NUnit.
    /// This class is a modified version of the EventCollector that can be found in the NUnit source.
    /// 
    /// NUnit License stuff:
    /// Portions Copyright © 2002-2004 James W. Newkirk, Michael C. Two, Alexei A. Vorontsov, Charlie Poole or 
    /// Copyright © 2000-2004 Philip A. Craig
    /// </summary>
    class SBEventCollector : LongLivingMarshalByRefObject, EventListener
    {
        #region Private members
        private StringBuilder _builder;
        private int _allTestRunCount;
        private int _allTestIgnoreCount;
        private int _allFailureCount;

        private int _testRunCount;
        private int _testIgnoreCount;
        private int _failureCount;

        private string _currentTestName;
        private System.Collections.Specialized.StringCollection messages;
        private System.Collections.Specialized.StringCollection failMessages;

        private TaskLoggingHelper _buildLogger;
        private bool _sentFailureMessage;
        #endregion

        public SBEventCollector(StringBuilder Builder, TaskLoggingHelper BuildLogger)
        {
            this.Builder = Builder;
            this.BuildLogger = BuildLogger;
            this.messages = new System.Collections.Specialized.StringCollection();
            this.failMessages = new System.Collections.Specialized.StringCollection();
            this._allFailureCount = 0;
            this._allTestIgnoreCount = 0;
            this._allTestRunCount = 0;
            this._sentFailureMessage = false;
        }

        #region Public properties
        public int AllTestRunCount
        {
            get
            {
                return this._allTestRunCount;
            }
        }
        public int AllTestIgnoreCount
        {
            get
            {
                return this._allTestIgnoreCount;
            }
        }
        public int AllTestFailureCount
        {
            get
            {
                return this._allFailureCount;
            }
        }
        public TaskLoggingHelper BuildLogger
        {
            get
            {
                return this._buildLogger;
            }
            set
            {
                this._buildLogger = value;
            }
        }
        #endregion

        /// <summary>
        /// The StringBuilder that will be appended to
        /// </summary>
        public StringBuilder Builder
        {
            get
            {
                return this._builder;
            }
            set
            {
                this._builder = value;
            }
        }
        /// <summary>
        /// Will return the messages related to failures.
        /// If no failures then this is empty.
        /// </summary>
        public System.Collections.Specialized.StringCollection FailMessages
        {
            get
            {
                return this.failMessages;
            }
        }

        private void Append(string text)
        {
            _builder.Append(text);
        }
        private void AppendLine(string text)
        {
            _builder.AppendLine(text);
        }
        private void AppendLineMessage(string text)
        {
            AppendLine(text);
            BuildLogger.LogMessage(text);
        }
        private void AppendLineWarning(string text)
        {
            AppendLine(text);
            BuildLogger.LogWarning(text);
        }
        private void AppendLineError(string text)
        {
            AppendLine(text);
            BuildLogger.LogError(text);
        }
        


        #region EventListener Members

        public void RunFinished(Exception exception)
        {
        }

        public void RunFinished(TestResult[] results)
        {
        }

        public void RunStarted(Test[] tests)
        {
        }

        public void SuiteFinished(TestSuiteResult result)
        {
            //_allTestRunCount += _testRunCount;
            _allTestIgnoreCount += _testIgnoreCount;
            _allFailureCount += _failureCount;
            _testRunCount = 0;
            _testIgnoreCount = 0;
            _failureCount = 0;
            AppendLine("############################################################################");
            if (messages.Count == 0)
            {
                AppendLine("##############                 S U C C E S S               #################");
            }
            else
            {
                if (!_sentFailureMessage)
                {
                    //Only send error message once
                    AppendLineError("##############                F A I L U R E S              #################");
                    _sentFailureMessage = true;
                }
                foreach (string s in messages)
                {
                    AppendLine(s);
                }
                AppendLine("############################################################################");
                AppendLine("Executed tests : " + _testRunCount);
                AppendLine("Ignored tests  : " + _testIgnoreCount);
                AppendLine("Failed tests   : " + _failureCount);
                AppendLine("Total time     : " + result.Time + " seconds");
                AppendLine("############################################################################");
            }
        }

        public void SuiteStarted(TestSuite suite)
        {
            messages = new System.Collections.Specialized.StringCollection();

            _testRunCount = 0;
            _testIgnoreCount = 0;
            _failureCount = 0;

            AppendLine("################################ UNIT TESTS ################################");
            AppendLineMessage("Running tests in '" + suite.FullName + "'...");

        }

        public void TestFinished(TestCaseResult result)
        {
            if (result.Executed)
            {
                _testRunCount++;
                if (result.IsFailure)
                {
                    _failureCount++;
                    //Append("F");
                    messages.Add(string.Format("{0}) {1} :", _failureCount, result.Test.FullName));
                    messages.Add(result.Message.Trim(Environment.NewLine.ToCharArray()));

                    failMessages.Add(string.Format("{0}) {1} :", _failureCount, result.Test.FullName));
                    messages.Add(result.Message.Trim(Environment.NewLine.ToCharArray()));

                    string stackTrace = StackTraceFilter.Filter(result.StackTrace);
                    string[] trace = stackTrace.Split(System.Environment.NewLine.ToCharArray());
                    foreach (string s in trace)
                    {
                        if (s != string.Empty)
                        {
                            string link = Regex.Replace(s.Trim(), @".* in (.*):line (.*)", "$1($2)");
                            messages.Add(string.Format("at\n{0}", link));
                            failMessages.Add(string.Format("at\n{0}", link));
                        }
                    }
                }
                else
                {
                }
            }
        }

        public void TestStarted(TestCase testCase)
        {
            this._currentTestName = testCase.FullName;
            AppendLineMessage("Executing test: " + testCase.FullName);
            this._allTestRunCount++;
        }

        public void UnhandledException(Exception exception)
        {
            string msg = string.Format("##### Unhandled Exception while running {0}", this._currentTestName);

            AppendLineError(msg);
        }

        #endregion
    }
}
