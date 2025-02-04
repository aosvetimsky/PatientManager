using System;
using System.Threading.Tasks;
using PatientManagement.Domain.Patients;
using PatientManagement.Persistence.Common;
using PatientManagement.Persistence.Exceptions;

namespace PatientManagement.Persistence.Repositories
{
    internal class PatientRepository : EfRepository<AppDbContext, Patient, Guid>, IPatientRepository, IPatientReadOnlyRepository
    {
        private readonly AppDbContext _db;
        public PatientRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task DeleteAsync(Guid id)
        {
            var entry = await _db.FindAsync<Patient>(id) ?? throw new EntityNotFoundException<Patient>(id);
            _db.Remove(entry);
        }
        public async Task<Patient?> FindAsync(Guid id) => await _db.FindAsync<Patient>(id);
        public async Task<Patient> AddAsync(Patient entity) => (await _db.AddAsync(entity)).Entity;
        public async Task<Patient> UpdateAsync(Patient entity)
        {
            _db.Attach(entity);
            return _db.Update(entity).Entity;
        }
    }
}
