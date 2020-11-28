using System;
using System.Collections.Generic;
using System.Text;
using static ProjectHydraRestLibary.Models.User;

namespace ProjectHydraRestLibary.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CommanderId { get; set; }
        public virtual User Commander { get; set; }
        public string DeputyCommanderId { get; set; }
        public virtual User DeputyCommander { get; set; }

        public virtual List<Unit> ChildernUnits { get; set; }

        public int? ParentId { get; set; }
        public virtual Unit Parent { get; set; }

        public virtual List<User> SoldiersInUnit { get; set; }
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
