using System;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Domain
{
    public class Conhecimento
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Informacao { get; set; }
        public int AnosDeExperiencia { get; set; }
        public int MesesDeExperiencia { get; set; }
        public string Icone { get; set; }
        public string Cor { get; set; }
        public DateTime DataAtualizacaoConhecimento { get; set; }

    }
}
