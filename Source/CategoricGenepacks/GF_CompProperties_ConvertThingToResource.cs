using PipeSystem;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace CategoricGenepacks
{
    public class GF_CompProperties_ConvertThingToResource : PipeSystem.CompProperties_Resource
    {
        public int ratio = 1;

        public ThingCategoryDef catThing;

        public GF_CompProperties_ConvertThingToResource()
        {
            compClass = typeof(GF_ConvertThingToResource);
        }

        public override IEnumerable<string> ConfigErrors(ThingDef parentDef)
        {
            foreach (string item in base.ConfigErrors(parentDef))
            {
                yield return item;
            }

            if (parentDef.thingClass != typeof(Building_Storage))
            {
                yield return "Can't use GF_CompProperties_ConvertThingToResource with a thing that don't have Building_Storage as thingClass.";
            }

            if (parentDef.comps.FindAll((CompProperties c) => c is CompProperties_ConvertThingToResource).Count > 1)
            {
                yield return "Can't use multiple GF_CompProperties_ConvertThingToResource on the same thing.";
            }

            if (catThing == null)
            {
                yield return "Can't use GF_CompProperties_ConvertThingToResource with a null thing.";
            }
        }
    }
}
