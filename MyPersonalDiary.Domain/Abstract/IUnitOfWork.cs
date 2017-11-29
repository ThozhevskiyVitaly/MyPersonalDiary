using System;

namespace MyPersonalDiary.Domain.Abstract
{
   public interface IUnitOfWork:IDisposable
    {
        IUserRepository Users { get; }
        IRecordRepository Records { get; }
        void Save();
    }
}
