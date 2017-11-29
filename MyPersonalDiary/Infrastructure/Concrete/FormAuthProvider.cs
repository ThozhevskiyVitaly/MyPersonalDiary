using System.Web.Security;
using MyPersonalDiary.Infrastructure.Abstract;
using MyPersonalDiary.Domain.Entities;
using MyPersonalDiary.Domain.Abstract;

namespace MyPersonalDiary.Infrastructure.Concrete
{
    public class FormAuthProvider : IAuthProvider
    {
        IUnitOfWork repository;
        public FormAuthProvider(IUnitOfWork repo)
        {
            repository = repo;    
        }
        public bool Authenticate(User user)
        {
            bool result =repository.Users.Exist(user);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(user.Name, false);
            }
            return result;
        }

        public void SignOut()
        {
           FormsAuthentication.SignOut();
        }
        
    }
}