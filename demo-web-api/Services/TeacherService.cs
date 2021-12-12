using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_web_api.Entities;
using demo_web_api.Interfaces;
using demo_web_api.Models.Requests;
using demo_web_api.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace demo_web_api.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly DemoDbContext _db;

        public TeacherService(DemoDbContext db)
        {
            _db = db;
        }

        public async Task Add(AddTeacherRequest request)
        {
            var teacher = new TeacherEntity
            {
                FullName = request.FullName
            };

            _db.Teachers.Add(teacher);

            await _db.SaveChangesAsync();
        }

        public Task<TeacherResponse> Get(int teacherId)
        {
            var teacherResponse = _db.Teachers
                .Select(c => new TeacherResponse
                {
                    Id = c.Id,
                    FullName = c.FullName
                })
                .FirstOrDefaultAsync(c => c.Id == teacherId);

            return teacherResponse;
        }
    }
}
