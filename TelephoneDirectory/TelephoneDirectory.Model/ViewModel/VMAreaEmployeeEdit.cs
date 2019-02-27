using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.DAL.Entities;

namespace TelephoneDirectory.Model.ViewModel
{
    public class VMAreaEmployeeEdit
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
        public int SupervisorId { get; set; }
        public int DepartmentId { get; set; }
        public int TitleId { get; set; }

        public static VMAreaEmployeeEdit Parse(Employee item)
        {
            VMAreaEmployeeEdit result = new VMAreaEmployeeEdit
            {
                EmployeeId = item.EmployeeId,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Telephone = item.Telephone,
                Email = item.Email,
                Password = item.Password,
                IsActive = item.IsActive,
                RoleId = item.RoleId,
                SupervisorId = item.SupervisorId,
                DepartmentId = item.DepartmentId,
                TitleId = item.TitleId
            };

            return result;
        }
    }
}
