using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColTrain.Shared.DTO.Models
{
    public class UserTable : BaseTable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Int16 UserType { get; set; }
    }
}
