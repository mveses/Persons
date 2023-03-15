using Microsoft.AspNetCore.Mvc;

namespace Persons.Controllers
{
    public class HomeController : Controller
    {

        public RedirectResult Index()
        {
            return new RedirectResult(url: "/UsersList"); 
        }
    }
}