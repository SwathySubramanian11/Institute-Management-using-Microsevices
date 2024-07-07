using CourseLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseLibrary.Repos
{
    public class EFCourseRepoAsync : IEFCourseRepoAsync
    {
        EYCourseDBContext ctx = new EYCourseDBContext();
        public async Task DeleteCourseAsync(string cc)
        {
            Course course = await GetCourseAsync(cc);
            ctx.Courses.Remove(course);
            await ctx.SaveChangesAsync();

        }

        public async Task<List<Course>> GetAllCoursesAsync()
        {
            List<Course> courses = await ctx.Courses.ToListAsync();
            return courses;
        }

        public async Task<Course> GetCourseAsync(string cc)
        {
            try
            {
                Course course = await(from c in ctx.Courses where c.CourseCode == cc select c).FirstAsync();
                return course;
            }
            catch
            {
                throw new CourseException("No such Course code Exist");
            }
        }

        public async Task InsertCourseAsync(Course course)
        {
            await ctx.Courses.AddAsync(course);
            await ctx.SaveChangesAsync(); 
        }

        public async Task UpdateCourseAsync(string cc, Course crs)
        {
            Course crs2update = await GetCourseAsync(cc);
            crs2update.CourseTitle = crs.CourseTitle;
            crs2update.Duration = crs.Duration;
            crs2update.CourseFee = crs.CourseFee;
            await ctx.SaveChangesAsync();
        }
    }
}
