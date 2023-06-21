using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;

namespace CategoricGenepacks
{
    public class GF_AG_MechanitorGenepack : GF_Pack
    {
        protected override void SetCategory()
        {
            GeneCat = "AG_Mechanitor";
        }
    }
    public class GF_AG_PutMechanitorInGeneBank : WorkGiver_PutPackInGenebank
    {
        public override ThingRequest PotentialWorkThingRequest => ThingRequest.ForDef(GF_Utility.getGFPack("GF_AG_MechanitorGenepack"));
    }
}
