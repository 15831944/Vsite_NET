using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Civil.ApplicationServices;
using Autodesk.Civil.DatabaseServices;
using Autodesk.Civil.DatabaseServices.Styles;
using Autodesk.Civil.Settings;

using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;

// IExstensionApplication je interface koji se mora inicijalizirati
// Ulazna točka u aplikaciju
namespace Acad2020Plugin2
{
    public class Pokretanje : IExtensionApplication
    {
        public void Initialize()
        {
            //throw new NotImplementedException();
        }

        public void Terminate()
        {
            //throw new NotImplementedException();
        }

        // atribut kojeg pronalazi AutoCad
        [CommandMethod("PLOVNIPUT")]
        public void PokretanjeForme()
        {
            //inicijalizacija forme
            PlovniPutApp win = new PlovniPutApp();

            AcAp.ShowModalDialog(win);
        }
    }
}
