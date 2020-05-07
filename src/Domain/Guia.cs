using System;

namespace Domain
{
    public class Guia 
    {
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public int GuiaStatusId { get; set; }
        public GuiaStatus GuiaStatus { get; set; }
        public int GuiaTipoId { get; set; }
        public GuiaTipo GuiaTipo { get; set; }
        public int StatusCheckInId { get; set; }
        public StatusCheckIn StatusCheckIn { get; set; }
        public int IdGuiaExterno{ get; set; }
    }
}