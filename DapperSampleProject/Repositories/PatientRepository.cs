using DapperSampleProject.Models;
using System.Linq.Expressions;
using DapperSampleProject.IRepository;
using DapperSampleProject.Helpers;
using Dapper;
using System.Data;

namespace DapperSampleProject.Repositories
{
    public class PatientRepository : IGenericRepository<Patient>
    {
        private readonly ConnectionHelper _connectionHelper;
        public PatientRepository(ConnectionHelper connectionHelper)
        {
             _connectionHelper= connectionHelper;
        }
        public async Task AddAsync(Patient entity)
        {
            var query = "INSERT INTO Patients (Name,Surname,IdentityNumber,Disease,DoctorId,CreatedDate,UpdatedDate)" + "VALUES(@Name,@Surname,@IdentityNumber,@Disease,@DoctorId,@CreatedDate,@UpdatedDate)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", entity.Name, DbType.String);
            parameters.Add("Surname", entity.Surname, DbType.String);
            parameters.Add("IdentityNumber", entity.IdentityNumber, DbType.String);
            parameters.Add("Disease", entity.Disease, DbType.String);
            parameters.Add("DoctorId", entity.DoctorId, DbType.String);
            parameters.Add("CreatedDate", DateTime.Now, DbType.DateTime2);
            parameters.Add("UpdatedDate", DateTime.Now, DbType.DateTime2);
            using var connection = _connectionHelper.CreateSqlConnection();
            await connection.ExecuteAsync(query, parameters);
        }

        public Task AddRangeAsync(IEnumerable<Patient> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<Patient, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            var query = "SELECT p.Id,p.Name,p.Surname,IdentityNumber,Disease,p.CreatedDate,DoctorId,d.Name,d.Surname FROM Patients p LEFT JOIN Doctors d ON p.DoctorId=d.Id ";
            using var connection = _connectionHelper.CreateSqlConnection();
            var patients = await connection.QueryAsync<Patient, Doctor, Patient>(query, (patient, doctor) =>
            {
                patient.Doctor = doctor;
                return patient;
            },
            splitOn: "DoctorId");
            return patients.ToList();
       
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            var query = "SELECT * FROM Patients Where Id=@Id";
            using var connection = _connectionHelper.CreateSqlConnection();
            var patient = await connection.QueryFirstAsync<Patient>(query, new { Id = id });
            return patient;
        }

        public void Remove(Patient entity)
        {
            var query = "DELETE FROM Patients Where Id=@Id";
            using var connection=_connectionHelper.CreateSqlConnection();
            connection.Execute(query,new {entity.Id});
        }

        public void RemoveRange(IEnumerable<Patient> entities)
        {
            throw new NotImplementedException();
        }
                                   
        public void Update(Patient entity)
        {
            var query = "UPDATE Patients SET Name=@Name,Surname=@Surname,IdentityNumber=@IdentityNumber,Disease=@Disease,DoctorId=@DoctorId,CreatedDate=@CreatedDate,UpdatedDate=@UpdatedDate WHERE Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", entity.Id);
            parameters.Add("Name", entity.Name, DbType.String);
            parameters.Add("Surname", entity.Surname, DbType.String);
            parameters.Add("IdentityNumber", entity.IdentityNumber, DbType.String);
            parameters.Add("Disease", entity.Disease, DbType.String);
            parameters.Add("DoctorId", entity.DoctorId, DbType.Int32);
            parameters.Add("CreatedDate", entity.CreatedDate, DbType.DateTime2);
            parameters.Add("UpdatedDate", DateTime.Now, DbType.DateTime2);
            using var connection = _connectionHelper.CreateSqlConnection();
            connection.Execute(query, parameters);
        }

        public IEnumerable<Patient> Where(Expression<Func<Patient, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
