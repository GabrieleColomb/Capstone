namespace Capstone.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DettaglioOrdine")]
    public partial class DettaglioOrdine
    {
        [Key]
        public int IdDettaglioOrdine { get; set; }

        public int Quantita { get; set; }

        public decimal PrezzoUnitario { get; set; }

        public int GiocoId { get; set; }

        public int OrdineId { get; set; }

        public virtual Giochi Giochi { get; set; }

        public virtual Ordine Ordine { get; set; }
    }
}
