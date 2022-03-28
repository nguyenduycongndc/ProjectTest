using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;
using WebReport.DataAccess;
using WebReport.ExecuteModels;
using WebReport.Models;

namespace WebReport.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    public class ReportController : Controller
    {
        private readonly MyDbContext _dbContext;
        public ReportController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            //IEnumerable<Repost> data = _dbContext.Reposts;
            return View();
        }
        //[HttpGet("Search")]
        [HttpGet]
        public async Task<IActionResult> Search(string jsonData)
        {
            try
            {
                var obj = JsonSerializer.Deserialize<RepostSearchModel>(jsonData);
                Expression<Func<Repost, bool>> filter = c => (string.IsNullOrEmpty(obj.name) || c.Name.ToLower().Contains(obj.name.ToLower()))
                                                    && (string.IsNullOrEmpty(obj.department) || c.Department.ToLower().Contains(obj.department.ToLower()))
                                                    && (string.IsNullOrEmpty(obj.branch) || c.Branch.ToLower().Contains(obj.branch.ToLower()));

                var data = await _dbContext.Reposts.FindAsync(filter);
                //var report = data.Select(a => new RepostModel()
                //{
                //    id = a.Id,
                //    name = a.Name,
                //    department = a.Department,
                //    branch = a.Branch,
                //});
                return View(data);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
