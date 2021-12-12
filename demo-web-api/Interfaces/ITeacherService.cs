using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_web_api.Models.Requests;
using demo_web_api.Models.Responses;

namespace demo_web_api.Interfaces
{
    public interface ITeacherService
    {
        Task Add(AddTeacherRequest request);
        Task<TeacherResponse> Get(int teacherId);
    }
}
