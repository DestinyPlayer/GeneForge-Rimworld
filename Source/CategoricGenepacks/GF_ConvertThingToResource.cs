using PipeSystem;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace CategoricGenepacks
{
    public class GF_ConvertThingToResource : PipeSystem.CompResource
    {
        private CompBreakdownable compBreakdownable;

        private CompPowerTrader compPowerTrader;

        private CompFlickable compFlickable;

        public new GF_CompProperties_ConvertThingToResource Props => (GF_CompProperties_ConvertThingToResource)props;

        public bool CanInputNow => (compBreakdownable == null || !compBreakdownable.BrokenDown) && (compPowerTrader == null || compPowerTrader.PowerOn) && (compFlickable == null || compFlickable.SwitchIsOn);

        public Thing HeldThing
        {
            
            get
            {
                List<Thing> list = parent.Map.thingGrid.ThingsListAt(parent.Position);
                List<ThingDef> catList = Props.catThing.childThingDefs;

                Log.Message("Grabbing category: " + catList.ToString());

                for (int i=0; i < catList.Count; i++)
                {
                    Log.Message("  Grabbing corpse: " + catList[i].ToString());
                    for (int j = 0; j < list.Count; j++)
                    {
                        Log.Message("    Testing for corpses: " + list[j].ToString());
                        if (list[j].def == catList[i])
                        {
                            Log.Message("      Success! Depositing " + list[j].ToString() + " into the extractor.");
                            return list[j];
                        }
                    }
                }
                Log.Message("Nothing's been found mate");
                return null;
            }
        }

        public int MaxCanInput => (int)PipeNet.AvailableCapacity;

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            compBreakdownable = parent.GetComp<CompBreakdownable>();
            compPowerTrader = parent.GetComp<CompPowerTrader>();
            compFlickable = parent.GetComp<CompFlickable>();
        }

        public override void CompTick()
        {
            base.CompTick();
            if (parent.IsHashIntervalTick(250))
            {
                CompTickRare();
            }
        }

        public override void CompTickRare()
        {
            base.CompTickRare();
            Thing heldThing = HeldThing;
            Log.Message("Blah blah testing if this bloody thing even loaded");
            if (CanInputNow && heldThing != null)
            {
                int num = heldThing.stackCount / Props.ratio;
                PipeNet.DistributeAmongStorage(num);
                heldThing.DeSpawn();
            }
        }
    }
}
