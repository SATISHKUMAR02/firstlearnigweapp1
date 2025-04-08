// Repository is the place where this file directly interacts with the database
// instead of the controllers interacting , an abstraction layer is created
// this abstraction layer prevents the controllers to interact with the database
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Controllers;

namespace WebApplication1.Data.Repository
{                                   // the below line will prevent implementing all the common methods in the current file
    public class StudentRepository : CollegeRepository<Student>, IStudentRepository

    {
        private readonly CollegeDbContext _dbContext;
        public StudentRepository(CollegeDbContext dbContext):base(dbContext)
        {


            //private readonly ILogger<Studentcontrollers> _logger;
            _dbContext = dbContext;
        // private readonly IMapper _mapper;
    }

        public Task<List<Student>> GetStudentsByFeeStatusAsync(int feestatus)
        {
            return null;
        }
    } 
}



























/* 
 
  //public StudentRepository(CollegeDbContext dbContext) { // constructor to initialize 
        //    _dbContext = dbContext;
        //}
        //public async Task<int> CreateAsync(Student student)
        //{
        //    _dbContext.Students.Add(student);
        //    await _dbContext.SaveChangesAsync();
        //    return student.Id;
        //}

        //public async Task<bool> DeleteAsync(Student student)
        //{
        //    //var curstudent = await _dbContext.Students.Where(n => n.Id == id).FirstOrDefaultAsync();
        //    //if (curstudent == null)
        //    //{
        //    //    throw new ArgumentNullException("No student found");
        //    //    //return false;


        //    //}
        //    _dbContext.Students.Remove(student);
        //    await _dbContext.SaveChangesAsync();
        //    return true;
        //}
         
        //public async Task<List<Student>> GetAllAsync()
        //{
        //    return await _dbContext.Students.ToListAsync();

        //}

        //public async Task<Student> GetByIdAsync(int id , bool useNoTracking=false )
        //{
        //    if (useNoTracking) {
        //        return await _dbContext.Students.AsNoTracking().Where(n => n.Id == id).FirstOrDefaultAsync();

        //    }
        //    else
        //    {
        //        return await _dbContext.Students.Where(n => n.Id == id).FirstOrDefaultAsync();

        //    }


        //}

        //public async Task<Student> GetByNameAsync(string name)
        //{
        //    return await _dbContext.Students.Where(n => n.StudentName.ToLower().Contains(name.ToLower())).FirstOrDefaultAsync();
        //}

        //public async Task<int> UpdateAsync(Student student)
        //{
        //    var studentToUpdate = await _dbContext.Students.Where(student => student.Id == student.Id).FirstOrDefaultAsync();

        //    //studentToUpdate.StudentName = student.StudentName;
        //    //studentToUpdate.Email = student.Email;
        //    //studentToUpdate.Address = student.Address;
        //    //studentToUpdate.DOB = student.DOB;
        //    _dbContext.Update(student);
        //        await _dbContext.SaveChangesAsync();
        //        return student.Id;
            
        //    throw new ArgumentNullException("No student found");
        //}
 
 */
