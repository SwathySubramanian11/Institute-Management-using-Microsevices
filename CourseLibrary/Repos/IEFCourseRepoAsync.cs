using CourseLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLibrary.Repos
{
    public interface IEFCourseRepoAsync
    {
        Task<List<Course>> GetAllCoursesAsync();
        Task<Course> GetCourseAsync(string cc);
        Task InsertCourseAsync(Course crs);
        Task UpdateCourseAsync(string cc, Course crs);
        Task DeleteCourseAsync(string cc);
    }
}
