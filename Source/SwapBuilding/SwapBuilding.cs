using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace RimWorld
{
    // Token: 0x020013E4 RID: 5092
    public class CompSwapBuilding_GF : ThingComp
    {
        // Token: 0x170015C2 RID: 5570
        // (get) Token: 0x06007DB1 RID: 32177 RVA: 0x002B3405 File Offset: 0x002B1605
        private CompPropertiesSwapBuilding_GF Props
        {
            get
            {
                return (CompPropertiesSwapBuilding_GF)this.props;
            }
        }
        //public bool ParentIsInactive
        //{
        //    get
        //    {
        //        Building_MechUpgrader building_Upgrader = this.parent as Building_MechUpgrader;
        //        if (building_Upgrader != null && building_Upgrader.State == SubcoreScannerState.Inactive)
        //        {
        //            return true;
        //        }
        //        CompPawnSpawnOnWakeup compPawnSpawnOnWakeup = this.parent.TryGetComp<CompPawnSpawnOnWakeup>();
        //        return compPawnSpawnOnWakeup != null && !compPawnSpawnOnWakeup.CanSpawn;
        //    }
        //}
        public bool PowerOn => parent.TryGetComp<CompPowerTrader>().PowerOn;
        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            if (this.PowerOn)
            {
                yield return new Command_Action
                {
                    defaultLabel = this.Props.buttonName,
                    defaultDesc = this.Props.buttonDescription,
                    icon = ContentFinder<Texture2D>.Get(this.Props.uiIconPath, true),//ContentFinder<Texture2D>.Get("UI/Gizmos/BMU_AllowCombatMechs", true),//this.Props.uiIcon,
                    action = delegate ()
                    {
                        Thing thing2 = ThingMaker.MakeThing(this.Props.buildingSwap, null);
                        Thing thing = GenSpawn.Spawn(thing2, parent.Position, parent.Map, parent.Rotation, WipeMode.Vanish, false);
                        //Thing thing = GenSpawn.Spawn(this.Props.buildingSwap, parent.Position, parent.Map, parent.GetRotStage, WipeMode.Vanish, false);
                        //  thing.Rotation = parent.Rotation;
                        thing.HitPoints = parent.HitPoints;
                        if (this.Props.destroyOriginal)
                        {
                            parent.Destroy();
                        }

                        thing.SetFaction(Faction.OfPlayer);
                        // if (parent.def == this.Props.buildingSwap)
                        // {
                        // Thing thing = GenSpawn.Spawn(this.Props.buildingSwap, parent.Position, parent.Map);
                        // thing.HitPoints = parent.HitPoints;
                        //   parent.Destroy();
                        // }
                    }
                };
            }

        }

    }
}
