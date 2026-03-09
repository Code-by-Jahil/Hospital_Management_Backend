using System.ComponentModel.DataAnnotations;

namespace Hospital_Management.DTO
{
    public class PatientDto
    {
        
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }
        
        public string Phone { get; set; }
        
        public string Address { get; set; }

        public string Diseases { get; set; }

        public DateTime Created { get; set; }
    }
}
