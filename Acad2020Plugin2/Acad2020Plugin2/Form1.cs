using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

namespace Acad2020Plugin2
{
    public partial class PlovniPutApp : Form
    {
        Document doc = AcAp.DocumentManager.MdiActiveDocument;
        public Database db = AcAp.DocumentManager.MdiActiveDocument.Database;
        public Editor ed = AcAp.DocumentManager.MdiActiveDocument.Editor;
        private OkomitoPero com;
        
        public PlovniPutApp()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                com = new OkomitoPero();

                PostaviStacionazu();

                com.Strana = fStrana.Text;
                com.KodGlave = fStrana.Text;
                com.KodUzglavlje = fStrana.Text;
                com.KodZaglavlje = fStrana.Text;
                com.SirinaKrune = 4.0;

                Alignment aligmentMent = com.IzradaAlignmenta();
               
                Profile profileFile = com.IzradaProfila(aligmentMent);
                Corridor corriDori = com.IzradaPera(aligmentMent, profileFile);
                
                ed.WriteMessage("Izrađena je corridor na " + com.Stacionaza.ToString());
                tr.Commit();
                
                
            }
                
                        
        }

        private void PostaviStacionazu()
        {
            ed.WriteMessage("Odaberite vrijednost stacionaže");
            com.Stacionaza = (double)fStacionaze.Value;
        }
    }
}
