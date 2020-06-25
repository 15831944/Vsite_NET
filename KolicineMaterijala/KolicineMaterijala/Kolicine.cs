using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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


namespace KolicineMaterijala
{
    class Kolicine
    {
        Document doc = AcAp.DocumentManager.MdiActiveDocument;
        Database db = AcAp.DocumentManager.MdiActiveDocument.Database;
        Editor ed = AcAp.DocumentManager.MdiActiveDocument.Editor;
        CivilDocument civDoc = CivilApplication.ActiveDocument;

        public BindingList<Corridor> corridors = new BindingList<Corridor>();
        public BindingList<double> stacionaze = new BindingList<double>();
        public BindingList<string> kodovi = new BindingList<string>();
        public BindingList<string> shapes = new BindingList<string>();

        Corridor _Corr;

        public Corridor Corr
        {
            get
            {
                return _Corr;
            }

            set
            {
                _Corr = value;
            }
        }
            
        string _Naziv;
        double _Stacionaza;

        public string Naziv
        {
            get
            {
                return _Naziv;
            }
            set
            {
                _Naziv = value;
            }
        }

        public double Stacionaza
        {
            get
            {
                return _Stacionaza;
            }
            set
            {
                _Stacionaza = value;
            }
        }

        
        

        public void PopunjavanjeListeCorridora()
        {
            using (Transaction ts = db.TransactionManager.StartTransaction())
            {
                foreach (ObjectId objId in civDoc.CorridorCollection)
                {
                    if (objId != null)
                    {
                        Corridor corr = ts.GetObject(objId, OpenMode.ForRead) as Corridor;
                        corridors.Add(corr);
                        
                    }
                }              
            }
        }

        public void PopunjavanjeListeStacionaza()
        {
            using (Transaction ts = db.TransactionManager.StartTransaction())
            {
                Baseline bl = corridors.SingleOrDefault(x => x.Name == _Naziv).Baselines[0] as Baseline;

                foreach (double stacionaza in bl.SortedStations())
                {
                    //stacionaze.Add(Math.Round(stacionaza, civDoc.Settings.DrawingSettings.AmbientSettings.Station.Precision.Value));
                    //zbog nekog rezloga ne radi iako bi po svemu trebalo !!!
                    stacionaze.Add(stacionaza);
                }
            }

        }

        public void PopunjavanjeListeKodova()
        {
            _Corr = corridors.SingleOrDefault(x => x.Name == _Naziv);
            string[] _kodovi = _Corr.GetLinkCodes();
            foreach (string kod in _kodovi)
            {
                kodovi.Add(kod);
            }
        }

        public void PopunjavanjeListeShape()
        {
            _Corr = corridors.SingleOrDefault(x => x.Name == _Naziv);
            string[] _shapes = _Corr.GetShapeCodes();
            foreach (string shape in _shapes)
            {
                shapes.Add(shape);
            }

        }

        public double DobivanjeDuljineLinka(string ime, double station)
        {
            double ukupnaDuljina = 0;
            Baseline bl = corridors.SingleOrDefault(x => x.Name == _Naziv).Baselines[0] as Baseline;
            AppliedAssembly applied = bl.GetAppliedAssemblyAtStation(station);
           

            CalculatedLinkCollection clLinks = applied.GetLinksByCode(ime);

            if (clLinks == null)
                return 0;

            foreach (CalculatedLink link in clLinks)
            {
                CalculatedPointCollection clPoints = link.CalculatedPoints;
                Point3d point1 = new Point3d(clPoints[0].StationOffsetElevationToBaseline.X, clPoints[0].StationOffsetElevationToBaseline.Y, clPoints[0].StationOffsetElevationToBaseline.Z);
                Point3d point2 = new Point3d(clPoints[1].StationOffsetElevationToBaseline.X, clPoints[1].StationOffsetElevationToBaseline.Y, clPoints[1].StationOffsetElevationToBaseline.Z);
                double udaljenost = point1.DistanceTo(point2);
                ukupnaDuljina = ukupnaDuljina + udaljenost;
            }

            return ukupnaDuljina;
        }

        public double DobivanjePovrsineShape(string ime, double station)
        {
            double ukupnaPovrsina = 0;
            Baseline bl = corridors.SingleOrDefault(x => x.Name == _Naziv).Baselines[0] as Baseline;
            AppliedAssembly applied = bl.GetAppliedAssemblyAtStation(station);
            CalculatedShapeCollection clShape = applied.GetShapesByCode(ime);

            if (clShape == null)
                return 0;

            foreach (CalculatedShape shape in clShape)
            {
                ukupnaPovrsina = ukupnaPovrsina + shape.Area;
            }

            return ukupnaPovrsina;
        }


    }
}
