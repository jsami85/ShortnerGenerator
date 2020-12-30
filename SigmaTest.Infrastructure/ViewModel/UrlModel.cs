using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SigmaTest.Infrastructure.ViewModel
{
    public class UrlModel
    {
        [Required]
        [JsonProperty("url")]
        public string Url { get; set; }

    }
}
