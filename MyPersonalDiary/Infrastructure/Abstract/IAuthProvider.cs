using MyPersonalDiary.Domain.Entities;

namespace MyPersonalDiary.Infrastructure.Abstract
{
   public interface IAuthProvider
    {
        bool Authenticate(User user);
        void SignOut();
    }
}
