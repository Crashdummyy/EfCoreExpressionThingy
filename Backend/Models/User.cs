namespace Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    ///     Der User repräsentiert einen AdministrationUser und
    ///     setzt sich zum Teil aus AD-Daten zusammen
    /// </summary>
    [Table("User", Schema = "Administration")]
    public class User : IEntity<int>
    {
        /// <inheritdoc />
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Windows LoginName (AD) des Benutzers
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Login { get; set; }

        /// <summary>
        ///     Vorname des Benutzers
        /// </summary>
        [Column("Vorname")]
        [StringLength(50)]
        public string FirstName { get; set; }

        /// <summary>
        ///     Nachname des Benutzers
        /// </summary>
        [Column("Nachname")]
        [StringLength(50)]
        public string LastName { get; set; }

        /// <summary>
        ///     Multivac-Mail-Adresse des Benutzers
        /// </summary>
        [StringLength(50)]
        public string Email { get; set; }

        /// <summary>
        ///     ForeignKey auf [Administration].[Location] 
        /// </summary>
        public int? LocationId { get; set; }

        /// <summary>
        ///     TenantGroupd des AD und AAD
        /// </summary>
        [Column("ADAbteilung")]
        [StringLength(250)]
        public string ActiveDirectoryGroup { get; set; }

        /// <summary>
        ///     NavigationProperty auf die Lokation
        /// </summary>
        public virtual Location Location { get; set; }

        /// <inheritdoc />
        public override string ToString() => $"{LastName}, {FirstName} ({Login} : {Id})";
    }
}