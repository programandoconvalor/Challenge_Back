using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
namespace tekchoice.Data.Models
{
    public class CalculoModel
    {
        [Key]
        [JsonProperty("sexo")]
        public string sexo { get; set; }
        [Required]
        [JsonProperty("altura")]
        public string altura { get; set; }

        [JsonProperty("peso")]
        public string peso { get; set; }

        [Required]
        [JsonProperty("cintura")]
        public string cintura { get; set; }

        [Required]
        [JsonProperty("cuello")]
        public string cuello { get; set; }

        [JsonProperty("cadera")]
        public string cadera { get; set; }


    }
}
