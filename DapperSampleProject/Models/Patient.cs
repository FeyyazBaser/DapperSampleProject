namespace DapperSampleProject.Models
{
    public class Patient : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdentityNumber { get; set; }
        public string Disease { get; set; }
        public Doctor Doctor { get; set; }     
        public int DoctorId { get; set; }
    }
}
