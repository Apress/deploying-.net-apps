using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.Collections;

namespace FileLoggers
{
    /// <summary>
    /// This class will be used as an MSBuild logger that will output an XML log file.
    /// 
    /// Author: Sayed Ibrahim Hashimi (sayed.hashimi@gmail.com)
    /// This class has not been throughly tested and is offered with no warranty.
    /// copyright Sayed Ibrahim Hashimi 2005
    /// </summary>
    public class XmlLogger : FileLoggerBase
    {
        #region Private members
        private IList<string> _errorList;
        private IList<string> _warningList;

        private XmlDocument _xmlDoc;
        private XmlElement _rootElement;
        private XmlElement _errorsElement;
        private XmlElement _warningsElement;


        private Stack<XmlElement> _buildElements;
        private Stack<XmlElement> _projectElements;
        private Stack<XmlElement> _targetElements;
        private Stack<XmlElement> _taskElements;

        /// <summary>
        /// Used to help determine what the last thing to build was for placement of message/warning/error
        /// </summary>
        private Stack<BuildType> _buildTypeList;

        #endregion
        public override void Initialize(IEventSource eventSource)
        {
            //try
            {
                //have base init the parameters
                base.Initialize(eventSource);


                _errorList = new List<string>();
                _warningList = new List<string>();

                _buildElements = new Stack<XmlElement>();
                _projectElements = new Stack<XmlElement>();
                _targetElements = new Stack<XmlElement>();
                _taskElements = new Stack<XmlElement>();
                _buildTypeList = new Stack<BuildType>();

                //apply default values
                FileName = "XmlLog.log.xml";
                Append = false;
                ShowSummary = false;



                this.InitalizeEvents(eventSource);

                this.InitalizeParameters();

                this.InitalizeXmlDoc();
            }
            //catch (Exception e)
            //{
            //    string message = "Unable to initalize; messagee=" + e.Message;
            //    throw new LoggerException(message, e);
            //}
        }

        /// <summary>
        /// This will regesiter all the events with MSBuild
        /// </summary>
        /// <param name="eventSource"></param>
        protected void InitalizeEvents(IEventSource eventSource)
        {
            try
            {
                eventSource.BuildStarted += new BuildStartedEventHandler(this.BuildStarted);
                eventSource.BuildFinished += new BuildFinishedEventHandler(this.BuildFinished);
                eventSource.ProjectStarted += new ProjectStartedEventHandler(this.ProjectStarted);
                eventSource.ProjectFinished += new ProjectFinishedEventHandler(this.ProjectFinished);
                eventSource.TargetStarted += new TargetStartedEventHandler(this.TargetStarted);
                eventSource.TargetFinished += new TargetFinishedEventHandler(this.TargetFinished);
                eventSource.TaskStarted += new TaskStartedEventHandler(this.TaskStarted);
                eventSource.TaskFinished += new TaskFinishedEventHandler(this.TaskFinished);
                eventSource.ErrorRaised += new BuildErrorEventHandler(this.BuildError);
                eventSource.WarningRaised += new BuildWarningEventHandler(this.BuildWarning);
                eventSource.MessageRaised += new BuildMessageEventHandler(this.BuildMessage);
            }
            catch (Exception e)
            {
                string message = "Unable to initalize events; message=" + e.Message;
                throw new LoggerException(message, e);
            }
        }

        protected void InitalizeXmlDoc()
        {
            this._xmlDoc = new XmlDocument();
            if (Append)
            {
                try
                {
                    _xmlDoc.Load(FileName);
                    _rootElement = _xmlDoc.DocumentElement;
                }
                catch (Exception e)
                {
                    //TODO: Should we try not to save it to that file???
                    string message = "Unable to load the xml document from: " + FileName;
                    throw new LoggerException(message, e);
                }
            }
            else
            {
                _rootElement = this._xmlDoc.CreateElement("MSBuild");
            }

            XmlAttribute createAtt = _xmlDoc.CreateAttribute("Started");
            createAtt.Value = DateTime.UtcNow.ToString();

            this._xmlDoc.AppendChild(_rootElement);
        }


        /// <summary>
        /// This is automagically called by MSBuild at the end of the build
        /// </summary>
        public override void Shutdown()
        {
            try
            {
                //TODO: Add error and warning elements here
                this._xmlDoc.Save(FileName);
            }
            catch (Exception e)
            {
                string message = "Unable to save the log to [" + FileName + "]";
                throw new LoggerException(message, e);
            }
        }

