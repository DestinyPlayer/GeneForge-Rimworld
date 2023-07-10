using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace CategoricGenepacks
{
    [StaticConstructorOnStartup]
    internal class GF_Utility
    {
        //public static ThingDef myDef = DefDatabase<ThingDef>.GetNamed("GenepackSingle");
        public static List<GeneDef> allGeneDefs = new List<GeneDef>();
        public static List<GeneCategoryDef> allGeneCatDefs = new List<GeneCategoryDef>();
        public static List<string> allGeneCats = new List<string>();

        static GF_Utility()
        {
            GF_UtilityStartup();
            //Log.Message("This worked!");
        }

        public static ThingDef getGFPack(string name)
        {
            return DefDatabase<ThingDef>.GetNamed(name);
        }
        public static void GF_UtilityStartup()
        {
            allGeneDefs = DefDatabase<GeneDef>.AllDefsListForReading;
            allGeneCatDefs = DefDatabase<GeneCategoryDef>.AllDefs.ToList();

            foreach (var cat in allGeneCatDefs)
            {
                allGeneCats.Add(cat.defName);
                //Log.Message("Category - " + cat);
            }
        }
    }
    public class GF_Pack : Genepack
    {
        public string GeneCat = "";
        public int AllowArchite = 0;
        public override void PostMake()
        {
            SetCategory();
            base.PostMake();
            geneSet = new GeneSet();
            
            foreach (var def in GF_Utility.allGeneDefs.InRandomOrder())
            {
                //Log.Message(def.displayCategory.ToString());
                if (def.displayCategory.ToString().Contains(GeneCat) && def.biostatArc < 1+AllowArchite && def.canGenerateInGeneSet && def.selectionWeight>0)
                {
                    //Log.Message("    --Successfully found a Hemogen gene! It's " + def.ToString());
                    geneSet.AddGene(def);
                    break;
                }
                //Log.Message("Gene: " + def.ToString() + ", GeneClass: " + def.geneClass.ToString());
            }
            geneSet.GenerateName();
        }

        protected virtual void SetCategory()
        {
            Log.Message("This shouldn't have ran like this. Please check your code dumbass.");
        }
    }
    public class WorkGiver_PutPackInGenebank : WorkGiver_Scanner
    {
        //private ThingDef myDef = DefDatabase<ThingDef>.GetNamed("GenepackSingle");
        public override ThingRequest PotentialWorkThingRequest => ThingRequest.ForDef(GF_Utility.getGFPack("Genepack"));

        public override PathEndMode PathEndMode => PathEndMode.Touch;

        public override bool ShouldSkip(Pawn pawn, bool forced = false)
        {
            return !ModsConfig.BiotechActive;
        }

        public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            if (!ModLister.CheckBiotech("Genepack"))
            {
                return false;
            }

            if (t.IsForbidden(pawn) || !pawn.CanReserve(t, 1, -1, null, forced))
            {
                return false;
            }

            return FindGeneBank(pawn, t) != null;
        }

        public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
        {
            Thing thing = FindGeneBank(pawn, t);
            if (thing != null)
            {
                Job job = JobMaker.MakeJob(JobDefOf.CarryGenepackToContainer, t, thing, thing.InteractionCell);
                job.count = t.stackCount;
                return job;
            }

            return null;
        }

        private Thing FindGeneBank(Pawn pawn, Thing genepackThing)
        {
            Genepack genepack = genepackThing as Genepack;

            if (!genepack.AutoLoad)
            {
                return null;
            }

            if (genepack.targetContainer != null)
            {
                if (genepack.targetContainer.Map == genepack.Map)
                {
                    CompGenepackContainer compGenepackContainer = genepack.targetContainer.TryGetComp<CompGenepackContainer>();
                    if (compGenepackContainer != null && !compGenepackContainer.Full)
                    {
                        return genepack.targetContainer;
                    }
                }

                return null;
            }

            return GenClosest.ClosestThingReachable(genepack.Position, genepack.Map, ThingRequest.ForGroup(ThingRequestGroup.GenepackHolder), PathEndMode.InteractionCell, TraverseParms.For(pawn), 9999f, delegate (Thing x)
            {
                if (x.IsForbidden(pawn) || !pawn.CanReserve(x))
                {
                    return false;
                }

                CompGenepackContainer compGenepackContainer2 = x.TryGetComp<CompGenepackContainer>();
                if (compGenepackContainer2 == null || compGenepackContainer2.Full || !compGenepackContainer2.autoLoad)
                {
                    return false;
                }

                Thing targetContainer = genepack.targetContainer;
                return (targetContainer == null || targetContainer == compGenepackContainer2.parent) ? true : false;
            });
        }
    }
}
