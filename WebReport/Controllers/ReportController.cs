using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
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
        protected readonly IConfiguration _config;

        public ReportController(MyDbContext dbContext, IUnitOfWork uow, IConfiguration config)
        {
            _dbContext = dbContext;
            _uow = uow;
            _config = config;
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
                Expression<Func<Event, bool>> filter = c => (string.IsNullOrEmpty(obj.name) || c.real_name.ToString().ToLower().Contains(obj.name.ToLower()))
                                                    && (string.IsNullOrEmpty(obj.department) || c.Subject.department.ToString().ToLower().Contains(obj.department.ToLower()))
                                                    && ((obj.todate == null) || c.timestamp >= obj.todate)
                                                    && ((obj.fromdate == null) || c.timestamp <= obj.fromdate)
                                                    && (string.IsNullOrEmpty(obj.description) || c.Subject.description.ToString().ToLower().Contains(obj.description.ToLower()))
                                                    && (c.fmp_error == 0)
                                                    && (c.pass_type == 1);

                var data = _uow.Repository<Event>().Include(x => x.Subject, x => x.Subject.Attendance).Where(filter);
                
                IEnumerable<Event> dt = data;
                var count = dt.Count();
                if (count == 0)
                {
                    return Ok(new { code = "1", msg = "success", data = "", total = count });
                }

                if (obj.StartNumber >= 0 && obj.PageSize > 0)
                {
                    dt = dt.Skip(obj.StartNumber).Take(obj.PageSize);
                }
                var report = dt.Select(a => new RepostModel()
                {
                    id = a.id,
                    subject_id = (int)a.subject_id,
                    description = a.Subject.description,
                    department = a.Subject.department,
                    name = a.real_name,
                    job_number = a.Subject.job_number,
                    title = a.Subject.title,
                    email = a.Subject.email,
                    phone = a.Subject.phone,
                    timestamp = (int)a.timestamp,
                    camera_position = a.camera_position,
                    AttendanceList = a.Subject.Attendance.Select(x => new AttendanceList
                    {
                        id = x.id,
                        subject_id = (int)x.subject_id,
                        earliest_record = (int)x.earliest_record,
                        latest_record = (int)x.latest_record,
                    }).ToList(),
                });
                var x = report.GroupBy(a => a.name).Select(z => z.FirstOrDefault()).ToList();
                    var arrData = new List<object>();
                for (int i = 0; i < x.Count(); i++)
                {
                    var AttendanceData = _uow.Repository<Attendance>().Include(c => c.Subject).FirstOrDefault(v => v.subject_id == x[i].subject_id);
                    var EventData = _uow.Repository<Event>().Include(c => c.Subject).FirstOrDefault(v => v.id == (int)AttendanceData.earliest_record);
                    var EventData2 = _uow.Repository<Event>().Include(c => c.Subject).FirstOrDefault(v => v.id == (int)AttendanceData.latest_record);
                    var RepostData1 = new ListData()
                    {
                        id = EventData.id,
                        subject_id = (int)EventData.subject_id,
                        name = EventData.real_name,
                        description = EventData.Subject.description,
                        department = EventData.Subject.department,
                        job_number = EventData.Subject.job_number,
                        title = EventData.Subject.title,
                        email = EventData.Subject.email,
                        phone = EventData.Subject.phone,
                        timestamp = (int)EventData.timestamp,
                        firstlogin = (int)EventData.timestamp,
                        lastlogout = (int)EventData2.timestamp,
                        camera_position = EventData.camera_position,
                    };
                    arrData.Add(RepostData1);
                }
                return Ok(new { code = "1", msg = "success", data = arrData, total = arrData.Count() });
                //return Ok(new { code = "1", msg = "success", data = report, total = count });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //xuất excel
        [HttpGet("ExportExcel")]
        public IActionResult Export(string jsonData)
        {
            try
            {
                //if (HttpContext.Items["UserInfo"] is not CurrentUserModel userInfo)
                //{
                //    return Unauthorized();
                //}

                var obj = JsonSerializer.Deserialize<RepostSearchModel>(jsonData);
                Expression<Func<Event, bool>> filter = c => (string.IsNullOrEmpty(obj.name) || c.real_name.ToString().ToLower().Contains(obj.name.ToLower()))
                                                    && (string.IsNullOrEmpty(obj.department) || c.Subject.department.ToString().ToLower().Contains(obj.department.ToLower()))
                                                    && ((obj.todate == null) || c.timestamp >= obj.todate)
                                                    && ((obj.fromdate == null) || c.timestamp <= obj.fromdate)
                                                    && (string.IsNullOrEmpty(obj.description) || c.Subject.description.ToString().ToLower().Contains(obj.description.ToLower()))
                                                    && (c.fmp_error == 0)
                                                    && (c.pass_type == 1);

                var data = _uow.Repository<Event>().Include(x => x.Subject, x => x.Subject.Attendance).Where(filter);

                IEnumerable<Event> dt = data;
                var count = dt.Count();
                if (count == 0)
                {
                    return Ok(new { code = "1", msg = "success", data = "", total = count });
                }

                if (obj.StartNumber >= 0 && obj.PageSize > 0)
                {
                    dt = dt.Skip(obj.StartNumber).Take(obj.PageSize);
                }
                var report = dt.Select(a => new RepostModel()
                {
                    id = a.id,
                    subject_id = (int)a.subject_id,
                    description = a.Subject.description,
                    department = a.Subject.department,
                    name = a.real_name,
                    job_number = a.Subject.job_number,
                    title = a.Subject.title,
                    email = a.Subject.email,
                    phone = a.Subject.phone,
                    timestamp = (int)a.timestamp,
                    camera_position = a.camera_position,
                    AttendanceList = a.Subject.Attendance.Select(x => new AttendanceList
                    {
                        id = x.id,
                        subject_id = (int)x.subject_id,
                        earliest_record = (int)x.earliest_record,
                        latest_record = (int)x.latest_record,
                    }).ToList(),
                });
                var x = report.GroupBy(a => a.name).Select(z => z.FirstOrDefault()).ToList();
                var arrData = new List<object>();
                for (int i = 0; i < x.Count(); i++)
                {
                    var AttendanceData = _uow.Repository<Attendance>().Include(c => c.Subject).FirstOrDefault(v => v.subject_id == x[i].subject_id);
                    var EventData = _uow.Repository<Event>().Include(c => c.Subject).FirstOrDefault(v => v.id == (int)AttendanceData.earliest_record);
                    var EventData2 = _uow.Repository<Event>().Include(c => c.Subject).FirstOrDefault(v => v.id == (int)AttendanceData.latest_record);
                    var RepostData1 = new ListData()
                    {
                        id = EventData.id,
                        subject_id = (int)EventData.subject_id,
                        name = EventData.real_name,
                        description = EventData.Subject.description,
                        department = EventData.Subject.department,
                        job_number = EventData.Subject.job_number,
                        title = EventData.Subject.title,
                        email = EventData.Subject.email,
                        phone = EventData.Subject.phone,
                        timestamp = (int)EventData.timestamp,
                        firstlogin = (int)EventData.timestamp,
                        lastlogout = (int)EventData2.timestamp,
                        camera_position = EventData.camera_position,
                    };
                    arrData.Add(RepostData1);
                }

                //var fullPath = Path.Combine(_config["Template:AuditDocsTemplate"], "ReportFile.xlsx");
                //fullPath = fullPath.ToString().Replace("\\", "/");
                //var template = new FileInfo(fullPath);

                var template = new FileInfo(@"D:\test\ReportFile.xlsx");

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                ExcelPackage excelPackage;
                byte[] Bytes = null;
                var memoryStream = new MemoryStream();
                using (excelPackage = new ExcelPackage(template, false))
                {
                    var worksheet = excelPackage.Workbook.Worksheets["Sheet1"];
                    var startrow = 2;
                    var startcol = 1;
                    var index = 0;

                    var checkEvent = _uow.Repository<Event>().Find(z => z.fmp_error == 0 && z.pass_type == 1).ToArray();
                    foreach (var a in arrData)
                    {
                        ExcelRange cellNo = worksheet.Cells[startrow, startcol];
                        cellNo.Value = index;
                        //
                        ExcelRange dataRp = worksheet.Cells[startrow, startcol];
                        //dataRp.Value = string.Join(", ", a);
                        //
                        ExcelRange AdminFrameworkAuditDetect = worksheet.Cells[startrow, startcol + 2];
                        //AdminFrameworkAuditDetect.Value = string.Join(", ", (a.admin_framework == 1 ? "Quản trị/Tổ chức/Số liệu tài chính" :
                        //    a.admin_framework == 2 ? "Hoạt động/Quy trình/Quy định" :
                        //    a.admin_framework == 3 ? "Kiểm sát vận hành/Thực thi" :
                        //    a.admin_framework == 4 ? "Công nghệ thông tin" : ""));
                        ////
                        //ExcelRange ClassifyAuditDetect = worksheet.Cells[startrow, startcol + 3];
                        //ClassifyAuditDetect.Value = string.Join(", ", a.CatDetectType.Name);
                        ////
                        //ExcelRange RatingRiskAuditDetect = worksheet.Cells[startrow, startcol + 4];
                        //RatingRiskAuditDetect.Value = string.Join(", ", (a.rating_risk == 1 ? "Cao/Quan trọng" :
                        //    a.rating_risk == 2 ? "Trung bình" :
                        //    a.rating_risk == 3 ? "Thấp/Ít quan trọng" : ""));
                        ////
                        //ExcelRange CodeAuditDetect = worksheet.Cells[startrow, startcol + 5];
                        //CodeAuditDetect.Value = string.Join(", ", a.code);
                        ////
                        //ExcelRange TitleAuditDetect = worksheet.Cells[startrow, startcol + 6];
                        //TitleAuditDetect.Value = string.Join(", ", a.title);
                        ////
                        //ExcelRange DescriptionAuditDetect = worksheet.Cells[startrow, startcol + 7];
                        //DescriptionAuditDetect.Value = string.Join(", ", a.description);
                        ////
                        //ExcelRange EvidenceAuditDetect = worksheet.Cells[startrow, startcol + 8];
                        //EvidenceAuditDetect.Value = string.Join(", ", a.evidence);
                        ////
                        //ExcelRange AffectAuditDetect = worksheet.Cells[startrow, startcol + 9];
                        //AffectAuditDetect.Value = string.Join(", ", a.affect);
                        ////
                        //ExcelRange CauseAuditDetect = worksheet.Cells[startrow, startcol + 10];
                        //CauseAuditDetect.Value = string.Join(", ", a.cause);
                        ////
                        //ExcelRange CodeARM = worksheet.Cells[startrow, startcol + 11];
                        //CodeARM.Value = string.Join(", ", codeARM);
                        ////
                        //ExcelRange ReeportAuditDetect = worksheet.Cells[startrow, startcol + 12];
                        //ReeportAuditDetect.Value = string.Join(", ", (a.audit_report == true ? "Có" : "Không"));
                        ////
                        //ExcelRange OpinionAuditDetect = worksheet.Cells[startrow, startcol + 13];
                        //OpinionAuditDetect.Value = string.Join(", ", (a.opinion_audit == true ? "Đồng ý " : "Không đồng ý"));
                        ////
                        //ExcelRange ReasonAuditDetect = worksheet.Cells[startrow, startcol + 14];
                        //ReasonAuditDetect.Value = string.Join(", ", a.reason);
                        ////
                        //ExcelRange PaperCodeAuditDetect = worksheet.Cells[startrow, startcol + 15];
                        //PaperCodeAuditDetect.Value = string.Join(", ", codeAO);
                        ////
                        //ExcelRange AuditprocessNameAuditDetect = worksheet.Cells[startrow, startcol + 16];
                        //AuditprocessNameAuditDetect.Value = string.Join(", ", a.auditprocess_name);
                        ////
                        //ExcelRange AuditfacilitiesNameAuditDetect = worksheet.Cells[startrow, startcol + 17];
                        //AuditfacilitiesNameAuditDetect.Value = string.Join(", ", a.auditfacilities_name);
                        ////
                        //ExcelRange StatusNameAuditDetect = worksheet.Cells[startrow, startcol + 18];
                        //StatusNameAuditDetect.Value = string.Join(", ", statusName.StatusName);
                        startrow++;
                    }
                    using ExcelRange r = worksheet.Cells[5, 1, startrow, 19];
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                    Bytes = excelPackage.GetAsByteArray();
                }
                return File(Bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReportFile.xlsx");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
