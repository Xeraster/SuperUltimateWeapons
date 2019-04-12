using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace UltimateWeaponsMod
{
    class Projectile_BadCatSwarm : Bullet
    {
        #region Properties
        //
        public Thingdef_BadCatSwarm Def
        {
            get
            {
                return this.def as Thingdef_BadCatSwarm;
            }
        }
        #endregion Properties
        #region Overrides
        protected override void Impact(Thing hitThing)
        {
            base.Impact(hitThing);

            //this is the part about the thingdef array
            /*ThingDef[] thingArray = new ThingDef[35];
            int j = 0;
            thingArray[j] = ThingDefOf.Beer; j++;
            thingArray[j] = ThingDefOf.BlocksGranite; j++;
            thingArray[j] = ThingDefOf.Chemfuel; j++;
            thingArray[j] = ThingDefOf.Chocolate; j++;
            thingArray[j] = ThingDefOf.ChunkSlagSteel; j++;
            thingArray[j] = ThingDefOf.Cloth; j++;
            thingArray[j] = ThingDefOf.ElephantTusk; j++;
            thingArray[j] = ThingDefOf.Hay; j++;
            thingArray[j] = ThingDefOf.Heart; j++;
            thingArray[j] = ThingDefOf.Gold; j++;
            thingArray[j] = ThingDefOf.Granite; j++;
            thingArray[j] = ThingDefOf.Hyperweave; j++;
            thingArray[j] = ThingDefOf.InsectJelly; j++;
            thingArray[j] = ThingDefOf.Kibble; j++;
            thingArray[j] = ThingDefOf.Leather_Plain; j++;
            thingArray[j] = ThingDefOf.Luciferium; j++;
            thingArray[j] = ThingDefOf.MealFine; j++;
            thingArray[j] = ThingDefOf.MealNutrientPaste; j++;
            thingArray[j] = ThingDefOf.MealSimple; j++;
            thingArray[j] = ThingDefOf.MealSurvivalPack; j++;
            thingArray[j] = ThingDefOf.Meat_Human; j++;
            thingArray[j] = ThingDefOf.MedicineHerbal; j++;

            thingArray[j] = ThingDefOf.MedicineUltratech; j++;
            thingArray[j] = ThingDefOf.Pemmican; j++;
            thingArray[j] = ThingDefOf.Plasteel; j++;
            thingArray[j] = ThingDefOf.RawBerries; j++;
            thingArray[j] = ThingDefOf.RawPotatoes; j++;
            thingArray[j] = ThingDefOf.ShipChunk; j++;
            thingArray[j] = ThingDefOf.Silver; j++;
            thingArray[j] = ThingDefOf.SmokeleafJoint; j++;
            thingArray[j] = ThingDefOf.Snowman; j++;
            thingArray[j] = ThingDefOf.Steel; j++;
            thingArray[j] = ThingDefOf.Uranium; j++;
            thingArray[j] = ThingDefOf.WoodLog; j++;
            thingArray[j] = ThingDefOf.Wort; j++;
            Log.Message("decalred all the items", true);*/
            /*
             * Null checking is very important in RimWorld.
             * 99% of errors reported are from NullReferenceExceptions (NREs).
             * Make sure your code checks if things actually exist, before they
             * try to use the code that belongs to said things.
             */

            //try this maybe it wont crash
            var randS = Rand.Value; // This is a random percentage between 0% and 100%
            GenExplosion.DoExplosion(this.Position, this.launcher.Map, 3.9f, DamageDefOf.Bomb, this, 0, 0);
            GenClamor.DoClamor(this, 3.9f, ClamorDefOf.Impact);
            Map thisMap = this.launcher.Map;

            int hitPos3X = this.Position.x;
            int hitPos3Y = this.Position.y;
            for (int g = -5; g < 5; g++)
            {
                for (int i = -5; i < 5; i++)
                {
                    IntVec3 positionWhatever = new IntVec3(this.Position.x + i, this.Position.y, this.Position.z + g);
                    Pawn newThing = PawnGenerator.GeneratePawn(DefDatabase<PawnKindDef>.GetNamed("Cat")/*pawnKindDefArray[rInt]*/);
                    //newThing.SetFaction(Faction.OfPlayer);//this will probably make them tamed cats
                    //newThing.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.ManhunterPermanent);
                    GenSpawn.Spawn(newThing, positionWhatever, thisMap);
                    //positionWhatever.GetFirstPawn(thisMap).min
                    //rInt = r.Next(0, pawnsCount - 2);

                    //so apparently, you have to do this AFTER you spawn the pawn. I'm leaving all my useless commented-out code so anyone that looks at this will see what I tried that didn't work
                    newThing.mindState.mentalStateHandler.TryStartMentalState(MentalStateDefOf.ManhunterPermanent);
                }
            }
            //Log.Message("done with pawn spawning", true);
        }
        #endregion Overrides
    }
}
