using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebReport.DataAccess;
using WebReport.Models;

namespace WebReport.Controllers
{
    public class ReportController : Controller
    {
        private readonly MyDbContext _dbContext;
        public ReportController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Repost> data = _dbContext.Reposts;
            return View(data);
        }
    }
}
