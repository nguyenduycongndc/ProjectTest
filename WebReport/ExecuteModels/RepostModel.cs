using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebReport.ExecuteModels
{
    public class RepostModel
    {
        [JsonPropertyName("id")]
        public int id { get; set; }
        [JsonPropertyName("subject_id")]
        public int subject_id { get; set; }
        [JsonPropertyName("name")]
        public string name { get; set; }//họ tên nv
        [JsonPropertyName("description")]
        public string description { get; set; }//chi nhánh
        [JsonPropertyName("department")]
        public string department { get; set; }//phòng ban
        [JsonPropertyName("job_number")]
        public string job_number { get; set; }//mã nv
        [JsonPropertyName("title")]
        public string title { get; set; }//chức danh
        [JsonPropertyName("email")]
        public string email { get; set; }//email
        [JsonPropertyName("phone")]
        public string phone { get; set; }//phone

        [JsonPropertyName("timestamp")]
        public int timestamp { get; set; }//ngày

        [JsonPropertyName("attendancelist")]
        public List<AttendanceList> AttendanceList { get; set; }
        [JsonPropertyName("firstlogin")]
        public int firstlogin { get; set; }//giờ vào sớm nhất
        [JsonPropertyName("lastlogout")]
        public int lastlogout { get; set; }//giờ ra muộn nhất
        [JsonPropertyName("sumtime")]
        public int sumtime { get; set; }//tổng thời gian
                                        //giờ ra trừ giờ vào - 1h30
                                        //Trường hợp giờ ra < 11h30 hoặc giờ vào > 13h00: giờ ra – giờ vào

        [JsonPropertyName("absent")]
        public int absent { get; set; }//vắng
                                       //Vắng buổi sáng khi giờ vào >= 10h00
                                       //Vắng buổi chiều khi giờ ra <= 15h00
        [JsonPropertyName("missing")]
        public int missing{ get; set; }//Thiếu giờ vào/ra
                                       //Lấy tổng thời gian làm việc chuẩn là 8h – Tổng thời gian làm việc trong ngày
        [JsonPropertyName("camera_position")]
        public string camera_position { get; set; }//địa điểm chấm công
        [JsonPropertyName("date")]
        public int date { get; set; }
    }
    public class RepostSearchModel
    {
        [JsonPropertyName("name")]
        public string name { get; set; }
        [JsonPropertyName("department")]
        public string department { get; set; }
        [JsonPropertyName("description")]
        public string description { get; set; }
        [JsonPropertyName("todate")]
        public int? todate { get; set; }
        [JsonPropertyName("fromdate")]
        public int? fromdate { get; set; }
        [JsonPropertyName("start_number")]
        public int StartNumber { get; set; }
        [JsonPropertyName("page_size")]
        public int PageSize { get; set; }
    }
    public class AttendanceList
    {
        [JsonPropertyName("id")]
        public int? id { get; set; }
        [JsonPropertyName("subject_id")]
        public int subject_id { get; set; }
        [JsonPropertyName("earliest_record")]
        public int earliest_record { get; set; }
        [JsonPropertyName("latest_record")]
        public int latest_record { get; set; }
        [JsonPropertyName("date")]
        public DateTime? date { get; set; }
        [JsonPropertyName("clock_in")]
        public int clock_in { get; set; }
        [JsonPropertyName("clock_out")]
        public int clock_out { get; set; }
        [JsonPropertyName("worktime")]
        public int worktime { get; set; }
    }
    public class ListData
    {
        [JsonPropertyName("id")]
        public int id { get; set; }
        [JsonPropertyName("subject_id")]
        public int subject_id { get; set; }
        [JsonPropertyName("name")]
        public string name { get; set; }//họ tên nv
        [JsonPropertyName("description")]
        public string description { get; set; }//chi nhánh
        [JsonPropertyName("department")]
        public string department { get; set; }//phòng ban
        [JsonPropertyName("job_number")]
        public string job_number { get; set; }//mã nv
        [JsonPropertyName("title")]
        public string title { get; set; }//chức danh
        [JsonPropertyName("email")]
        public string email { get; set; }//email
        [JsonPropertyName("phone")]
        public string phone { get; set; }//phone
        [JsonPropertyName("timestamp")]
        public int timestamp { get; set; }//ngày

        [JsonPropertyName("firstlogin")]
        public int firstlogin { get; set; }//giờ vào sớm nhất
        [JsonPropertyName("lastlogout")]
        public int lastlogout { get; set; }//giờ ra muộn nhất
        [JsonPropertyName("sumtime")]
        public int sumtime { get; set; }//tổng thời gian
                                        //giờ ra trừ giờ vào - 1h30
                                        //Trường hợp giờ ra < 11h30 hoặc giờ vào > 13h00: giờ ra – giờ vào

        [JsonPropertyName("absent")]
        public int absent { get; set; }//vắng
                                       //Vắng buổi sáng khi giờ vào >= 10h00
                                       //Vắng buổi chiều khi giờ ra <= 15h00
        [JsonPropertyName("missing")]
        public int missing { get; set; }//Thiếu giờ vào/ra
                                        //Lấy tổng thời gian làm việc chuẩn là 8h – Tổng thời gian làm việc trong ngày
        [JsonPropertyName("camera_position")]
        public string camera_position { get; set; }//địa điểm chấm công
        [JsonPropertyName("date")]
        public int date { get; set; }
    }
}
