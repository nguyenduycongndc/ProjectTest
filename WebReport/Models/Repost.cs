using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebReport.Models
{
    [Table("REPOST")]
    public class Repost
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [Column("name")]
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [Column("department")]
        [JsonPropertyName("department")]
        public string Department { get; set; }
        [Column("branch")]
        [JsonPropertyName("branch")]
        public string Branch { get; set; }
        [Column("date")]
        [JsonPropertyName("date")]
        public int date { get; set; }
    }
}
