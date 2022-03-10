using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColTrain.Services.DTO
{
    public class CityRequestDto
    {
        public int StateId { get; set; }
        //public StateTable State { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
    }
}
