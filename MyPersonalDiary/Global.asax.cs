using MyPersonalDiary.Infrastructure;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using MyPersonalDiary.Domain.Entities;
using MyPersonalDiary.Models;

namespace MyPersonalDiary
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
            Mapper.Initialize(cfg => 
            {
                cfg.CreateMap<Record, RecordViewModel>();
                cfg.CreateMap<RecordViewModel,Record>();
                cfg.CreateMap<CreateRecordViewModel, Record>();
                cfg.CreateMap<RegisterViewModel, User>();
                cfg.CreateMap<LoginViewModel, User>();
                cfg.CreateMap<EditRecordViewModel,Record>();
                cfg.CreateMap<Record, EditRecordViewModel>();
            });
        }
    }
}
