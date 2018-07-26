using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(255)]
        public string Name { get; set; }
   
        public DateTime ReleaseDate { get; set; }
        
        public DateTime DateAdded { get; set; }
        
        public byte  NumberInStock { get; set; }
  
        public Genres Genre { get; set; }
        
        [Required]
        public byte GenreId { get; set; }
        public byte NumberAvailable { get; set; }

    }


}