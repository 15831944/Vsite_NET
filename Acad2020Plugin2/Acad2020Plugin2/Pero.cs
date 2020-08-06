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
using System.Windows.Forms;

using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace Acad2020Plugin2
{
    public class Pero
    {
        // Obavezno za pristup aplikaciji
        Document doc = AcAp.DocumentManager.MdiActiveDocument;
        Database db = AcAp.DocumentManager.MdiActiveDocument.Database;
        Editor ed = AcAp.DocumentManager.MdiActiveDocument.Editor;
        CivilDocument civDoc = CivilApplication.ActiveDocument;

        //fields
        double duljinaPera;
        double sirinaKrune;
        double stacionaza;
        string kodUglavlje;
        string kodZaglavlje;
        string strana;
        string kodGlave;
        

        // Properties
        // Mogao sam dio fields inicijalizirati u constructoru
        // Ali sam ostavio propertie
        #region properties
        public double DuljinaPera
        {
            get
            {
                return  duljinaPera;
            }
            set
            {
                duljinaPera = value;
            }
        }

        public double SirinaKrune
        {
            get
            {
                return sirinaKrune;
            }
            set
            {
                sirinaKrune = value;
            }
        }

        public double Stacionaza
        {
            get
            {
                return stacionaza;
            }

            set
            {
                stacionaza = value;
            }
        }

        public string Strana
        {
            get
            {
                return strana;
            }

            set
            {
                strana = value;
            }
        }

        public string KodUzglavlje
        {
            get
            {
                return kodUglavlje;
            }

            set
            {
                if(value == "Lijevo")
                {
                    kodUglavlje = "UglavljeLijevo";
                }
                else if(value == "Desno")
                {
                    kodUglavlje = "UglavljeDesno";
                }
                else
                {
                    ed.WriteMessage("\nNe postoji valjan kod");
                    kodUglavlje = null;
                }
            }
        }

        public string KodZaglavlje
        {
            get
            {
                return kodZaglavlje;
            }

            set
            {
                if (value == "Lijevo")
                {
                    kodZaglavlje = "PocetakTijelaPeraL";
                }
                else if (value == "Desno")
                {
                    kodZaglavlje = "PocetakTijelaPeraD";
                }
                else
                {
                    ed.WriteMessage("\nNe postoji valjan kod");
                    kodZaglavlje = null;
                }
            }
        }

        public string KodGlave
        {
            get
            {
                return kodGlave;
            }

            set
            {
                if (value == "Lijevo")
                {
                    kodGlave = "GlavaPeraLijevo";
                }
                else if (value == "Desno")
                {
                    kodGlave = "GlavaPeraDesno";
                }
                else
                {
                    ed.WriteMessage("\nNe postoji valjan kod");
                    kodUglavlje = null;
                }
            }
        }



        #endregion

        // metoda za definiranje tocaka konstrukcije
        // Vraca structure Point3d koji se primjenjuje 
        // za izradu ostalih objecata classe: alignment, profile i corridor

        // metoda za odabir pravilnog corridora
        // želimo je ostaviti private tako da se ograniči pristup
        // krucijalno je imati pravilan corridor za funkcioniranje aplikacije
        // moguća danja nadogradnja da traži subassembly sa određenim imenom
        // potrebna danja rasprava
       
        private Corridor PlovniPutMetoda()
        {
            foreach (ObjectId objId in civDoc.CorridorCollection)
            {
                Corridor corr = objId.GetObject(OpenMode.ForRead) as Corridor;
                if (corr.Name == "PlovniPut")
                    return corr;

            
            }

            return null;
        }
        
        protected Point3d TockaNaStacionazi(string kodTocke)
        {
            using(Transaction tr = doc.TransactionManager.StartTransaction())
            {
                Corridor corr = PlovniPutMetoda();
                Baseline bl = corr.Baselines[0] as Baseline;

                bl.UpdateStation(stacionaza);
                AppliedAssembly appliedassy = bl.GetAppliedAssemblyAtStation(stacionaza);

                CalculatedPointCollection ptsbycode = appliedassy.GetPointsByCode(kodTocke);

                Point3d pt3 = ptsbycode[0].StationOffsetElevationToBaseline;
                Point3d ptWorld = bl.StationOffsetElevationToXYZ(pt3);

                tr.Commit();
                return ptWorld;
            }
            
        }
       
    }
}
