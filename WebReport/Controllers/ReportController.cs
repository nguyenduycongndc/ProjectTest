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
using WebReport.Repositories;

namespace WebReport.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    public class ReportController : Controller
    {
        private readonly MyDbContext _dbContext;
        protected readonly IUnitOfWork _uow;

        public ReportController(MyDbContext dbContext, IUnitOfWork uow)
        {
            _dbContext = dbContext;
            _uow = uow;
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
                                                    && ((obj.todate == null) || c.date >= obj.todate)
                                                    && ((obj.fromdate == null) || c.date <= obj.fromdate)
                                                    && (string.IsNullOrEmpty(obj.branch) || c.Branch.ToLower().Contains(obj.branch.ToLower()));

                //var data = await _dbContext.Reposts.ToListAsync();
                var data = _uow.Repository<Repost>().Find(filter);
                IEnumerable<Repost> dt = data;
                var count = dt.Count();
                if (count == 0)
                {
                    return Ok(new { code = "1", msg = "success", data = "", total = count });
                }

                if (obj.StartNumber >= 0 && obj.PageSize > 0)
                {
                    data = data.Skip(obj.StartNumber).Take(obj.PageSize);
                }
                var report = data.Select(a => new RepostModel()
                {
                    id = a.Id,
                    name = a.Name,
                    department = a.Department,
                    branch = a.Branch,
                    date = a.date,
                });
                return Ok(new { code = "1", msg = "success", data = report, total = count });
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