        #region Logging handlers
        void BuildStarted(object sender, BuildStartedEventArgs e)
        {
            _buildTypeList.Push(BuildType.Build);

            XmlElement buildElement = _xmlDoc.CreateElement("Build");

            _rootElement.AppendChild(buildElement);
            buildElement.Attributes.Append(CreateStartedAttribute(e.Timestamp));
            buildElement.Attributes.Append(CreateAttribute("Verbosity", this.Verbosity.ToString()));

            if (this.Parameters != null && base.IsVerbosityAtLeast(LoggerVerbosity.Detailed))
            {
                XmlElement paramElement = _xmlDoc.CreateElement("LoggerParameters");
                buildElement.AppendChild(paramElement);
                foreach (string current in DefiniedParameters)
                //foreach (string current in this.Parameters.Split(";".ToCharArray()))
                {
                    XmlElement currentElement = _xmlDoc.CreateElement("Parameter");
                    currentElement.InnerText = current + "=" + GetParameterValue(current);
                    paramElement.AppendChild(currentElement);
                }
            }

            buildElement.AppendChild(CreateMessageElement(e.Message));

            _buildElements.Push(buildElement);

        }
        void BuildFinished(object sender, BuildFinishedEventArgs e)
        {
            XmlElement buildElement = _buildElements.Pop();
            buildElement.Attributes.Append(CreateFinishedAttribute(e.Timestamp));
            buildElement.Attributes.Append(CreateAttribute("Succeeded", e.Succeeded.ToString()));

            buildElement.AppendChild(CreateMessageElement(e.Message));

            _buildTypeList.Pop();
        }
        void ProjectStarted(object sender, ProjectStartedEventArgs e)
        {
            _buildTypeList.Push(BuildType.Project);

            XmlElement projectElement = _xmlDoc.CreateElement("Project");
            _projectElements.Push(projectElement);

            _buildElements.Peek().AppendChild(projectElement);

            projectElement.Attributes.Append(CreateAttribute("Name", e.ProjectFile));

            projectElement.Attributes.Append(CreateAttribute("Message", e.Message));
            projectElement.Attributes.Append(CreateStartedAttribute(e.Timestamp));

            if (base.IsVerbosityAtLeast(LoggerVerbosity.Detailed))
            {
                projectElement.Attributes.Append(CreateAttribute("SenderName", e.SenderName));
            }

            if (base.IsVerbosityAtLeast(LoggerVerbosity.Diagnostic))
            {
                XmlElement propertiesElement = _xmlDoc.CreateElement("Properties");
                projectElement.AppendChild(propertiesElement);

                foreach (DictionaryEntry current in e.Properties)
                {
                    if (current.Equals(null) || current.Key == null || current.Key.Equals(string.Empty) ||
                        current.Value == null || current.Value.Equals(string.Empty))
                    {
                        continue;
                    }
                    XmlElement newElement = _xmlDoc.CreateElement(current.Key.ToString());
                    newElement.InnerText = current.Value.ToString();
                    propertiesElement.AppendChild(newElement);
                }   
            }
        }

