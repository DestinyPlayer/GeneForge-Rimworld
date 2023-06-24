using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace CategoricGenepacks
{
//Ability
    public class GF_AbilityGenepack : GF_Pack
    {
        public new string GeneCat = "Ability";
    }
    public class GF_PutAbilityInGeneBank : WorkGiver_PutPackInGenebank
    {
        public override ThingRequest PotentialWorkThingRequest => ThingRequest.ForDef(GF_Utility.getGFPack("GF_AbilityGenepack"));
    }
//Hemogen
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
//Alpha Genes Mechanitor
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
//Warhammer 40k Genes and Psycasts
    public class GF_BEWH_WarhammerGenepack : GF_Pack
    {
        public new string GeneCat = "BEWH";
        public new int AllowArchite = 100;
    }
    public class GF_BEWH_PutWarhammerInGeneBank : WorkGiver_PutPackInGenebank
    {
        public override ThingRequest PotentialWorkThingRequest => ThingRequest.ForDef(GF_Utility.getGFPack("GF_BEWH_WarhammerGenepack"));
    }
}