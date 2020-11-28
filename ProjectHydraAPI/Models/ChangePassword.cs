using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHydraAPI.Models
{
    public class ChangePassModel
    {
        public string UserID { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
