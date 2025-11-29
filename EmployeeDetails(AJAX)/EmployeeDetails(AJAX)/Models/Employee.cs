using System;
using System.Collections.Generic;

namespace EmployeeDetails_AJAX_.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Age { get; set; }
        public DateTime? Hiredate { get; set; }
    }
}
