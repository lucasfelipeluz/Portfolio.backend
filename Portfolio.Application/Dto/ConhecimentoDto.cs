using System;

namespace Portfolio.Application.Dto
{
    public class ConhecimentoDto
    {
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
