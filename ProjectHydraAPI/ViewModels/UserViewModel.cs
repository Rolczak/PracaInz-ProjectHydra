using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHydraAPI.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RankName { get; set; }
        public int UnitId { get; set; }
    }

    public class UserDetailsModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string RankName { get; set; }
        public int RankId { get; set; }
        public string PhoneNumber { get; set; }
        public string Birthday { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }

    }
}
