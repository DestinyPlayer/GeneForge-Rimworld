using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace CategoricGenepacks
{
    public class GF_HemoGenepack : Genepack
    {
        public override void PostMake()
        {
            base.PostMake();
            geneSet = new GeneSet();

            var allGeneDefs = DefDatabase<GeneDef>.AllDefsListForReading;
            foreach (var def in allGeneDefs.InRandomOrder())
            {
                //Log.Message(def.displayCategory.ToString());
                if (def.displayCategory.ToString().Contains("Hemogen") && def.biostatArc < 1)
                {
                    //Log.Message("    --Successfully found a Hemogen gene! It's " + def.ToString());
                    geneSet.AddGene(def);
                    break;
                }
                //Log.Message("Gene: " + def.ToString() + ", GeneClass: " + def.geneClass.ToString());
            }
            geneSet.GenerateName();
        }
    }
}
