using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Models
{
    public class Pelicula
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string MovieName { get; set; }

        public string Director { get; set; }

        [Required]
        public int CategoryId  { get; set; }

        [ForeignKey("CategoryId")]
        public Categoria Categoria { get; set; }
    }
}
