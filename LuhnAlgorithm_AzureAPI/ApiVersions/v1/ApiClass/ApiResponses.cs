using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuhnAlgorithm_AzureAPI.ApiVersions.v1.ApiClasses
{
    public class ApiResponses_CheckLuhnCalculation
    {
        public string value { get; set; }
        public bool result { get; set; }
        public int? correctedLuhn { get; set; }
    }

    public class ApiResponses_BadRequest
    {
        public string message { get; set; }
    }
}
