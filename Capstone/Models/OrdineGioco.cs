namespace Capstone.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrdineGioco")]
    public partial class OrdineGioco
    {
        [Key]
        public int IdOrdineGioco { get; set; }

        public int GiocoId { get; set; }

        public int UtenteId { get; set; }

        public int OrdineId { get; set; }

        public virtual Giochi Giochi { get; set; }

        public virtual Utenti Utenti { get; set; }
    }
}
