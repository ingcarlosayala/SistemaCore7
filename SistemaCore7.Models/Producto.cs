using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCore7.Models
{
    public class Producto
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Codigo es requerido")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Descripcion es requerido")]
        [MaxLength(200)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Costo es requerido")]
        [Range(1,double.MaxValue)]
        public double Costo { get; set; }

        [Required(ErrorMessage = "Precio es requerido")]
        [Range(1, double.MaxValue)]
        public double Precio { get; set; }

        [Required(ErrorMessage = "Estado es requerido")]
        public bool Estado { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string ImagenUrl { get; set; }

        [Required(ErrorMessage = "Categoria es requerida")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }

        [Required(ErrorMessage = "Marca es requerida")]
        [Display(Name = "Marca")]
        public int MarcaId { get; set; }

        [ForeignKey("MarcaId")]
        [Display(Name = "Marca")]
        public Marca Marca { get; set; }
    }
}
