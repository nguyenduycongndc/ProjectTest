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
        public IActionResult Search(string jsonData)
        {
            try
            {
                var obj = JsonSerializer.Deserialize<RepostSearchModel>(jsonData);
                Expression<Func<Event, bool>> filter = c => (string.IsNullOrEmpty(obj.name) || c.real_name.ToLower().Contains(obj.name.ToLower()))
                                                    && (string.IsNullOrEmpty(obj.department) || c.Subject.department.ToLower().Contains(obj.department.ToLower()))
                                                    && ((obj.todate == null) || c.timestamp >= obj.todate)
                                                    && ((obj.fromdate == null) || c.timestamp <= obj.fromdate)
                                                    && (string.IsNullOrEmpty(obj.description) || c.Subject.description.ToLower().Contains(obj.description.ToLower()))
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
                    DateTime _dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    var RepostData1 = new ListData()
                    {
                        id = EventData.id,
                        subject_id = (int)EventData.subject_id,
                        name = EventData.real_name,
                        description = EventData.Subject.description != null ? EventData.Subject.description : "",
                        department = EventData.Subject.department != null ? EventData.Subject.department : "",
                        job_number = EventData.Subject.job_number != null ? EventData.Subject.job_number : "",
                        title = EventData.Subject.title != null ? EventData.Subject.title : "",
                        email = EventData.Subject.email != null ? EventData.Subject.email : "",
                        phone = EventData.Subject.phone != null ? EventData.Subject.phone : "",
                        time = _dateTime.AddSeconds((double)EventData.timestamp).ToLocalTime().ToString("dd/MM/yyy HH:mm:ss"),
                        timefirstlogin = _dateTime.AddSeconds((double)EventData.timestamp).ToLocalTime().ToString("dd/MM/yyy HH:mm:ss"),
                        timelastlogout = _dateTime.AddSeconds((double)EventData2.timestamp).ToLocalTime().ToString("dd/MM/yyy HH:mm:ss"),
                        timestamp = (int)EventData.timestamp,
                        firstlogin = (int)EventData.timestamp,
                        lastlogout = (int)EventData2.timestamp,
                        camera_position = EventData.camera_position != null ? EventData.camera_position : "",
                    };
                    arrData.Add(RepostData1);
                }
                return  Ok(new { code = "1", msg = "success", data = arrData, total = arrData.Count() });
                //return Ok(new { code = "1", msg = "success", data = report, total = count });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //xuất excel
        [HttpGet]
        public IActionResult Export(string jsonData)
        {
            try
            {
                //if (HttpContext.Items["UserInfo"] is not CurrentUserModel userInfo)
                //{
                //    return Unauthorized();
                //}

                var obj = JsonSerializer.Deserialize<RepostSearchModel>(jsonData);
                Expression<Func<Event, bool>> filter = c => (string.IsNullOrEmpty(obj.name) || c.real_name.ToLower().Contains(obj.name.ToLower()))
                                                    && (string.IsNullOrEmpty(obj.department) || c.Subject.department.ToLower().Contains(obj.department.ToLower()))
                                                    && ((obj.todate == null) || c.timestamp >= obj.todate)
                                                    && ((obj.fromdate == null) || c.timestamp <= obj.fromdate)
                                                    && (string.IsNullOrEmpty(obj.description) || c.Subject.description.ToLower().Contains(obj.description.ToLower()))
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
                var arrData = new List<ListData>();
                for (int i = 0; i < x.Count(); i++)
                {
                    var AttendanceData = _uow.Repository<Attendance>().Include(c => c.Subject).FirstOrDefault(v => v.subject_id == x[i].subject_id);
                    var EventData = _uow.Repository<Event>().Include(c => c.Subject).FirstOrDefault(v => v.id == (int)AttendanceData.earliest_record);
                    var EventData2 = _uow.Repository<Event>().Include(c => c.Subject).FirstOrDefault(v => v.id == (int)AttendanceData.latest_record);
                    DateTime _dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    var RepostData1 = new ListData()
                    {
                        id = EventData.id,
                        subject_id = (int)EventData.subject_id,
                        name = EventData.real_name,
                        description = EventData.Subject.description != null ? EventData.Subject.description : "",
                        department = EventData.Subject.department != null ? EventData.Subject.department : "",
                        job_number = EventData.Subject.job_number != null ? EventData.Subject.job_number : "",
                        title = EventData.Subject.title != null ? EventData.Subject.title : "",
                        email = EventData.Subject.email != null ? EventData.Subject.email : "",
                        phone = EventData.Subject.phone != null ? EventData.Subject.phone : "",
                        time = _dateTime.AddSeconds((double)EventData.timestamp).ToLocalTime().ToString("dd/MM/yyy"),
                        timefirstlogin = _dateTime.AddSeconds((double)EventData.timestamp).ToLocalTime().ToString("HH:mm:ss"),
                        Hoursfirstlogin = _dateTime.AddSeconds((double)EventData.timestamp).ToLocalTime().ToString("HH"),
                        Minutesfirstlogin = _dateTime.AddSeconds((double)EventData.timestamp).ToLocalTime().ToString("mm"),
                        timelastlogout = _dateTime.AddSeconds((double)EventData2.timestamp).ToLocalTime().ToString("HH:mm:ss"),
                        Hourslastlogout = _dateTime.AddSeconds((double)EventData2.timestamp).ToLocalTime().ToString("HH"),
                        Minuteslastlogout = _dateTime.AddSeconds((double)EventData2.timestamp).ToLocalTime().ToString("mm"),
                        timestamp = (int)EventData.timestamp,
                        firstlogin = (int)EventData.timestamp,
                        lastlogout = (int)EventData2.timestamp,
                        camera_position = EventData.camera_position != null ? EventData.camera_position : "",
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
                        //ExcelRange cellNo = worksheet.Cells[startrow, startcol];
                        //cellNo.Value = index;
                        var SumTime = ((((Int64.Parse(a.Hourslastlogout) <= 11) && (Int64.Parse(a.Minuteslastlogout) < 30)) 
                            || ((Int64.Parse(a.Hoursfirstlogin) >= 13) && (Int64.Parse(a.Minutesfirstlogin) > 0))) 
                            ? (Math.Abs(Int64.Parse(a.Hourslastlogout) - Int64.Parse(a.Hoursfirstlogin)) + "giờ" + Math.Abs(Int64.Parse(a.Minuteslastlogout) - Int64.Parse(a.Minutesfirstlogin)) + "phút") : ((Int64.Parse(a.Hourslastlogout) - Int64.Parse(a.Hoursfirstlogin)) == 8 && (Int64.Parse(a.Minuteslastlogout) - Int64.Parse(a.Minutesfirstlogin)) > 0 
                            ? "8 giờ" : (Math.Abs(Int64.Parse(a.Hourslastlogout) - Int64.Parse(a.Hoursfirstlogin) - 1) + "giờ" + Math.Abs(Int64.Parse(a.Minuteslastlogout) - Int64.Parse(a.Minutesfirstlogin) - 30) + "phút")));
                        var Absent = ((Int64.Parse(a.Hoursfirstlogin) >= 10) ? "Vắng buổi sáng" : ((Int64.Parse(a.Hourslastlogout) <= 15) ? "Vắng buổi chiều" : ""));
                        var TimeInOut = ((((Int64.Parse(a.Hourslastlogout) <= 11) && (Int64.Parse(a.Minuteslastlogout) < 30))
                            || ((Int64.Parse(a.Hoursfirstlogin) >= 13) && (Int64.Parse(a.Minutesfirstlogin) > 0)))
                            ? (8 - Math.Abs(Int64.Parse(a.Hourslastlogout) - Int64.Parse(a.Hoursfirstlogin)) + "giờ" + (Math.Abs(Int64.Parse(a.Minuteslastlogout) - Int64.Parse(a.Minutesfirstlogin)) != 0
                            ? (60 - Math.Abs(Int64.Parse(a.Minuteslastlogout) - Int64.Parse(a.Minutesfirstlogin))) : Math.Abs(Int64.Parse(a.Minuteslastlogout) - Int64.Parse(a.Minutesfirstlogin))) + "phút") : ((Int64.Parse(a.Hourslastlogout) - Int64.Parse(a.Hoursfirstlogin)) == 8 && (Int64.Parse(a.Minuteslastlogout) - Int64.Parse(a.Minutesfirstlogin)) > 0
                            ? "8 giờ" : (8 - Math.Abs(Int64.Parse(a.Hourslastlogout) - Int64.Parse(a.Hoursfirstlogin) - 1) + "giờ" + (Math.Abs(Int64.Parse(a.Minuteslastlogout) - Int64.Parse(a.Minutesfirstlogin) - 30) != 0
                            ? (60 - Math.Abs(Int64.Parse(a.Minuteslastlogout) - Int64.Parse(a.Minutesfirstlogin) - 30)) : Math.Abs(Int64.Parse(a.Minuteslastlogout) - Int64.Parse(a.Minutesfirstlogin) - 30)) + "phút")));

                        //
                        ExcelRange dataRp1 = worksheet.Cells[startrow, startcol];
                        dataRp1.Value = string.Join(", ", a.description);
                        //
                        ExcelRange dataRp2 = worksheet.Cells[startrow, startcol + 1];
                        dataRp1.Value = string.Join(", ", a.department);
                        //
                        ExcelRange dataRp3 = worksheet.Cells[startrow, startcol + 2];
                        dataRp3.Value = string.Join(", ", a.name);
                        //
                        ExcelRange dataRp4 = worksheet.Cells[startrow, startcol + 3];
                        dataRp4.Value = string.Join(", ", a.job_number);
                        //
                        ExcelRange dataRp5 = worksheet.Cells[startrow, startcol + 4];
                        dataRp5.Value = string.Join(", ", a.title);
                        //
                        ExcelRange dataRp6 = worksheet.Cells[startrow, startcol + 5];
                        dataRp6.Value = string.Join(", ", a.email);
                        //
                        ExcelRange dataRp7 = worksheet.Cells[startrow, startcol + 6];
                        dataRp7.Value = string.Join(", ", a.phone);
                        //
                        ExcelRange dataRp8 = worksheet.Cells[startrow, startcol + 7];
                        dataRp8.Value = string.Join(", ", a.time);
                        //
                        ExcelRange dataRp9 = worksheet.Cells[startrow, startcol + 8];
                        dataRp9.Value = string.Join(", ", a.timefirstlogin);
                        //
                        ExcelRange dataRp10 = worksheet.Cells[startrow, startcol + 9];
                        dataRp10.Value = string.Join(", ", a.timelastlogout);
                        //
                        ExcelRange dataRp11 = worksheet.Cells[startrow, startcol + 10];//tong tg
                        dataRp11.Value = string.Join(", ", SumTime);
                        //
                        ExcelRange dataRp12 = worksheet.Cells[startrow, startcol + 11];//vang
                        dataRp12.Value = string.Join(", ", Absent);
                        //
                        ExcelRange dataRp13 = worksheet.Cells[startrow, startcol + 12];//thieu gio
                        dataRp13.Value = string.Join(", ", TimeInOut);
                        //
                        ExcelRange dataRp14 = worksheet.Cells[startrow, startcol + 13];//dia diem
                        dataRp14.Value = string.Join(", ", a.camera_position);
                        startrow++;
                    }
                    using ExcelRange r = worksheet.Cells[2, 1, startrow, 14];
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
