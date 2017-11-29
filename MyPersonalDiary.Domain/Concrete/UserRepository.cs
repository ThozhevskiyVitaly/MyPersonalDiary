using System.Linq;
using MyPersonalDiary.Domain.Abstract;
using MyPersonalDiary.Domain.Entities;

namespace MyPersonalDiary.Domain.Concrete
{
    public class UserRepository : IUserRepository
    {
        private EfDbContext db;
        public UserRepository(EfDbContext db)
        {
            this.db=db;
        }
        public void Create(User user)=> db.Users.Add(user);
        public User Get(string userName) => db.Users.Where(u => u.Name == userName).FirstOrDefault();
        public bool Exist(string userName) => db.Users.Where(u => u.Name == userName).FirstOrDefault() != null;

        public bool Exist(User user) => db.Users.Where(u => u.Name == user.Name && u.Password == user.Password).FirstOrDefault() != null;
    }
}
