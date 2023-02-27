namespace DapperSampleProject.Models
{
    public class Doctor:BaseEntity
    {
      
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Department { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullName { get { return this.Name + " " + this.Surname; } }
        public virtual ICollection<Patient> Patients { get; set; }

    }
}
