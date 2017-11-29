using MyPersonalDiary.Domain.Entities;

namespace MyPersonalDiary.Domain.Abstract
{
   public interface IUserRepository
    {
        User Get(string userName);
        bool Exist(string userName);
        void Create(User user);
        bool Exist(User user);
    }
}
