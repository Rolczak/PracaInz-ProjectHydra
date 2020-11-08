using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHydraAPI.Models
{
    public class Class
    {
        [Key]
        public int Id { get; set; }
        public string Topic { get; set; }
        public string TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public AppUser Teacher { get; set; }
        public int? UnitId { get; set; }
        [ForeignKey("UnitId")]
        public virtual Unit Unit { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public float Time { get; set; }
        public string Comment { get; set; }
    }

    public class ClassVM
    {
        public int Id { get; set; }
        public string TeacherId { get; set; }
        public int? UnitId { get; set; }
        public string Topic { get; set; }
        public string TeacherName { get; set; }
        public string Unit { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public float Time { get; set; }
        public string Comment { get; set; }
    }
}
