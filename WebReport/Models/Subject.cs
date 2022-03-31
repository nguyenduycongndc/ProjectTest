using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebReport.Models
{
    [Table("subject")]
    public class Subject
    {
        public Subject()
        {
            this.Event = new HashSet<Event>();
            this.Attendance = new HashSet<Attendance>();
        }
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("company_id")]
        public int? company_id { get; set; }

        
        [JsonPropertyName("subject_type")]
        public short subject_type { get; set; }//smallint

        [JsonPropertyName("create_time")]
        public int? create_time { get; set; }

        [JsonPropertyName("update_time")]
        public long? update_time { get; set; }
        
        [JsonPropertyName("email")]
        [Column(TypeName = "varchar")]
        [StringLength(64)]
        public string email { get; set; }

        [JsonPropertyName("password_hash")]
        [Column(TypeName = "varchar")]
        [StringLength(256)]
        public string password_hash { get; set; }

        [JsonPropertyName("password_reseted")]
        public byte? password_reseted { get; set; }//tinyint

        [Required]
        [JsonPropertyName("real_name")]
        [Column(TypeName = "varchar")]
        [StringLength(64)]
        public string real_name { get; set; }

        [Required]
        [JsonPropertyName("pinyin")]
        [StringLength(512)]
        [Column(TypeName = "varchar")]
        public string pinyin { get; set; }

        [JsonPropertyName("gender")]
        public short? gender { get; set; }

        [JsonPropertyName("phone")]
        [Column(TypeName = "varchar")]
        [StringLength(20)]
        public string phone { get; set; }

        [JsonPropertyName("avatar")]
        [Column(TypeName = "varchar")]
        [StringLength(256)]
        public string avatar { get; set; }

        [JsonPropertyName("department")]
        [Column(TypeName = "varchar")]
        [StringLength(256)]
        public string department { get; set; }

        [JsonPropertyName("department_pinyin")]
        [Column(TypeName = "varchar")]
        [StringLength(512)]
        public string department_pinyin { get; set; }

        [JsonPropertyName("title")]
        [Column(TypeName = "varchar")]
        [StringLength(64)]
        public string title { get; set; }

        [JsonPropertyName("description")]
        [Column(TypeName = "varchar")]
        [StringLength(128)]
        public string description { get; set; }

        [JsonPropertyName("mobile_os")]
        public int? mobile_os { get; set; }

        [JsonPropertyName("birthday")]
        [Column(TypeName = "date")]
        public DateTime? birthday { get; set; }

        [JsonPropertyName("entry_date")]
        [Column(TypeName = "date")]
        public DateTime? entry_date { get; set; }

        [JsonPropertyName("job_number")]
        [Column(TypeName = "varchar")]
        [StringLength(64)]
        public string job_number { get; set; }

        [JsonPropertyName("remark")]
        [Column(TypeName = "varchar")]
        [StringLength(128)]
        public string remark { get; set; }

        [JsonPropertyName("purpose")]
        public int? purpose { get; set; }

        [JsonPropertyName("interviewee")]
        [Column(TypeName = "varchar")]
        [StringLength(20)]
        public string interviewee { get; set; }

        [JsonPropertyName("interviewee_pinyin")]
        [Column(TypeName = "varchar")]
        [StringLength(512)]
        public string interviewee_pinyin { get; set; }

        [JsonPropertyName("come_from")]
        [Column(TypeName = "varchar")]
        [StringLength(128)]
        public string come_from { get; set; }

        [JsonPropertyName("inviter_id")]
        public int? inviter_id { get; set; }

        [JsonPropertyName("visited")]
        public byte? visited { get; set; }

        [JsonPropertyName("visit_notify")]
        public byte? visit_notify { get; set; }

        [JsonPropertyName("start_time")]
        public int? start_time { get; set; }

        [JsonPropertyName("end_time")]
        public int? end_time { get; set; }

        [JsonPropertyName("extra_id")]
        [Column(TypeName = "varchar")]
        [StringLength(64)]
        public string extra_id { get; set; }

        [JsonPropertyName("wg_number")]
        [Column(TypeName = "varchar")]
        [StringLength(64)]
        public string wg_number { get; set; }

        [JsonPropertyName("is_use")]
        public byte? is_use { get; set; }

        [JsonPropertyName("people_type")]
        public int? people_type { get; set; }

        [JsonPropertyName("credential_type")]
        public int? credential_type { get; set; }

        [JsonPropertyName("credential_no")]
        [Column(TypeName = "varchar")]
        [StringLength(64)]
        public string credential_no { get; set; }

        [JsonPropertyName("nation")]
        public int? nation { get; set; }

        [JsonPropertyName("origin")]
        [Column(TypeName = "varchar")]
        [StringLength(64)]
        public string origin { get; set; }

        [JsonPropertyName("domicile_province_code")]
        public long? domicile_province_code { get; set; }//bigint

        [JsonPropertyName("domicile_city_code")]
        public long? domicile_city_code { get; set; }

        [JsonPropertyName("domicile_district_code")]
        public long? domicile_district_code { get; set; }

        [JsonPropertyName("domicile_street_code")]
        public long? domicile_street_code { get; set; }

        [JsonPropertyName("domicile_address")]
        [Column(TypeName = "varchar")]
        [StringLength(256)]
        public string domicile_address { get; set; }

        [JsonPropertyName("residence_province_code")]
        public long? residence_province_code { get; set; }

        [JsonPropertyName("residence_city_code")]
        public long? residence_city_code { get; set; }

        [JsonPropertyName("residence_district_code")]
        public long? residence_district_code { get; set; }

        [JsonPropertyName("residence_street_code")]
        public long? residence_street_code { get; set; }

        [JsonPropertyName("residence_address")]
        [Column(TypeName = "varchar")]
        [StringLength(256)]
        public string residence_address { get; set; }

        [JsonPropertyName("education_code")]
        public int? education_code { get; set; }

        [JsonPropertyName("marital_status_code")]
        public int? marital_status_code { get; set; }

        [JsonPropertyName("nationality_code")]
        [Column(TypeName = "varchar")]
        [StringLength(4)]
        public string nationality_code { get; set; }

        [JsonPropertyName("source")]
        public int? source { get; set; }

        [JsonPropertyName("village")]
        [Column(TypeName = "varchar")]
        [StringLength(64)]
        public string village { get; set; }

        [JsonPropertyName("building")]
        [Column(TypeName = "varchar")]
        [StringLength(64)]
        public string building { get; set; }

        [JsonPropertyName("house")]
        [Column(TypeName = "varchar")]
        [StringLength(64)]
        public string house { get; set; }

        [JsonPropertyName("house_rel_code")]
        public int? house_rel_code { get; set; }

        [JsonPropertyName("entrance_people_type")]
        public int? entrance_people_type { get; set; }

        public virtual ICollection<Event> Event { get; set; }
        public virtual ICollection<Attendance> Attendance { get; set; }
    }
}
