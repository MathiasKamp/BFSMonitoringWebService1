using System;

namespace BFSMonitoringWebService1.Models
{
    public class StatusMessage
    {
        public StatusMessage(string agentName, string directory,string fileName , DateTime dateChecked , string status, DateTime lastModifiedDate)
        {
            AgentName = agentName;
            Directory = directory;
            FileName = fileName;
            DateChecked = dateChecked;
            Status = status;
            LastModifiedDate = lastModifiedDate;
        }

        public string AgentName { get; set; }

        public string Directory { get; set; }

        public string Status { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public DateTime DateChecked { get; set; }

        public string FileName { get; set; }
        
    }
}