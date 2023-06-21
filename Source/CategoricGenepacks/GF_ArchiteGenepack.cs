using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

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
}
