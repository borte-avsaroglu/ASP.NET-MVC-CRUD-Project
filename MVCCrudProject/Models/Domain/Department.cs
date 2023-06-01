using System.ComponentModel.DataAnnotations;

namespace MVCCrudProject.Models.Domain
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string DeptName { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
