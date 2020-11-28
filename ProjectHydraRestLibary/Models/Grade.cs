using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHydraRestLibary.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public float GradeNumber { get; set; }

        public int LessonId { get; set; }
        public virtual Lesson Lesson { get; set; }

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
