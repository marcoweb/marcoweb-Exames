using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcoCarvalho.Exames.Data.Entities
{
    public class Exame
    {
        public int Id { get; set; }
        public int IdPaciente { get; set; }
        public int Afericao { get; set; }
        public DateTime DataHora { get; set; }

        public string Data { get { return DataHora.ToString("dd/MM/yyyy"); } }
        public string Hora { get { return DataHora.ToString("HH:mm"); } }
    }
}
