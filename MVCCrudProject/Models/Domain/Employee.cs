using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCCrudProject.Models.Domain
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Fname { get; set; }
        [Required]
        public string Lname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public long Salary { get; set; }
        [Required]
        public int DeptID { get; set; }
        public Department Department { get; set; }

        public string aaaaa { get; set; }
    }
}
