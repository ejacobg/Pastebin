using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Database.Models
{
    [Index(nameof(Shortlink), IsUnique = true)]
    public class Paste
    {
        [Key]
        public int PasteId { get; set; }

        // Apply length constraints?
        public string Shortlink { get; set; }
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        public int Expires { get; set; } // Expiration in minutes after creation.
    }
}