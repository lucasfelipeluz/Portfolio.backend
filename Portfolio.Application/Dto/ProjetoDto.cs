using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Dto
{
    public class ProjetoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} é obrigatório!")]
        public string Detalhes { get; set; }

        public string UrlWebSite { get; set; }
        public string UrlGitHub { get; set; }
        public string Tags { get; set; }
        public DateTime DataAtualizacaoProjeto { get; set; }
    }
}
