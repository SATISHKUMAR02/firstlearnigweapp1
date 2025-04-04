namespace WebApplication1.Data.Repository
{
    public interface IStudentRepository:ICollegeRepository<Student>
    {
        Task<List<Student>> GetStudentsByFeeStatusAsync(int feestatus);
    }
}
