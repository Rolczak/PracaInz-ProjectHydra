using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHydraRestLibary.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Topic { get; set; }
        public string TeacherId { get; set; }
        public User Teacher { get; set; }
        public int? UnitId { get; set; }
        public virtual Unit Unit { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public float Time { get; set; }
        public string Comment { get; set; }
    }

    public class LessonVM
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
