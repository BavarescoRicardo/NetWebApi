using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetWebApi.Modelos
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Bio { get; set; }
        public Char? Sexo { get; set; }
        public string Historico { get; set; }
        public string Contato { get; set; }
        public string Observacoes { get; set; }
        public byte[] Foto { get; set; }
        public string ApelidoLogin { get; set; }
        [Required]
        [StringLength(20)]
        public string Senha { get; set; }
        public string token { get; set; }
        public int Permissao { get; set; }
    }
}
