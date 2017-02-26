using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Autofac;
namespace AdaptiveClient.WebDemo.Controllers
{
    public class HomeController : Controller
    {
        private IHttpContextAccessor httpContext;

        public HomeController(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
        }

        public IActionResult Index()
        {
            List<HomeViewModel> viewModels = new List<HomeViewModel>();
            
            // First pass - connect to SQL box on LAN:

            using (var scope = new ContainerBuilder().Build().BeginLifetimeScope(builder => AutofacHelper.RegisterComponents(builder, httpContext)))
            {
                Demo demo = scope.Resolve<Demo>();
                viewModels.Add(demo.BuildViewModel());
            }


            // Second pass - simulate no LAN connectivity, fall back to WebAPI server:

            using (var scope = new ContainerBuilder().Build().BeginLifetimeScope(builder => AutofacHelper.RegisterMocks(builder, httpContext)))
            {
                Demo demo = scope.Resolve<Demo>();
                viewModels.Add(demo.BuildViewModel());
            }

            return View(viewModels);
        }
    }
}