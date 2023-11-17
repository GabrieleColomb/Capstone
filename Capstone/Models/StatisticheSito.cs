namespace Capstone.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StatisticheSito")]
    public partial class StatisticheSito
    {
        [Key]
        public int IdStatistiche { get; set; }

        [Required]
        [StringLength(50)]
        public string Metriche { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataStatistiche { get; set; }
    }
}
