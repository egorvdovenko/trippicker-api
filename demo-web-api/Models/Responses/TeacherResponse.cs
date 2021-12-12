using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_web_api.Entities;

namespace demo_web_api.Models.Responses
{
    public class TeacherResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
