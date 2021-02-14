using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleAppTest.Models
{
    [Table("summary")]
    public class Summary
    {
        [Key()]
        public int Id { get; set; }

        [Required()]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required()]
        public long Count { get; set; }
    }
}
