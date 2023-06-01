﻿namespace MVCCrudProject.Models
{
    public class UpdateEmployeeViewModel
    {
        public Guid Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public long Salary { get; set; }
        public int DeptID { get; set; }
    }
}
