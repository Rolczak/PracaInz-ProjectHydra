using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHydraAPI.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PictureUrl { get; set; }

        public DateTime Birthday { get; set; }

        public int? RankId { get; set; }
        [ForeignKey("RankId")]
        public virtual Rank Rank { get; set; }

        public int? UnitId { get; set; }
        [ForeignKey("UnitId")]
        public virtual Unit Unit { get; set; }

    }



}
