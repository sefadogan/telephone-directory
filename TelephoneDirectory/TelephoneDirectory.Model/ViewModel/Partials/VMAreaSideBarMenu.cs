using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.DAL.Entities;

namespace TelephoneDirectory.Model.ViewModel.Partials
{
    public class VMAreaSideBarMenu
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static VMAreaSideBarMenu Parse(Employee item)
        {
            VMAreaSideBarMenu result = new VMAreaSideBarMenu
            {
                FirstName = item.FirstName,
                LastName = item.LastName
            };

            return result;
        }
    }
}
