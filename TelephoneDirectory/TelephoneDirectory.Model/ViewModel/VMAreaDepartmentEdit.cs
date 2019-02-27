using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.DAL.Entities;

namespace TelephoneDirectory.Model.ViewModel
{
    public class VMAreaDepartmentEdit
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Abridgment { get; set; }

        public static VMAreaDepartmentEdit Parse(Department item)
        {
            VMAreaDepartmentEdit result = new VMAreaDepartmentEdit
            {
                DepartmentId = item.DepartmentId,
                Name = item.Name,
                Description = item.Description,
                Abridgment = item.Abridgment
            };

            return result;
        }
    }
}
