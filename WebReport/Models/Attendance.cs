using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebReport.Models
{
    [Table("attendance")]
    public class Attendance
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("company_id")]
        public int? company_id { get; set; }

        [JsonPropertyName("subject_id")]
        public int? subject_id { get; set; }

        [ForeignKey("subject_id")]
        public Subject Subject { get; set; }

        [JsonPropertyName("date")]
        [Column(TypeName = "date")]
        public DateTime? date { get; set; }

        [JsonPropertyName("earliest_record")]
        public int? earliest_record { get; set; }

        [JsonPropertyName("latest_record")]
        public int? latest_record { get; set; }

        [JsonPropertyName("clock_in")]
        public int? clock_in { get; set; }

        [JsonPropertyName("clock_out")]
        public int? clock_out { get; set; }

        [JsonPropertyName("worktime")]
        public int? worktime { get; set; }
    }
}
