using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.DAL.Entities;

namespace TelephoneDirectory.Model.ViewModel
{
    public class VMAreaDepartment
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }

        public static VMAreaDepartment Parse(Department item)
        {
            VMAreaDepartment result = new VMAreaDepartment
            {
                DepartmentId = item.DepartmentId,
                Name = item.Name
            };

            return result;
        }
        public static List<VMAreaDepartment> Parse(List<Department> items)
        {
            List<VMAreaDepartment> results = new List<VMAreaDepartment>();
            foreach (var item in items)
            {
                results.Add(Parse(item));
            }

            return results;
        }
    }
}
