using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FahrzeugVerwaltung;
using FahrzeugVerwaltung.Service;
using Unity;

namespace FahrzeugVerwaltung.Setup
{
    internal class Init
    {
        public static IUnityContainer container { get; private set; }
    

    public static void Initialize()
        {
            container = new UnityContainer();

            
            container.RegisterType<IVehicleService, VehicleService>();
        }
    }
}
