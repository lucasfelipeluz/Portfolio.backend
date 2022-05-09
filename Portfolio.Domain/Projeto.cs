using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Domain
{
    public class Projeto
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Detalhes { get; set; }
        public string UrlWebSite { get; set; }
        public string UrlGitHub { get; set; }
        public string Tags { get; set; }
        public DateTime DataAtualizacaoProjeto { get; set; }

    }
}
