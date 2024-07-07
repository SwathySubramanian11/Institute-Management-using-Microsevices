using StudentLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentLibrary.Repos
{
    public interface IEFStudentRepoAsync
    {
        Task<List<Student>> GetAllStudentsAsync();
        Task<Student> GetStudentAsync(string rno);
        Task<List<Student>> GetStudentsByBatchCodeAsync(string bc);
        Task InsertStudentAsync(Student std);
        Task UpdateStudentAsync(string rno, Student std);
        Task DeleteStudentAsync(string rno);
        Task InsertBatchAsync(Batch batch);
    }
}