        void ProjectFinished(object sender, ProjectFinishedEventArgs e)
        {
            XmlElement projectElement = _projectElements.Pop();

            projectElement.Attributes.Append(CreateFinishedAttribute(e.Timestamp));

            _buildTypeList.Pop();
        }
        void TargetStarted(object sender, TargetStartedEventArgs e)
        {
            _buildTypeList.Push(BuildType.Target);

            XmlElement targetElement = _xmlDoc.CreateElement("Target");
            _targetElements.Push(targetElement);
            _projectElements.Peek().AppendChild(targetElement);

            targetElement.Attributes.Append(CreateStartedAttribute(e.Timestamp));
            targetElement.Attributes.Append(CreateAttribute("Name", e.TargetName));

            targetElement.Attributes.Append(CreateAttribute("Message", e.Message));

            if (base.IsVerbosityAtLeast(LoggerVerbosity.Detailed))
            {
                targetElement.Attributes.Append(CreateAttribute("TargetFile",e.TargetFile));
                targetElement.Attributes.Append(CreateAttribute("ProjectFile",e.ProjectFile));
            }

        }
        void TargetFinished(object sender, TargetFinishedEventArgs e)
        {
            XmlElement targetElement = _targetElements.Pop();
            _targetElements.Push(targetElement);
            targetElement.Attributes.Append(CreateFinishedAttribute(e.Timestamp));

            targetElement.Attributes.Append(CreateAttribute("Succeeded", e.Succeeded.ToString()));

            if (base.IsVerbosityAtLeast(LoggerVerbosity.Detailed))
            {
                targetElement.Attributes.Append(CreateAttribute("FinishMessage", e.Message));
            }

            _buildTypeList.Pop();
        }
        void TaskStarted(object sender, TaskStartedEventArgs e)
        {
            _buildTypeList.Push(BuildType.Task);

            XmlElement taskElemet = _xmlDoc.CreateElement("Task");
            _taskElements.Push(taskElemet);
            _targetElements.Peek().AppendChild(taskElemet);

            taskElemet.Attributes.Append(CreateStartedAttribute(e.Timestamp));

            taskElemet.Attributes.Append(CreateAttribute("Name", e.TaskName));
        }
        void TaskFinished(object sender, TaskFinishedEventArgs e)
        {
            XmlElement taskElement = _taskElements.Pop();
            taskElement.Attributes.Append(CreateFinishedAttribute(e.Timestamp));

            if (base.IsVerbosityAtLeast(LoggerVerbosity.Detailed))
            {
                taskElement.Attributes.Append(CreateAttribute("FinishMessage", e.Message));
                taskElement.Attributes.Append(CreateAttribute("ProjectFile", e.ProjectFile));
                taskElement.Attributes.Append(CreateAttribute("TaskFile", e.TaskFile));
            }
            _buildTypeList.Pop();
        }
        void BuildError(object sender, BuildErrorEventArgs e)
        {
            XmlElement errorElement = _xmlDoc.CreateElement("Error");

            GetCurrentElement().AppendChild(errorElement);

            if (ShowSummary)
            {
                if (_errorsElement == null)
                {
                    _errorsElement = _xmlDoc.CreateElement("Errors");
                    _buildElements.Peek().AppendChild(_errorsElement);
                }
                _errorsElement.AppendChild(errorElement);
            }
            errorElement.AppendChild(CreateMessageElement(FormatErrorEvent(e)));

            errorElement.Attributes.Append(CreateAttribute("File", e.File));
            errorElement.Attributes.Append(CreateAttribute("Code", e.Code));
            errorElement.Attributes.Append(CreateAttribute("Subcategory", e.Subcategory));
            if (e.HelpKeyword != null && !e.HelpKeyword.Trim().Equals(string.Empty))
            {
                errorElement.Attributes.Append(CreateAttribute("Hint", e.HelpKeyword));
            }

            XmlElement locElement = _xmlDoc.CreateElement("Location");
            errorElement.AppendChild(locElement);
            locElement.Attributes.Append(CreateAttribute("Line", e.LineNumber.ToString()));
            if (e.LineNumber != e.EndLineNumber && e.EndLineNumber > 0)
            {
                locElement.Attributes.Append(CreateAttribute("EndLine", e.EndLineNumber.ToString()));
            }

            locElement.Attributes.Append(CreateAttribute("ColumnNumber", e.ColumnNumber.ToString()));
            if (e.ColumnNumber != e.EndColumnNumber && e.EndColumnNumber > 0)
            {
                locElement.Attributes.Append(CreateAttribute("EndColumnNumber", e.EndColumnNumber.ToString()));
            }
        }
        void BuildWarning(object sender, BuildWarningEventArgs e)
        {
            //first see if we are interested in logging this
            if (!base.IsVerbosityAtLeast(LoggerVerbosity.Normal))
            {
                return;
            }

            XmlElement warningElement = _xmlDoc.CreateElement("Warning");
            //figure out where to place this element

            GetCurrentElement().AppendChild(warningElement);

            if (ShowSummary)
            {
                if (_warningsElement == null)
                {
                    _warningsElement = _xmlDoc.CreateElement("Warnings");
                }
                _warningsElement.AppendChild(warningElement);
            }

            warningElement.AppendChild(CreateMessageElement(FormatWarningEvent(e)));

            warningElement.Attributes.Append(CreateAttribute("Code", e.Code));
            warningElement.Attributes.Append(CreateAttribute("Subcategory", e.Subcategory));

            if (e.HelpKeyword != null && !e.HelpKeyword.Trim().Equals(string.Empty))
            {
                warningElement.Attributes.Append(CreateAttribute("Hint", e.HelpKeyword));
            }

            if (base.IsVerbosityAtLeast(LoggerVerbosity.Detailed))
            {
                XmlElement locElement = _xmlDoc.CreateElement("Location");
                warningElement.AppendChild(locElement);
                locElement.Attributes.Append(CreateAttribute("Line", e.LineNumber.ToString()));
                if (e.LineNumber != e.EndLineNumber && e.EndLineNumber > 0)
                {
                    locElement.Attributes.Append(CreateAttribute("EndLine", e.EndLineNumber.ToString()));
                }

                locElement.Attributes.Append(CreateAttribute("ColumnNumber", e.ColumnNumber.ToString()));
                if (e.ColumnNumber != e.EndColumnNumber && e.EndColumnNumber > 0)
                {
                    locElement.Attributes.Append(CreateAttribute("EndColumnNumber", e.EndColumnNumber.ToString()));
                }
            }
        }
        void BuildMessage(object sender, BuildMessageEventArgs e)
        {
            //first figure out if we are interested in actually logging this
            switch (e.Importance)
            {
                case (MessageImportance.High):
                    break;
                case (MessageImportance.Normal):
                    if (!base.IsVerbosityAtLeast(LoggerVerbosity.Normal))
                        return;
                    break;
                case (MessageImportance.Low):
                    if (!base.IsVerbosityAtLeast(LoggerVerbosity.Detailed))
                        return;
                    break;
            }

            XmlElement messageElement = _xmlDoc.CreateElement("Message");
            GetCurrentElement().AppendChild(messageElement);

            messageElement.InnerText = e.Message;

            messageElement.Attributes.Append(CreateAttribute("Importance", e.Importance.ToString()));
            if (e.HelpKeyword != null && !e.HelpKeyword.Trim().Equals(string.Empty))
            {
                messageElement.Attributes.Append(CreateAttribute("MessageKeyword", e.HelpKeyword));
            }

            if (base.IsVerbosityAtLeast(LoggerVerbosity.Detailed))
            {
                messageElement.Attributes.Append(CreateAttribute("SenderName", e.SenderName));
            }

        }
        void CustomEvent(object sender, CustomBuildEventArgs e)
        {
            //Do nothing, really a place holder for sub-classes
        }
        #endregion

