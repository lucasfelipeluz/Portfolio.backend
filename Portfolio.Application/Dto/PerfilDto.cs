using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Application.Dto
{
    public class PerfilDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Cargo { get; set; }
        public string UrlInstagram { get; set; }
        public string UrlTelegram { get; set; }
        public string UrlEmail { get; set; }
        public string UrlLinkedin { get; set; }
        public DateTime DataAtualizacaoPerfil { get; set; }
    }
}
