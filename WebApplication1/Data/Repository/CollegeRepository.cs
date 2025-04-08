using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data.Repository
{
    public class CollegeRepository<T> : ICollegeRepository<T> where T : class
    {
        private readonly CollegeDbContext _dbContext;
        private DbSet<T> _dbSet; // this will hold the Dbset of table T
        public CollegeRepository(CollegeDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public async Task<T> CreateAsync(T dbRecord)
        {
            _dbSet.Add(dbRecord);
            await _dbContext.SaveChangesAsync();
            return dbRecord;
        }
        public async Task<bool> DeleteAsync(T dbRecord)
        {
            _dbSet.Remove(dbRecord);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<List<T>> GetAllAsync()
        {
            /* 
            ToListAsync and other data retriveal directly from the database , this is done in repository only
            
            */
            return await _dbSet.ToListAsync();

        }

       
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false)
             /* 
        asNoTracking is used mostly in the update part of the operation , for ex - when I try to update a data from an existing user , I create a new user with 
        existing user's Id and then map then map the updated details , and try to save change , if the asNoTracking() is not present , then it will throw an error 
        because due to primary key id , the id can;t be same , so here we are basically updating by creating a new user with new user with same user ID

        if we don't want to track any data from database , then use useNoTrack , by default all the data are being tracked from the database
        ==============================================================================================================================================================================
        */
        {
            if (useNoTracking)
            {
                return await _dbSet.AsNoTracking().Where(filter).FirstOrDefaultAsync();

            }
            else
            {
                return await _dbSet.Where(filter).FirstOrDefaultAsync();

            }


        }
        public async Task<List<T>>GetAllByAnyAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
            {
                return await _dbSet.AsNoTracking().Where(filter).ToListAsync();

            }
            else
            {
                return await _dbSet.Where(filter).ToListAsync();

            }


        }

        //public async Task<T> GetByNameAsync(Expression<Func<T, bool>> filter)
        //{
        //    return await _dbSet.Where(filter).FirstOrDefaultAsync();
        //}

        public async Task<T> UpdateAsync(T dbRecord)
        {
            //var studentToUpdate = await _dbSet.Where(student => student.Id == student.Id).FirstOrDefaultAsync();

            _dbContext.Update(dbRecord);
            await _dbContext.SaveChangesAsync();
            return dbRecord;

            throw new ArgumentNullException("No student found");
        }
    }
}