        #region Convienence methods
        protected XmlElement GetCurrentElement()
        {
            try
            {
                //when you override targets a message is created before any other events get fired
                if (_buildTypeList.Count <= 0)
                {
                    return this._rootElement;
                }


                switch (_buildTypeList.Peek())
                {
                    case (BuildType.Build):
                        return _buildElements.Peek();

                    case (BuildType.Project):
                        return _projectElements.Peek();

                    case (BuildType.Target):
                        return _targetElements.Peek();

                    case (BuildType.Task):
                        return _taskElements.Peek();

                    default:
                        return _rootElement;    //should never get here
                }
            }
            catch (Exception e)
            {
                this._xmlDoc.Save(FileName);
                throw new LoggerException("Unable to get the current element", e);
            }
        }
        protected XmlAttribute CreateAttribute(string name, string value)
        {
            try
            {
                XmlAttribute att = _xmlDoc.CreateAttribute(name);
                att.Value = value;
                return att;
            }
            catch (Exception e)
            {
                string message = "Unable to create attribute; name=" + name + ", value=" + value;
                throw new LoggerException(message);
            }
        }
        protected XmlAttribute CreateFinishedAttribute(DateTime time)
        {
            try
            {
                XmlAttribute att = _xmlDoc.CreateAttribute("Finished");
                att.Value = time.ToString();
                return att;
            }
            catch (Exception e)
            {
                throw new LoggerException("Unable to create finished attribute");
            }
        }
        protected XmlAttribute CreateStartedAttribute(DateTime time)
        {
            try
            {
                XmlAttribute att = _xmlDoc.CreateAttribute("Started");
                att.Value = time.ToString();
                return att;
            }
            catch (Exception e)
            {
                throw new LoggerException("Unable to create started att");
            }
        }
        protected XmlElement CreateMessageElement(string message)
        {
            try
            {
                XmlElement messageElement = _xmlDoc.CreateElement("Message");
                messageElement.InnerText = message;
                return messageElement;
            }
            catch (Exception e)
            {
                throw new LoggerException("Unable to create message element");
            }
        }



        #endregion

    }

    enum BuildType
    {
        Build = 4,
        Project = 3,
        Target = 2,
        Task = 1
    }
}
