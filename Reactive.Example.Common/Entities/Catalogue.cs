using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reactive.Example.Common.Entities
{
    [Table("catalogue")]
    public class Catalogue
    {
        public Catalogue(int x, int y)
        {
            X = x;
            Y = y;
            Timestamp = DateTime.UtcNow;
        }
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column("x_value")]
        public int X { get; set; }
        
        [Column("y_value")]
        public int Y { get; set; }
        
        [Column("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}