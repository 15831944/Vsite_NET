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
using Autodesk.Civil.DatabaseServices;
using AcAp = Autodesk.AutoCAD.ApplicationServices.Application;
using System.Windows.Forms;
using Autodesk.Civil.ApplicationServices;
using System.ComponentModel;
using Autodesk.Aec.Geometry;
using Autodesk.Aec.DatabaseServices;
using ObjectIdCollection = Autodesk.AutoCAD.DatabaseServices.ObjectIdCollection;
using Entity = Autodesk.Civil.DatabaseServices.Entity;
using ObjectId = Autodesk.AutoCAD.DatabaseServices.ObjectId;
using System.Diagnostics;

namespace ExportAlignment
{
    class Align
    {
        private Document doc = AcAp.DocumentManager.MdiActiveDocument;
        private Database db = AcAp.DocumentManager.MdiActiveDocument.Database;
        private Editor ed = AcAp.DocumentManager.MdiActiveDocument.Editor;

        ProfileEntity oneEntity;
        
        public Alignment selectedALig;
        public Alignment newAlig;
        public BindingList<Autodesk.Civil.DatabaseServices.Profile> profiles = new BindingList<Autodesk.Civil.DatabaseServices.Profile>();

        

        public string alignmentName { get; set; }
        public string profileName { get; set; }

        public void SelectAlignment()
        {
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                try
                {
                    PromptEntityOptions prEntOpts = new PromptEntityOptions("\nPlease select exsisting alignment: ");
                    prEntOpts.AllowNone = true;
                    prEntOpts.SetRejectMessage("\n Selected object must be an alignment type");
                    prEntOpts.AddAllowedClass(typeof(Alignment), true);

                    PromptEntityResult prEntRes = ed.GetEntity(prEntOpts);
                    if (prEntRes.Status != PromptStatus.OK)
                    {
                        MessageBox.Show("Please repeat selection", "Info", MessageBoxButtons.OK);
                    }

                    else
                    {
                        Alignment alig = tr.GetObject(prEntRes.ObjectId, OpenMode.ForRead) as Alignment;
                        ed.WriteMessage("\n You selected alignment: " + alig.Name);
                        
                        selectedALig = alig;
                        ObjectIdCollection profIds = selectedALig.GetProfileIds();
                        foreach (ObjectId id in profIds)
                        {
                            Autodesk.Civil.DatabaseServices.Profile prof = tr.GetObject(id, OpenMode.ForRead) as Autodesk.Civil.DatabaseServices.Profile;
                            profiles.Add(prof);
                        }

                        tr.Commit();
                    }
                }

                catch (System.Exception ex)
                {
                    ed.WriteMessage("Error encountered" + ex.Message);
                    tr.Abort();
                    selectedALig = null;
                }
            }
        }

