using StudentLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentLibrary.Repos
{
    public class EFStudentRepoAsync : IEFStudentRepoAsync
    {
        EYStudentDBContext ctx = new EYStudentDBContext();
        public async Task DeleteStudentAsync(string rno)
        {
            Student stu2Del = await GetStudentAsync(rno);
            ctx.Students.Remove(stu2Del);
            await ctx.SaveChangesAsync();
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            List<Student> students = await ctx.Students.ToListAsync();
            return students;
        }

        public async Task<Student> GetStudentAsync(string rno)
        {
            try
            {
                Student student = await (from st in ctx.Students where st.RollNo == rno select st).FirstAsync();
                return student;
            }
            catch (Exception)
            {
                throw new StudentException("Roll No not found");
            }
        }

        public async Task<List<Student>> GetStudentsByBatchCodeAsync(string bc)
        {
            List<Student> students = await (from st in ctx.Students where st.BatchCode == bc select st).ToListAsync();
            if (students.Count > 0)
                return students;
            else
                throw new StudentException("Batch code not found");

        }

        public async Task InsertBatchAsync(Batch batch)
        {
            await ctx.Batches.AddAsync(batch);
            await ctx.SaveChangesAsync();
        }

        public async Task InsertStudentAsync(Student std)
        {
            await ctx.Students.AddAsync(std);
            await ctx.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(string rno, Student std)
        {
            Student std2Edit = await GetStudentAsync(rno);
            std2Edit.StudentName = std.StudentName;
            std2Edit.StudentAddress = std.StudentAddress;
            std2Edit.BatchCode = std.BatchCode;
            await ctx.SaveChangesAsync();
        }
    }
}
