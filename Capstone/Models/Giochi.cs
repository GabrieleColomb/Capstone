namespace Capstone.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    [Table("Giochi")]
    public partial class Giochi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Giochi()
        {
            DettaglioOrdine = new HashSet<DettaglioOrdine>();
            OrdineGioco = new HashSet<OrdineGioco>();
            RecensioneGioco = new HashSet<RecensioneGioco>();
        }

        [Key]
        public int IdGioco { get; set; }

        [Required]
        public string TitoloDelGioco { get; set; }

        [Required]
        public string Descrizione { get; set; }

        public decimal Prezzo { get; set; }

        [Required]
        [StringLength(50)]
        public string StatoDelGioco { get; set; }

        public DateTime? DataDiInserimento { get; set; }

        public string Immagine { get; set; }

        [NotMapped]
        public HttpPostedFileBase FotoGioco { get; set; }

        public int UtenteId { get; set; }

        public int CategoriaId { get; set; }

        public virtual CategoriaGiochi CategoriaGiochi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DettaglioOrdine> DettaglioOrdine { get; set; }

        public virtual Utenti Utenti { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdineGioco> OrdineGioco { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RecensioneGioco> RecensioneGioco { get; set; }
    }
}
