using System;
using UnityEngine;
using Verse;

namespace RimWorld
{
    // Token: 0x020013E3 RID: 5091
    public class CompPropertiesSwapBuilding_GF : CompProperties
    {
        // Token: 0x06007DB0 RID: 32176 RVA: 0x002B33ED File Offset: 0x002B15ED
        public CompPropertiesSwapBuilding_GF()
        {
            this.compClass = typeof(CompSwapBuilding_GF);
        }

        // Token: 0x040046D2 RID: 18130
        public ThingDef buildingSwap;
        public string buttonName;
        public string buttonDescription;
        //public Texture2D uiIcon = BaseContent.BadTex;
        public string uiIconPath;
        public bool destroyOriginal = false;
    }
}
