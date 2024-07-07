using BatchLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchLibrary.Repos
{
    public class EFBatchRepoAsync : IEFBatchRepoAsync
    {
        EYBatchDBContext ctx = new EYBatchDBContext();
        public async Task DeleteBatchAsync(string bc)
        {
            Batch bch2Del = await GetBatchAsync(bc);
            ctx.Batches.Remove(bch2Del);
            await ctx.SaveChangesAsync();
        }

        public async Task<List<Batch>> GetAllBatchesAsync()
        {
            List<Batch> batches = await ctx.Batches.ToListAsync();
            return batches;
        }

        public async Task<Batch> GetBatchAsync(string bc)
        {
            try
            {
                Batch batch = await (from bch in ctx.Batches where bch.BatchCode == bc select bch).FirstAsync();
                return batch;
            }
            catch (BatchException)
            {
                throw new BatchException("Batch code not found");
            }
        }

        public async Task<List<Batch>> GetBatchesByCourseCodeAsync(string cc)
        {
            List<Batch> batches = await (from bch in ctx.Batches where bch.CourseCode == cc select bch).ToListAsync();
            if (batches.Count > 0)
                return batches;
            else
                throw new BatchException("Course code not found");
        }

        public async Task InsertBatchAsync(Batch bch)
        {
            await ctx.Batches.AddAsync(bch);
            await ctx.SaveChangesAsync();
        }

        public async Task InsertCourseAsync(Course course)
        {
            await ctx.Courses.AddAsync(course);
            await ctx.SaveChangesAsync();
        }

        public async Task UpdateBatchAsync(string bc, Batch bch)
        {
            Batch bch2Edit = await GetBatchAsync(bc);
            bch2Edit.StartDate = bch.StartDate;
            bch2Edit.EndDate = bch.EndDate;
            bch2Edit.CourseCode = bch.CourseCode;
            await ctx.SaveChangesAsync();
        }
    }
}
