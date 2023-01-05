using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColTrain.Services.DTO
{
    public class StateResponseDto : BaseDto
    {
        public string StateCode { get; set; }
        public string StateName { get; set; }
    }
}
