using System.ComponentModel.DataAnnotations;

namespace PatientOptOutAPI.Models
{
    public class Numbers
    {
        [Key]
        public string NHSNumber { get; set; }
        
        public string HospitalNumber { get; set; }
    }
}
