using MyPersonalDiary.Domain.Abstract;
using MyPersonalDiary.Infrastructure.Abstract;
using MyPersonalDiary.Infrastructure.Concrete;
using MyPersonalDiary.Domain.Concrete;
using System.Web.Http;
using MyPersonalDiary.Domain.Entities;


namespace MyPersonalDiary.Controllers
{
    [Authorize]
    public class DiaryServiceController : ApiController
    {
        private IAuthProvider authProvider;
        private IUnitOfWork repository;
        public DiaryServiceController()
        {
            repository = new EfUnitOfWork("MyPersonalDiaryConnection");
            authProvider = new FormAuthProvider(repository);
        }
        public DiaryServiceController(IUnitOfWork repo, IAuthProvider provider)
        {
            repository = repo;
            authProvider = provider;
        }
        [HttpGet]
        [Route("{userName}/{password}")]
        [AllowAnonymous]
        public IHttpActionResult SignIn(string userName,string password)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(new User {Name=userName, Password=password}))
                {
                    return Ok();
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [Route("GetRecords")]
        public IHttpActionResult GetRecords()=>Ok(repository.Records.GetAll(User.Identity.Name));
    }
}
