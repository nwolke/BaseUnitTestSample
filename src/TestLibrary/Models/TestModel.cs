using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestLibrary.Models
{
    /// <summary>
    /// simple model
    /// </summary>
    public class TestModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("company")]
        public string Company { get; set; }
    }
}
