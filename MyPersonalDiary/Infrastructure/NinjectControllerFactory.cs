using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using MyPersonalDiary.Domain.Abstract;
using MyPersonalDiary.Domain.Concrete;
using MyPersonalDiary.Infrastructure.Abstract;
using MyPersonalDiary.Infrastructure.Concrete;

namespace MyPersonalDiary.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        private string connectionString = "MyPersonalDiaryConnection";
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }
        private void AddBindings()
        {
            ninjectKernel.Bind<IUnitOfWork>().To<EfUnitOfWork>().WithConstructorArgument(connectionString);
            ninjectKernel.Bind<IAuthProvider>().To<FormAuthProvider>();
        }
    }
}