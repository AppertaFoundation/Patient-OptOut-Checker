using System;
using System.ComponentModel.DataAnnotations;

namespace PatientOptOutAPI.Models
{
    public class Log
    {
        [Key]
        public int LogId { get; set; }

        public string Username { get; set; }

        public string Message { get; set; }

        public DateTime DateTime { get; set; }
    }
}
