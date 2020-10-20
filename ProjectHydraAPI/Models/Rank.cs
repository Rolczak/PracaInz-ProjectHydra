using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHydraAPI.Models
{
    public class Rank
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Hierarchy { get; set; }
    }

    public class RankCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Hierarchy { get; set; }
    }

    public class RankSelect
    { 
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
