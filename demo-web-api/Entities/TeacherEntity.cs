﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace demo_web_api.Entities
{
    public class TeacherEntity
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public List<StudentEntity> Students { get; set; }
    }
}
