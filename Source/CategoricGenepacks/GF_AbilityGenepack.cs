using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace CategoricGenepacks
{
    public class GF_AbilityGenepack : GF_Pack
    {
        public new string GeneCat = "Ability";
    }
    public class GF_PutAbilityInGeneBank : WorkGiver_PutPackInGenebank
    {
        public override ThingRequest PotentialWorkThingRequest => ThingRequest.ForDef(GF_Utility.getGFPack("GF_AbilityGenepack"));
    }
}
