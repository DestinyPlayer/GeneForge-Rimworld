using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;
using Verse.AI;
using static CategoricGenepacks.GF_Utility;

namespace CategoricGenepacks
{
    public class GF_ArchiteGenepack : Genepack
    {
        public override void PostMake()
        {
            base.PostMake();
            geneSet = new GeneSet();

            var allGeneDefs = DefDatabase<GeneDef>.AllDefsListForReading;
            foreach (var def in allGeneDefs.InRandomOrder())
            {
                if (def.biostatArc > 0)
                {
                    geneSet.AddGene(def);
                    break;
                }
            }
            geneSet.GenerateName();
        }
    }
    public class GF_PutArchiteInGeneBank : WorkGiver_PutPackInGenebank
    {
        public override ThingRequest PotentialWorkThingRequest => ThingRequest.ForDef(GF_Utility.getGFPack("GF_ArchiteGenepack"));
    }
}
