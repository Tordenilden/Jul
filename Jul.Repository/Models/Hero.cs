using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jul.Repository.Models
{
    /// <summary>
    /// This is used for Entity Framework 6.0++
    /// EF => can be used with different approaches
    /// "Code-first", "Datamodeling", "DatabaseFirst"
    /// 1) public int Id { get; set; } this is the PK , HeroId, Heroid...
    /// </summary>
    public class Hero
    {
        [Key] // DataAnnotation / Attribute
        public int Id { get; set; } // PK className+Id 
        public string Name { get; set; } = string.Empty;
        public string RealName { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(40)")]
        public string Place { get; set; } = string.Empty;
        public DateTime DebutYear { get; set; }

        

    }
}
