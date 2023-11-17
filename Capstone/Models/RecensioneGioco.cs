namespace Capstone.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RecensioneGioco")]
    public partial class RecensioneGioco
    {
        [Key]
        public int IdRecensione { get; set; }

        public int Valutazione { get; set; }

        [Required]
        public string TestoDellaRecensione { get; set; }

        public int GiocoId { get; set; }

        public int UtenteId { get; set; }

        public virtual Giochi Giochi { get; set; }

        public virtual Utenti Utenti { get; set; }
    }
}
