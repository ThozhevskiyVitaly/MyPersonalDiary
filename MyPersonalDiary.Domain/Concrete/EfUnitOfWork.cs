using System;
using MyPersonalDiary.Domain.Abstract;

namespace MyPersonalDiary.Domain.Concrete
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private EfDbContext db;
        private bool disposedValue = false;
        private UserRepository userRepository;
        private RecordRepository recordRepository;
        public EfUnitOfWork(string connectionString)
        {
            db = new EfDbContext(connectionString);
        }
        public IUserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public IRecordRepository Records
        {
            get
            {
                if (recordRepository == null)
                    recordRepository = new RecordRepository(db);
                return recordRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
