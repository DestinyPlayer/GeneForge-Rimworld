using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;

namespace CategoricGenepacks
{
    public class GF_HemoGenepack : GF_Pack
    {
        protected override void SetCategory()
        {
            GeneCat = "Hemogen";
        }
    }
    public class GF_PutHemoInGeneBank : WorkGiver_PutPackInGenebank
    {
        public override ThingRequest PotentialWorkThingRequest => ThingRequest.ForDef(GF_Utility.getGFPack("GF_HemoGenepack"));
    }
}
