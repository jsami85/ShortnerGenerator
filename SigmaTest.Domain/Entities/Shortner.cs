using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SigmaTest.Domain.Entities
{
    
    public class Shortner : BaseEntity
    {
		
		[Required]
		public string LongUrl { get; set; }

		[Required]
		public string ShortenedUrl { get; set; }

		[Required]
		[Column("added")]
		public DateTime CreationDate { get; set; }

		[Required]
		[StringLength(50)]
		public string Ip { get; set; }

		[Required]
		public int NumOfClicks { get; set; }
	}
}
