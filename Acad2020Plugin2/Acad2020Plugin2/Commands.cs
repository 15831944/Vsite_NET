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
    public class Commands 
    {
        private Document doc = AcAp.DocumentManager.MdiActiveDocument;
        private Database db = AcAp.DocumentManager.MdiActiveDocument.Database;
        private Editor ed = AcAp.DocumentManager.MdiActiveDocument.Editor;
        private double stacionaza = 900.00;
        public double Stacionaza
        {
            get
            {
                return stacionaza;
            }

            set
            {
                this.stacionaza = value;
            }
        }
        

        
        public void IzradiPoint()
        {
            Corridor corr = OdabraniCorridor();
            Point3d ptDraw = PocetnaTockaNaStacionaziD(corr);

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {

                BlockTable acBlkTbl;
                acBlkTbl = tr.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;

                BlockTableRecord acBlkTblRec;
                acBlkTblRec = tr.GetObject(acBlkTbl[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;

                DBPoint acPoint = new DBPoint(ptDraw);


                acBlkTblRec.AppendEntity(acPoint);
                tr.AddNewlyCreatedDBObject(acPoint, true);

                db.Pdmode = 34;
                db.Pdsize = 10;

                tr.Commit();
            }
        }

        public Point3d PocetnaTockaNaStacionaziD(Corridor plovniPut)
        {
            Corridor corr = plovniPut;
            Baseline bl = corr.Baselines[0];
            

            AppliedAssembly appliedassy = bl.GetAppliedAssemblyAtStation(stacionaza);
            CalculatedPointCollection pts = appliedassy.Points;


            CalculatedPointCollection ptsbycode = appliedassy.GetPointsByCode("UglavljeDesno");
            Point3d pt3 = ptsbycode[0].StationOffsetElevationToBaseline;
            Point3d ptWorld = bl.StationOffsetElevationToXYZ(pt3);

           
            return ptWorld;
        }

        public Corridor OdabraniCorridor()
        {
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    PromptEntityOptions prEntOpts = new PromptEntityOptions("\nOdaberite corridor plovnog puta: ");
                    prEntOpts.AllowNone = true;
                    prEntOpts.SetRejectMessage("\nOdabrani objekat mora biti corridor");
                    prEntOpts.AddAllowedClass(typeof(Corridor), true);

                    PromptEntityResult prEntRes = ed.GetEntity(prEntOpts);
                    if (prEntRes.Status != PromptStatus.OK)
                    {
                        return null;
                    }
                    else
                    {
                        Corridor cor = tr.GetObject(prEntRes.ObjectId, OpenMode.ForRead) as Corridor;
                        ed.WriteMessage("\n Odabrali ste corridor imena: " + cor.Name);
                        tr.Commit();
                        return cor;
                    }



                }
                catch (System.Exception ex)
                {

                    ed.WriteMessage("Error encountered: " + ex.Message);
                    tr.Abort();
                    return null;
                }
            }

        }

        public Alignment OdabraniAlignment()
        {
            using (var tr = db.TransactionManager.StartTransaction()) 
            {
                try 
                {
                    PromptEntityOptions prEntOpts = new PromptEntityOptions("\nOdaberite allignment plovnog puta:");
                    prEntOpts.AllowNone = true;
                    prEntOpts.SetRejectMessage("\nOdabrani objekat mora biti corridor");
                    prEntOpts.AddAllowedClass(typeof(Alignment), true);

                    PromptEntityResult prEntRes = ed.GetEntity(prEntOpts);
                    if (prEntRes.Status != PromptStatus.OK)
                        return null;
                    else
                    {
                        Alignment al = tr.GetObject(prEntRes.ObjectId, OpenMode.ForRead) as Alignment;
                        ed.WriteMessage("\n Odabrali ste alignment imena: " + al.Name);
                        tr.Commit();
                        return al;
                    }
                }

                catch (System.Exception ex)
                {
                    ed.WriteMessage("Error enountered: " + ex.Message);
                    tr.Abort();
                    return null;
                }
            }
        }
    }
}