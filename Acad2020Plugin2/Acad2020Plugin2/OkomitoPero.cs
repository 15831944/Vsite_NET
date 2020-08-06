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
    public class OkomitoPero : Pero
    {
        // Obavezno za pristup aplikaciji
        Document doc = AcAp.DocumentManager.MdiActiveDocument;
        Database db = AcAp.DocumentManager.MdiActiveDocument.Database;
        Editor ed = AcAp.DocumentManager.MdiActiveDocument.Editor;
        CivilDocument civDoc = CivilApplication.ActiveDocument;

        // svaki put kada se izradi objekat ++
        static int brojPera = 0;
        
        // konstruktor klase
        public OkomitoPero()
        {
            brojPera++;
        }
        public Corridor PlovniPut;
        // override abstractne metode 
        // prilagodena za okomita pera
        // izrada polyline objecta iz kojeg
        // će biti izrađen alignment i profile
        private Polyline ElementiAlignmenta()
        {
            // definiranje pointova za izradu konstrukcije
            if ((KodUzglavlje != null) && (KodZaglavlje != null) && (KodGlave != null))
            {
                Point3d prviPoint = TockaNaStacionazi(KodUzglavlje);
                Point3d drugiPoint = TockaNaStacionazi(KodZaglavlje);
                Point3d _prviPoint = new Point3d(prviPoint.X, prviPoint.Y, 0);
                Point3d _drugiPoint = new Point3d(drugiPoint.X, drugiPoint.Y, 0);

                //definiranje desnog dijela konstrukcije
                Line pomocnaLinijaD = new Line(_prviPoint, _drugiPoint);
                DBObjectCollection dbLineD = pomocnaLinijaD.GetOffsetCurves(SirinaKrune / 2);
                Line krunadesno = (Line)dbLineD[0];


                Point3d UglavljePeraD = new Point3d(krunadesno.StartPoint.X, krunadesno.StartPoint.Y, prviPoint.Z);
                Point3d ZaglavljePeraD = new Point3d(krunadesno.EndPoint.X, krunadesno.EndPoint.Y, drugiPoint.Z);

                //definiranje lijevg dijela konstrukcije
                Line pomocnaLinijaL = new Line(_prviPoint, _drugiPoint);
                DBObjectCollection dbLineL = pomocnaLinijaL.GetOffsetCurves(-SirinaKrune / 2);
                Line krunalijevo = (Line)dbLineL[0];

                Point3d UglavljePeraL = new Point3d(krunalijevo.StartPoint.X, krunalijevo.StartPoint.Y, prviPoint.Z);
                Point3d ZaglavljePeraL = new Point3d(krunalijevo.EndPoint.X, krunalijevo.EndPoint.Y, drugiPoint.Z);

                Point3d GlavaPera = TockaNaStacionazi(KodGlave);

                // pretvorba 3d tocaka u 2d tocke desne strane
                Point2d UglavljePeraD2 = new Point2d(UglavljePeraD.X, UglavljePeraD.Y);
                Point2d ZaglavljePeraD2 = new Point2d(ZaglavljePeraD.X, ZaglavljePeraD.Y);

                // pretvorba 3d tocaka u 2d tocke lijeve strane
                Point2d UglavljePeraL2 = new Point2d(UglavljePeraL.X, UglavljePeraL.Y);
                Point2d ZaglavljePeraL2 = new Point2d(ZaglavljePeraL.X, ZaglavljePeraL.Y);

                // pretvorba 3d pointa glave u 2d point
                Point2d GlavaPera2 = new Point2d(GlavaPera.X, GlavaPera.Y);

                // izračun kuteva za konstrukciju polukruznice
                double kut1 = ZaglavljePeraL2.GetVectorTo(GlavaPera2).Angle;
                double kut2 = GlavaPera2.GetVectorTo(ZaglavljePeraD2).Angle;
                double poluKrug = Math.Tan((kut2 - kut1) / 2.0);

                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    BlockTableRecord curSpace = tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;

                    Polyline pLine = new Polyline();
                    
                    pLine.AddVertexAt(0, UglavljePeraL2, 0, 0, 0);
                    pLine.AddVertexAt(1, ZaglavljePeraL2, poluKrug, 0, 0);
                    pLine.AddVertexAt(2, ZaglavljePeraD2, 0, 0, 0);
                    pLine.AddVertexAt(3, UglavljePeraD2, 0, 0, 0);

                    pLine.TransformBy(ed.CurrentUserCoordinateSystem);
                    curSpace.AppendEntity(pLine);
                    tr.AddNewlyCreatedDBObject(pLine, true);
                        
                    
                    tr.Commit();

                    return pLine;
                    
                }
                
            }

            else
                return null;
        }

        

        public Alignment IzradaAlignmenta()
        {
            Polyline poly = ElementiAlignmenta();
            if (poly != null)
            {
                
                PolylineOptions pOpts = new PolylineOptions();
                pOpts.AddCurvesBetweenTangents = false;
                pOpts.EraseExistingEntities = false;
                pOpts.PlineId = poly.ObjectId;
                

                string ime = "Okomito pero " + brojPera.ToString();

                ObjectId peroAlignmentID = Alignment.Create(CivilApplication.ActiveDocument, pOpts, ime, null, "0", "Basic", "_No Labels");

                using (Transaction tr = db.TransactionManager.StartTransaction())
                {
                    Alignment peroAlignment = tr.GetObject(peroAlignmentID, OpenMode.ForWrite) as Alignment;
                    if (Strana == "Desno")
                        try
                        {
                            peroAlignment.Reverse();
                        }
                        catch (System.Exception ex)
                        {
                            ed.WriteMessage("Alignment reverse nije uspio" + ex.Message);
                            throw;
                        }


                    tr.Commit();
                    return peroAlignment;
                }
                 
            }
            else
            {
                return null;
            }
        }
        // definira visinsko vođenje objekta
        // postoji mogućnost poboljšanja
        public Profile IzradaProfila(Alignment aligIn)
        {
            
            if (aligIn != null)
            {
                
                string ime = "Niveleta okomitog pera " + brojPera.ToString();
                using (Transaction ts = AcAp.DocumentManager.MdiActiveDocument.Database.TransactionManager.StartTransaction())
                {
                    Alignment alig = aligIn;
                    
                    // postavke profila
                    ObjectId layerProfila = alig.LayerId;
                    ObjectId styleProfila = civDoc.Styles.ProfileStyles["Basic"];
                    ObjectId labelSetId = civDoc.Styles.LabelSetStyles.ProfileLabelSetStyles["_No Labels"];

                    // stvaranje praznog profila
                    ObjectId ProfilPeraId = Profile.CreateByLayout(ime, alig.Id, layerProfila, styleProfila, labelSetId);

                    // dodavanje elemenata u profil
                    Profile ProfilPera = ts.GetObject(ProfilPeraId, OpenMode.ForRead) as Profile;

                    // definiranje pointova
                    Point3d prviPoint = TockaNaStacionazi(KodUzglavlje);
                    Point3d drugiPoint = TockaNaStacionazi(KodZaglavlje);
                    Point3d _prviPoint = new Point3d(prviPoint.X, prviPoint.Y, 0);
                    Point3d _drugiPoint = new Point3d(drugiPoint.X, drugiPoint.Y, 0);
                    Point3d GlavaPera = TockaNaStacionazi(KodGlave);

                    // određivanje duljine iz polyline-a
                    ObjectId pomocniPolyId = alig.GetPolyline();
                    Polyline pomocniPoly = ts.GetObject(pomocniPolyId, OpenMode.ForRead) as Polyline;
                    double ukupnaDuljina = pomocniPoly.Length;
                    double pocetnaDuljina = _prviPoint.DistanceTo(_drugiPoint);
                    DuljinaPera = pocetnaDuljina;
                    double duljinaLuka = ukupnaDuljina - 2 * pocetnaDuljina;

                    // izrada pointova i dobivanje profila (mislim da se može brže) 
                    // dodatno istražiti
                    // for petaljom ?
                    Point2d profilePoint1 = new Point2d(alig.StartingStation, prviPoint.Z);
                    Point2d profilePoint2 = new Point2d(alig.StartingStation + DuljinaPera, drugiPoint.Z);

                    ProfileTangent Tangenta1 = ProfilPera.Entities.AddFixedTangent(profilePoint1, profilePoint2);

                    Point2d profilePoint3 = new Point2d(Tangenta1.EndStation, Tangenta1.EndElevation);
                    Point2d profilePoint4 = new Point2d(Tangenta1.EndStation + duljinaLuka/2, GlavaPera.Z);

                    ProfileTangent Tangenta2 = ProfilPera.Entities.AddFixedTangent(profilePoint3, profilePoint4);

                    Point2d profilePoint5 = new Point2d(Tangenta2.EndStation, Tangenta2.EndElevation);
                    Point2d profilePoint6 = new Point2d(Tangenta2.EndStation + duljinaLuka/2, drugiPoint.Z);

                    ProfileTangent Tangenta3 = ProfilPera.Entities.AddFixedTangent(profilePoint5, profilePoint6);

                    Point2d profilePoint7 = new Point2d(Tangenta3.EndStation, Tangenta3.EndElevation);
                    Point2d profilePoint8 = new Point2d(alig.EndingStation, prviPoint.Z);

                    ProfileTangent Tangenta4 = ProfilPera.Entities.AddFixedTangent(profilePoint7,profilePoint8);

                    ts.Commit();

                    return ProfilPera;
                    
                }
            }

            else
            {
                return null;
            }
            
        }

        // dobivanje assemblija glave pera
        // TO DO -> dodati izbor selekcije
        // BIG TO-DO -> isprogramirati assemblije u c#
        private Assembly GlavaPera()
        {
            using (Transaction tr = doc.TransactionManager.StartTransaction())
            {
                foreach (ObjectId assemblyId in civDoc.AssemblyCollection)
                {
                    Assembly AssemblyH = assemblyId.GetObject(OpenMode.ForWrite) as Assembly;
                    if (AssemblyH.Name == "GlavaPera" && Strana == "Desno")
                        return AssemblyH;
                    if (AssemblyH.Name == "GlavaPeraLijevo" && Strana == "Lijevo")
                        return AssemblyH;  
                }
            }

            return null;
        }
        // dobivanje assemblija tijela pera
        // TO DO -> dodati izbor selekcije 
        // BIG TO-DO -> isprogramirati assemblije u c#
        private Assembly TijeloPera()
        {
            using (Transaction tr = doc.TransactionManager.StartTransaction())
            {
                foreach (ObjectId assemblyId in civDoc.AssemblyCollection)
                {
                    Assembly AssemblyH = assemblyId.GetObject(OpenMode.ForRead) as Assembly;
                    if ((AssemblyH.Name == "TijeloRavnogPera") && (Strana == "Desno"))
                        return AssemblyH;
                    if ((AssemblyH.Name == "TijeloRavnogPera2") && (Strana == "Lijevo"))
                        return AssemblyH;
                }
            }

            return null;
        }

        private Assembly NultiAss()
        {
            using (Transaction tr = doc.TransactionManager.StartTransaction())
            {
                foreach (ObjectId assemblyId in civDoc.AssemblyCollection)
                {
                    Assembly AssemblyZero = assemblyId.GetObject(OpenMode.ForRead) as Assembly;
                    if (AssemblyZero.Name == "0") 
                        return AssemblyZero;
                }
            }

            return null;
        }

        
        private Autodesk.Civil.DatabaseServices.Surface EGSurface()
        {
            ObjectIdCollection SurfaceIds = civDoc.GetSurfaceIds();
            foreach (ObjectId surfaceId in SurfaceIds)
            {
                Autodesk.Civil.DatabaseServices.Surface CivSurface = surfaceId.GetObject(OpenMode.ForRead) as Autodesk.Civil.DatabaseServices.Surface;
                if (CivSurface.Name == "EG")
                    return CivSurface;
            }

            return null;
        }

        public Corridor IzradaPera(Alignment alig, Profile profi)
        {
            if (alig != null && profi != null)
            {
                Transaction tr = doc.TransactionManager.StartTransaction();
                string ime = "Okomito pero " + brojPera.ToString();

                Alignment os = alig;
                ObjectId osId = os.ObjectId;

                Profile niveleta = profi;
                ObjectId niveletaId = niveleta.ObjectId;

                Assembly tijeloPera = TijeloPera();
                ObjectId tijeloPeraId = tijeloPera.ObjectId;

                Assembly zerro = NultiAss();
                ObjectId zerroId = zerro.ObjectId;

                Assembly glavaPera = GlavaPera();
                ObjectId glavaPeraId = glavaPera.ObjectId;

                Autodesk.Civil.DatabaseServices.Surface targetSurface = EGSurface();
                ObjectId targetSurfaceId = targetSurface.ObjectId;

                ObjectId novoPeroId = civDoc.CorridorCollection.Add(ime, "Niveleta " + ime, osId, niveletaId);
                Corridor novoPero = tr.GetObject(novoPeroId, OpenMode.ForWrite) as Corridor;

                BaselineRegionCollection blRegColl = novoPero.Baselines[0].BaselineRegions;
                BaselineRegion regTijeloPera = blRegColl.Add("Tijelo pera", tijeloPeraId, alig.StartingStation, DuljinaPera);
                BaselineRegion regGlavaPera = blRegColl.Add("GlavaPera", glavaPeraId, DuljinaPera, DuljinaPera + Math.PI * SirinaKrune / 2);
                BaselineRegion reg0 = blRegColl.Add("0", zerroId, regGlavaPera.EndStation, alig.EndingStation);

                // corridor frequency nije otvoren u API dokumentaciji
                // jedini način da malo progustim izradu
                for (int i = 10; i < DuljinaPera; i+=10)
                {
                    regTijeloPera.AddStation(i, "Stacionaza " + i.ToString());
                }

                for (double i = 0.1; i < (Math.PI * SirinaKrune/2); i+=0.3)
                {
                    regGlavaPera.AddStation(DuljinaPera + i, "Stacionaza " + i.ToString());
                }

                BaselineRegionCollection blRColl = novoPero.Baselines[0].BaselineRegions;
                foreach (BaselineRegion blReg in blRColl)
                {
                    SubassemblyTargetInfoCollection targets = blReg.GetTargets();
                    foreach (SubassemblyTargetInfo target in targets)
                    {
                        if (target.TargetType == SubassemblyLogicalNameType.Surface)
                        {
                            var ids = new ObjectIdCollection();
                            ids.Add(targetSurfaceId);
                            target.TargetIds = ids;
                        }
                    }

                    blReg.SetTargets(targets);
                }

                novoPero.Rebuild();

                tr.Commit();

                return novoPero;
            }

            else
            {
                ed.WriteMessage($"Na stacionazi {0} nije moguća izgradnja pera", Stacionaza);
                return null;
            }
                     
        }
    }
}
