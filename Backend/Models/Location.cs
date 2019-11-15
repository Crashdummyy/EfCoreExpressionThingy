using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Models
{
    /// <summary>
    ///     Tabelle: [Cad.Core.Administration].[Location]
    /// </summary>
    [SuppressMessage("ReSharper",
        "VirtualMemberCallInConstructor")]
    [Table("Location", Schema = "Administration")]
    public class Location : IEntity<int>
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <summary>
        ///     Bezeichnung der Lokation
        /// </summary>
        [Column("Bezeichnung")]
        [StringLength(50)]
        [Required]
        public string Label { get; set; }

        /// <summary>
        ///     Werk der Lokation
        /// </summary>
        [Column("Werk")]
        [StringLength(50)]
        [Required]
        public string Plant { get; set; }

        /// <inheritdoc />
        public override string ToString() => $"{Plant} - {Label}";
    }
}