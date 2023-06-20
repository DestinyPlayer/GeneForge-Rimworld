using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorld
{
    public class GF_ArchiteGenepack : Genepack
    {
        public bool canBeArchite = true;
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
}
