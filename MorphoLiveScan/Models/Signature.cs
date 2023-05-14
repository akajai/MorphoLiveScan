using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MorphoLiveScan.Models
{
    public class Signature
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "longblob")]
        public byte[] Image { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
