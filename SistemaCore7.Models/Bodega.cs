using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCore7.Models
{
    public class Bodega
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Bodega es requerida")]
        [Display(Name = "Bodega")]
        [MaxLength(60)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Descripcion es requerida")]
        [MaxLength(200)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Estado es requerido")]
        public bool Estado { get; set; }
    }
}
