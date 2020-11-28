using ProjectHydraAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHydraAPI.Models
{
    public class Unit
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CommanderId { get; set; }
        [ForeignKey("CommanderId")]
        public virtual AppUser Commander { get; set; }
        public string DeputyCommanderId { get; set; }
        [ForeignKey("DeputyCommanderId")]
        public virtual AppUser DeputyCommander { get; set; }

        public virtual List<Unit> ChildernUnits { get; set; }

        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Unit Parent { get; set; }

        public virtual List<AppUser> SoldiersInUnit { get; set; }
    }
    public class UnitVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CommanderId { get; set; }
        public string CommanderName { get; set; }
        public string DeputyCommanderId { get; set; }
        public string DeputyCommanderName { get; set; }
        public string ParentUnitName { get; set; }

    }

    public class UnitDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CommanderId { get; set; }
        public virtual UserDetailsModel Commander { get; set; }
        public string DeputyCommanderId { get; set; }
        public virtual UserDetailsModel DeputyCommander { get; set; }

        public virtual List<UnitVM> ChildernUnits { get; set; }
        public virtual UnitVM Parent { get; set; }

        public virtual List<UserDetailsModel> SoldiersInUnit { get; set; }
    }
}


