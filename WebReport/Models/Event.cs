using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebReport.Models
{
    [Table("event")]
    public class Event
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

        [JsonPropertyName("screen_id")]
        public int? screen_id { get; set; }

        [JsonPropertyName("photo")]
        [Column(TypeName = "varchar")]
        [StringLength(256)]
        public string photo { get; set; }

        [JsonPropertyName("age")]
        public double? age { get; set; }

        [JsonPropertyName("gender")]
        public double? gender { get; set; }//float

        [JsonPropertyName("group")]
        public int? group { get; set; }

        [JsonPropertyName("short_group")]
        public int? short_group { get; set; }

        [JsonPropertyName("quality")]
        public double? quality { get; set; }

        [JsonPropertyName("confidence")]
        public double? confidence { get; set; }

        [JsonPropertyName("event_type")]
        public int event_type { get; set; }

        [JsonPropertyName("subject_type")]
        public short? subject_type { get; set; }//smallint?

        [JsonPropertyName("real_name")]
        [Column(TypeName = "varchar")]
        [StringLength(64)]
        public string real_name { get; set; }

        [JsonPropertyName("pinyin")]
        [Column(TypeName = "varchar")]
        [StringLength(612)]
        public string pinyin { get; set; }

        [JsonPropertyName("subject_photo")]
        [Column(TypeName = "varchar")]
        [StringLength(256)]
        public string subject_photo { get; set; }//varchar

        [JsonPropertyName("timestamp")]
        public int? timestamp { get; set; }

        [JsonPropertyName("fmp")]
        public double? fmp { get; set; }

        [JsonPropertyName("fmp_error")]
        public byte? fmp_error { get; set; }//tinyint?

        [JsonPropertyName("camera_position")]
        [Column(TypeName = "varchar")]
        [StringLength(128)]
        public string camera_position { get; set; }

        [JsonPropertyName("uuid")]
        [Column(TypeName = "varchar")]
        [StringLength(64)]
        public string uuid { get; set; }

        [JsonPropertyName("pass_type")]
        public int? pass_type { get; set; }

        [JsonPropertyName("verification_mode")]
        public int? verification_mode { get; set; }

        [JsonPropertyName("is_entrance")]
        public short? is_entrance { get; set; }

        [JsonPropertyName("temperature")]
        public decimal? temperature { get; set; }

        [JsonPropertyName("temperature_type")]
        public int? temperature_type { get; set; }

        [JsonPropertyName("mask_type")]
        public int? mask_type { get; set; }
    }
}
