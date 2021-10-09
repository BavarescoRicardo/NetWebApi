using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetWebApi.Modelos
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
        public double Quantidade { get; set; }
        public double Valor { get; set; }
        public int Opcao { get; set; }

    }
}
