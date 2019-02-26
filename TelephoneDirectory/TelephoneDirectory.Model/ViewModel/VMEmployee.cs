using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.DAL.Entities;

namespace TelephoneDirectory.Model.ViewModel
{
    public class VMEmployee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }

        public static VMEmployee Parse(Employee item)
        {
            VMEmployee result = new VMEmployee
            {
                EmployeeId = item.EmployeeId,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Telephone = item.Telephone
            };

            return result;
        }
        public static List<VMEmployee> Parse(List<Employee> items)
        {
            List<VMEmployee> results = new List<VMEmployee>();
            foreach (var item in items)
            {
                if (item.Role.Name.Contains("Admin")) // UI'de sadece normal çalışanları göstermek adına.
                    continue;
                    
                results.Add(VMEmployee.Parse(item));
            }

            return results;
        }
    }
}
