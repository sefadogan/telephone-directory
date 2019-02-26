using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.DAL.Entities;

namespace TelephoneDirectory.Model.ViewModel
{
    public class VMAreaEmployee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public string Title { get; set; }
        public string DepartmentName { get; set; }

        public static VMAreaEmployee Parse(Employee item)
        {
            VMAreaEmployee result = new VMAreaEmployee
            {
                EmployeeId = item.EmployeeId,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Telephone = item.Telephone,
                Email = item.Email,
                IsActive = item.IsActive,
                CreateDate = item.CreateDate,
                Title = item.Title.Name,
                DepartmentName = item.Department.Name
            };

            return result;
        }
        public static List<VMAreaEmployee> Parse(List<Employee> items)
        {
            List<VMAreaEmployee> results = new List<VMAreaEmployee>();
            foreach (var item in items)
            {
                results.Add(VMAreaEmployee.Parse(item));
            }

            return results;
        }
    }
}
