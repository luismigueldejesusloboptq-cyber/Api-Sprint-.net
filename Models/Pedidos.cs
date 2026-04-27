
using System;
using System.Collections.Generic;
using Api_Lanchonete_Sprint.DTOs;
using System.Globalization;

namespace Api_Lanchonete_Sprint.Models
{
    public class Pedidos
    {
        public int IdPedido { get; set; }
        public DateTime DataPedido { get; set; } = DateTime.Now;
        public String Nome { get; set; } = string.Empty;
        public int NumeroMesa { get; set; }

        //relações//

        public virtual ICollection<ItensPedido> ItensPedido { get; set; } = new List<ItensPedido>();
    }
}
