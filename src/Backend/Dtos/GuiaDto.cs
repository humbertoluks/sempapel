using System;

namespace Backend.Dtos
{
    public class GuiaDto 
    {
        public int Id { get; set; }
        public string PrestadorId { get; set; }
        public int UnidadeId { get; set; }
        public string PushId { get; set; }
        public string TokenId { get; set; }
        public string Numero { get; set; }
        public string Beneficiario { get; set; }
        public string BeneficiarioCartao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string GuiaXML { get; set; }
        public GuiaStatusDto GuiaStatus { get; set; }
        public GuiaTipoDto GuiaTipo { get; set; }
        public StatusCheckInDto StatusCheckIn { get; set; }
        public int IdGuiaExterno{ get; set; }
        public bool GuiaCancelada { get; set; }
        public string NumeroLote { get; set; }
    }
}
