using System;
using System.Collections.Generic;
using System.Linq;

namespace PostMagnet.Domain.Entities
{
    public class Employee
    {
        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string Name { get; set; }
        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual Role Role { get; set; }
        public virtual int Rate { get; set; }
        public virtual IList<Post> Posts { get; set; }
        public virtual IList<Invoice> Invoices { get; set; }
        public virtual Employee Creator { get; set; }
        public virtual IList<Employee> OwnEmployees { get; set; }

        public Employee()
        {
            Posts = new List<Post>();
            Invoices = new List<Invoice>();
            OwnEmployees = new List<Employee>();
        }

        public override string ToString()
        {
            return String.Format(
                "[ Username: {0}, Name: {1}, Email: {2}, Phone: {3}, Active: {4}, Role: {5} ]", Username,
                Name, Email, Phone, IsActive ? "Yes" : "No", Role.Name);
        }
    }
}
