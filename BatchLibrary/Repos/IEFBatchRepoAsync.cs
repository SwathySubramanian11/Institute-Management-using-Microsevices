using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatchLibrary.Models;

namespace BatchLibrary.Repos
{
    public interface IEFBatchRepoAsync
    {
        Task<List<Batch>> GetAllBatchesAsync();
        Task<Batch> GetBatchAsync(string bc);
        Task<List<Batch>> GetBatchesByCourseCodeAsync(string cc);
        Task InsertBatchAsync(Batch bch);
        Task UpdateBatchAsync(string bc, Batch bch);
        Task DeleteBatchAsync(string bc);
        Task InsertCourseAsync(Course course);
    }
}
