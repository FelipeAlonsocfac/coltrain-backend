using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColTrain.Shared.DTO.Models
{
    public class StateTable : BaseTable
    {
        public string StateCode { get; set; }
        public string StateName { get; set; }

        public ICollection<CityTable> Cities { get; set; }
    }
}
