using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHydraAPI.Models
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual AppUser User { get; set; }
        public float GradeNumber { get; set; }

        public int LessonId { get; set; }
        [ForeignKey(nameof(LessonId))]
        public virtual Class Lesson { get; set; }

        public string Comment { get; set; }

    }
    public class GradeVM
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public float GradeNumber { get; set; }
        public int LessonId { get; set; }
        public string LessonName { get; set; }
        public string Comment { get; set; }
    }
}
