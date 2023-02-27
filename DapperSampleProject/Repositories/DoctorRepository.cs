using DapperSampleProject.Models;
using System.Linq.Expressions;
using DapperSampleProject.IRepository;
using DapperSampleProject.Helpers;
using Dapper;
using System.Data;

namespace DapperSampleProject.Repositories
{
    public class DoctorRepository : IGenericRepository<Doctor>
    {
        private readonly ConnectionHelper _connectionHelper;
        public DoctorRepository(ConnectionHelper connectionHelper)
        {
            _connectionHelper = connectionHelper;
        }

        public async Task AddAsync(Doctor entity)
        {
            var query = "INSERT INTO Doctors (Name,Surname,Department,PhoneNumber,Email,CreatedDate,UpdatedDate)" + "VALUES(@Name,@Surname,@Department,@PhoneNumber,@Email,@CreatedDate,@UpdatedDate)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", entity.Name, DbType.String);
            parameters.Add("Surname", entity.Surname, DbType.String);
            parameters.Add("Department", entity.Department, DbType.String);
            parameters.Add("PhoneNumber", entity.PhoneNumber, DbType.String);
            parameters.Add("Email", entity.Email, DbType.String);
            parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime2);
            parameters.Add("UpdatedDate", DateTime.Now, DbType.DateTime2);
            using var connection = _connectionHelper.CreateSqlConnection();
            await connection.ExecuteAsync(query, parameters);

        }

        public Task AddRangeAsync(IEnumerable<Doctor> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<Doctor, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            var query = "SELECT * FROM Doctors";
            using var connection = _connectionHelper.CreateSqlConnection();
            var doctors = await connection.QueryAsync<Doctor>(query);
            return doctors.ToList();
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Doctors WHERE Id=@Id";
            using var connection = _connectionHelper.CreateSqlConnection();
            var doctor = await connection.QueryFirstAsync<Doctor>(query, new { Id = id });
            return doctor;
        }

        public void Remove(Doctor doctor)
        {
            var query = "DELETE FROM Doctors WHERE Id=@Id";
            using var connection = _connectionHelper.CreateSqlConnection();
            connection.Execute(query,new {doctor.Id});
        }

        public void RemoveRange(IEnumerable<Doctor> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(Doctor entity)
        {
            var query = "UPDATE Doctors SET Name=@Name,Surname=@Surname,Department=@Department,PhoneNumber=@PhoneNumber,Email=@Email,CreatedDate=@CreatedDate,UpdatedDate=@UpdatedDate WHERE Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", entity.Id);
            parameters.Add("Name", entity.Name, DbType.String);
            parameters.Add("Surname", entity.Surname, DbType.String);
            parameters.Add("Department", entity.Department, DbType.String);
            parameters.Add("PhoneNumber", entity.PhoneNumber, DbType.String);
            parameters.Add("Email", entity.Email, DbType.String);
            parameters.Add("CreatedDate", entity.CreatedDate, DbType.DateTime2);
            parameters.Add("UpdatedDate", DateTime.Now, DbType.DateTime2);
            using var connection = _connectionHelper.CreateSqlConnection();
            connection.Execute(query, parameters);
        }

        public IEnumerable<Doctor> Where(Expression<Func<Doctor, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
