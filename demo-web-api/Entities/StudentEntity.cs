using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo_web_api.Entities
{
    public class StudentEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public int TeacherId { get; set; }
        public TeacherEntity Teacher { get; set; }
    }
}
