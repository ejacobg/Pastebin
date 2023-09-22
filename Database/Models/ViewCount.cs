using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Database.Models
{
    public class ViewCount
    {
        [Key]
        public int Id { get; set; }

        public int PasteId { get; set; }
        public Paste Paste { get; set; }

        public int Views { get; set; }
    }
}