using System;

namespace Backend.Dtos
{
    public class PutGuiaDto 
    {
        public int IdGuiaExterno{ get; set; }
        public GuiaStatusDto GuiaStatus { get; set; }
        public StatusCheckInDto StatusCheckIn { get; set; }
        public bool GuiaCancelada { get; set; }
        public string NumeroLote { get; set; }
    }
}
