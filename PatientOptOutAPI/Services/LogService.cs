using PatientOptOutAPI.Data;
using PatientOptOutAPI.Models;
using System;


namespace PatientOptOutAPI.Services
{
    public class LogService
    {
        private readonly LogContext _logContext;

        public LogService(LogContext logContext)
        {      
            _logContext = logContext;           
        }

        public void LogMessage(string message, string username)
        {
            var logs = _logContext;
            var log = new Log()
            {
                Username = username,
                Message = message,
                DateTime = DateTime.Now
            };

            logs.Logs.Add(log);
            logs.SaveChanges();
        }
    }
}