        public void CreateNewAlignment(double station1 , double station2)
        {
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                ObjectId aligStyleId = CivilApplication.ActiveDocument.Styles.AlignmentStyles["Basic"];
                ObjectId newAligIdTemp = Alignment.CreateOffsetAlignment("Temp", selectedALig.ObjectId, 1, aligStyleId, station1, station2);
                ObjectId newAligId = Alignment.CreateOffsetAlignment(alignmentName, newAligIdTemp, -1, aligStyleId);
                Entity item = tr.GetObject(newAligIdTemp, OpenMode.ForWrite) as Entity;
                item.Erase();
                
                
                newAlig = tr.GetObject(newAligId, OpenMode.ForWrite) as Alignment;

                CreateProfil(station1, station2);

                tr.Commit();
            }

            
        }

        private void CreateProfileFromEntities(ProfileEntity pentity, Autodesk.Civil.DatabaseServices.Profile Eprofile,
            Autodesk.Civil.DatabaseServices.Profile Nprofile, Point2d startPoint, Point2d endPoint)
        {
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {                
                switch (pentity.EntityType)
                {
                    case ProfileEntityType.Tangent:
                        ProfileTangent pTangent = Nprofile.Entities.AddFixedTangent(startPoint, endPoint);
                        break;

                    case ProfileEntityType.Circular:
                        ProfileCircular pCircular = pentity as ProfileCircular;
                        ProfilePVI pPVI = Eprofile.PVIs.GetPVIAt(pCircular.PVIStation, pCircular.PVIElevation);
                        double circleLength = pCircular.Length;
                        ProfileCircular pCircularNew = Nprofile.Entities.AddFreeCircularCurveByPVIAndLength(pPVI, circleLength);
                        break;

                    case ProfileEntityType.ParabolaSymmetric:
                        ProfileParabolaSymmetric pParabolaSymetric = pentity as ProfileParabolaSymmetric;
                        double parabolaRadius = pParabolaSymetric.Radius;
                        VerticalCurveType vcType = pParabolaSymetric.CurveType;
                        ProfileParabolaSymmetric pParabolaSymetricNew = Nprofile.Entities.AddFixedSymmetricParabolaByTwoPointsAndRadius(startPoint, endPoint, vcType, parabolaRadius);
                        break;

                    case ProfileEntityType.ParabolaAsymmetric:
                        ProfileParabolaAsymmetric pParabolaAsymmetric = pentity as ProfileParabolaAsymmetric;
                        ProfilePVI paPVI = Eprofile.PVIs.GetPVIAt(pParabolaAsymmetric.PVIStation, pParabolaAsymmetric.PVIElevation);
                        double pLength1 = pParabolaAsymmetric.AsymmetricLength1;
                        double pLength2 = pParabolaAsymmetric.AsymmetricLength2;
                        ProfileParabolaAsymmetric pParabolaAsymmetricNew = Nprofile.Entities.AddFreeAsymmetricParabolaByPVIAndLengths(paPVI, pLength1, pLength2);
                        break;
                    case ProfileEntityType.None:
                        ProfileTangent nTangent = Nprofile.Entities.AddFixedTangent(startPoint, endPoint);
                        break;                        
                }

                tr.Commit();
            }
           
        }

        
        private void CreateProfil(double station1, double station2)
        {           
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                // gets the existing profile
                Autodesk.Civil.DatabaseServices.Profile profile = profiles.SingleOrDefault(x => x.Name == profileName);
                ProfilePVICollection pPviCollExist = profile.PVIs;

                ObjectId labelSetId = CivilApplication.ActiveDocument.Styles.LabelSetStyles.ProfileLabelSetStyles["_No Labels"];
                ObjectId profileStyleId = CivilApplication.ActiveDocument.Styles.ProfileStyles["Design Profile"];
                ObjectId oProfileId = Autodesk.Civil.DatabaseServices.Profile.CreateByLayout("Copied profile", newAlig.ObjectId, newAlig.LayerId, profileStyleId, labelSetId);
                Autodesk.Civil.DatabaseServices.Profile newProfile = tr.GetObject(oProfileId, OpenMode.ForWrite) as Autodesk.Civil.DatabaseServices.Profile;

                if (pPviCollExist.Count <= 3)
                {

                    Point2d point1 = new Point2d(station1, profile.ElevationAt(station1));
                    Point2d point2 = new Point2d(station2, profile.ElevationAt(station2));

                    CreateProfileFromEntities(profile.Entities[0], profile, newProfile, point1, point2);

                    tr.Commit();
                }
                else
                {
                    // finding values near our start station for copied alignment
                    ProfilePVI pPviExistStart = pPviCollExist.GetPVIAt(station1, profile.ElevationAt(station1));
                    ProfilePVI pPviExistEnd = pPviCollExist.GetPVIAt(station2, profile.ElevationAt(station2));
                                                   
                    ProfileEntity pEntitiyAfter = profile.Entities.EntityAtId(pPviExistStart.EntityAfter);
                    Point2d pviEndPoint = new Point2d(pEntitiyAfter.StartStation, pEntitiyAfter.StartElevation);

                    ProfileEntity pEntitiyBefore = profile.Entities.EntityAtId(pPviExistEnd.EntityBefore);
                    Point2d pviStartPoint = new Point2d(pEntitiyBefore.EndStation, pEntitiyBefore.EndElevation);
                   
                    

                    ProfileEntity pStartEntity = pPviExistStart.VerticalCurve;
                    ProfileEntity pEndEntity = pPviExistEnd.VerticalCurve;

                    ProfileEntityCollection pEntities = profile.Entities;
                    uint index = pEntities.FirstEntity;

                    Point2d firstPoint = new Point2d(station1, profile.ElevationAt(station1));
                    CreateProfileFromEntities(pStartEntity, profile, newProfile, firstPoint, pviEndPoint);

                    int counter = 0;

                    try
                    {
                        while (true)
                        {
                            ProfileEntity pEntity = pEntities.EntityAtId(index);

                            if (pEntity.StartStation >= station1 && pEntity.EndStation <= station2)
                            {
                                Point2d StartPoint = new Point2d(pEntity.StartStation, pEntity.StartElevation);
                                Point2d EndPoint = new Point2d(pEntity.EndStation, pEntity.EndElevation);
                                CreateProfileFromEntities(pEntity, profile, newProfile, StartPoint, EndPoint);
                                counter++;
                            }

                            if (pEntity.StartStation <= station1 && pEntity.EndStation >= station2)
                            {
                                oneEntity = pEntity;
                            }

                            index = pEntity.EntityAfter;
                        }
                    }

                    catch
                    {
                        if (counter == 0)
                        {
                            Point2d point1 = new Point2d(station1, profile.ElevationAt(station1));
                            Point2d point2 = new Point2d(station2, profile.ElevationAt(station2));
                            CreateProfileFromEntities(oneEntity, profile, newProfile, point1, point2);

                        }

                        else
                        {
                            Point2d endPoint = new Point2d(station2, profile.ElevationAt(station2));
                            CreateProfileFromEntities(pEndEntity, profile, newProfile, pviStartPoint, endPoint);
                        }
                    }

                    tr.Commit();
                }
            }                     
        }
    }
}
