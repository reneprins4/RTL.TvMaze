using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RTL.TvMazeApp.Domain.Models
{
    public sealed class Show
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }
        [JsonProperty("id")]
        public int ShowId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public IEnumerable<Person> Cast { get; set; }
    }
}
