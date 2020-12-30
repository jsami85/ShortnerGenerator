using System.ComponentModel.DataAnnotations;

namespace SigmaTest.Domain
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
