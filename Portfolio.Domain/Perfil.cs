using System;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Domain
{
    public class Perfil
    {
        [Key]
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
