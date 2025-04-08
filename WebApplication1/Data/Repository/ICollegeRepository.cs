using System.Linq.Expressions;

namespace WebApplication1.Data.Repository
{
    public interface ICollegeRepository<T> // indicating a generic type
    {
        Task<List<T>> GetAllAsync();

        Task<List<T>>GetAllByAnyAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false);      

        Task<T> GetAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false);

       // Task<T> GetByNameAsync(Expression<Func<T, bool>> filter);

        Task<T> CreateAsync(T dbRecord);

        Task<T> UpdateAsync(T dbRecord);

        Task<bool> DeleteAsync(T dbRecord);
    }
}
/// this is the common repository for this college project for this each controller present in the application

// application -> Linq ->  (EFC) SQL Queries -> from database get data accoringly -> data converted to C# objects  -> C# objects used in the application


