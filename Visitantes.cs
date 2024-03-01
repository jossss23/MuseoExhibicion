using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MuseoExhibicion
{
    public class Visitantes : INotifyPropertyChanged
    {
        public int VisitantesActuales { get; private set; }
        public int VisitantesTotales { get; private set; }
        public bool MaximaCapacidad => VisitantesActuales >= 10;

        public Visitantes()
        {
            LeerVisitantes();
        }
        public void RegistrarIngreso()
        {
            if (!MaximaCapacidad)
            {
                VisitantesActuales++;
                VisitantesTotales++;
                GuardarVisitantes();
            }
        }
        public void RegistrarSalida()
        {
            if (VisitantesActuales > 0)
            {
                VisitantesActuales--;
            }
        }
        private void GuardarVisitantes()
        {
            File.WriteAllText("visitantes.json", JsonSerializer.Serialize(this));
        }

        private void LeerVisitantes()
        {
            if (File.Exists("visitantes.json"))
            {
                var info = JsonSerializer.Deserialize<Visitantes>(File.ReadAllText("visitantes.json"));
                if (info != null)
                {
                    this.VisitantesTotales = info.VisitantesTotales;
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
