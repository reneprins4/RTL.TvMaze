using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json;

namespace RTL.TvMazeApp.Domain.Models
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }
        [JsonProperty("id")]
        public int PersonId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }

        [JsonIgnore]
        public int ShowId { get; set; }

        [JsonIgnore]
        public Show Show { get; set; }
    }
}
