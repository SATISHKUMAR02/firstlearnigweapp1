using System.Linq.Expressions;

namespace WebApplication1.Data.Repository
{
    public interface ICollegeRepository<T> // indicating a generic type
    {
        Task<List<T>> GetAllAsync();

        Task<List<T>>GetAllByAnyAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false);      
        // useNoTracking to false meaning telling the EF Core to track the entitied from the database
// useNoTracking says don;t track the record 
        Task<T> GetAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false);

       // Task<T> GetByNameAsync(Expression<Func<T, bool>> filter);

        Task<T> CreateAsync(T dbRecord);

        Task<T> UpdateAsync(T dbRecord);

        Task<bool> DeleteAsync(T dbRecord);
    }
}
/// this is the common repository for this college project for this each controller present in the application

// application -> Linq ->  (EFC) SQL Queries -> from database get data accoringly -> data converted to C# objects  -> C# objects used in the application
    /*        Repository pattern ========================================================================================================================== important
    this is a way of abstraction in the layer where the controller will not perform database related operations , rather it will be done by the Repository which 
    will be talking to the database

    so instead of creating multiple repositories for each controller , we will be creating a common repo for the enitre application consiting of all the operations 
    like get post delete put , and these repository actions will be used by all the controllers which can perform all sorts of actions

    




*/


