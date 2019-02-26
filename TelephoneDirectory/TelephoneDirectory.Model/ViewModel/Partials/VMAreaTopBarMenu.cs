using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelephoneDirectory.DAL.Entities;

namespace TelephoneDirectory.Model.ViewModel.Partials
{
    public class VMAreaTopBarMenu
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public static VMAreaTopBarMenu Parse(Employee item)
        {
            VMAreaTopBarMenu result = new VMAreaTopBarMenu
            {
                FirstName = item.FirstName,
                LastName = item.LastName
            };

            return result;
        }
    }
}
