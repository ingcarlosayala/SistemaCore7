using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SistemaCore7.Models
{
    public class Marca
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Marca es requerida")]
        [Display(Name = "Categoria")]
        [MaxLength(60)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Estado es requerido")]
        public bool Estado { get; set; }
    }
}
