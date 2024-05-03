using GUIs.Models.DAO;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Xml.Linq;

namespace GUIs.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
       
        
    }
}
