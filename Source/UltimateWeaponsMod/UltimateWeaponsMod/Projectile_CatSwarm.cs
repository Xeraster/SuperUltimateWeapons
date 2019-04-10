using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace UltimateWeaponsMod
{
    class Projectile_CatSwarm : Bullet
    {
        #region Properties
        //
        public Thingdef_CatSwarm Def
        {
            get
            {
                return this.def as Thingdef_CatSwarm;
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
            //PawnKindDef[] pawnKindDefArray;
            //pawnKindDefArray = new PawnKindDef[DefDatabase<PawnKindDef>.DefCount];

            //int pawnsCount = 0;
            //foreach (PawnKindDef kindDef in DefDatabase<PawnKindDef>.AllDefs)
            //{
                //if (kindDef.ToString() == "Cat" /*&& !kindDef.ToString().Contains("Mercenary_") && !kindDef.ToString().Contains("Grenadier_") && !kindDef.ToString().Contains("Tribal_")
                    //&& !kindDef.ToString().Contains("Town") && kindDef.ToString() != "WildMan" && kindDef.ToString() != "Scavenger" && kindDef.ToString() != "Thrasher"
                    //&& !kindDef.ToString().Contains("Pirate") && kindDef.ToString() != "Slave" && kindDef.ToString() != "AncientSoldier"
                    //&& kindDef.ToString() != "Villager" && kindDef.ToString() != "Tribesperson" && kindDef.ToString() != "StrangerInBlack" && kindDef.ToString() != "SpaceRefugee"*/)
                //{

                    //pawnKindDefArray[pawnsCount] = kindDef;
                    //pawnsCount++;
                //}
            //}
            //Log.Message("declared all the pawns", true);
            //Random r = new Random();
            //int rInt = r.Next(0, pawnsCount - 1);
            int hitPos3X = this.Position.x;
            int hitPos3Y = this.Position.y;
            for (int g = -5; g < 5; g++)
            {
                for (int i = -5; i < 5; i++)
                {
                    IntVec3 positionWhatever = new IntVec3(this.Position.x + i, this.Position.y, this.Position.z + g);
                    Pawn newThing = PawnGenerator.GeneratePawn(DefDatabase<PawnKindDef>.GetNamed("Cat")/*pawnKindDefArray[rInt]*/);
                    GenSpawn.Spawn(newThing, positionWhatever, thisMap);
                    //rInt = r.Next(0, pawnsCount - 2);
                }
            }
            //Log.Message("done with pawn spawning", true);

            //ok on to the other shit
            /*if (hitThing != null) //Fancy way to declare a variable inside an if statement. - Thanks Erdelf.
            {
                //Log.Warning("hitThing = " + hitThing.ToString(), true);
                var rand = Rand.Value; // This is a random percentage between 0% and 100%
                GenExplosion.DoExplosion(hitThing.Position, hitThing.Map, 3.9f, DamageDefOf.Bomb, this, 15, 0);
                GenClamor.DoClamor(this, 3.9f, ClamorDefOf.Impact);

                int hitPosX = hitThing.Position.x;
                int hitPosY = hitThing.Position.y;
                for (int g = -5; g < 5; g++)
                {
                    for (int i = -5; i < 5; i++)
                    {
                        Random r2 = new Random();
                        int r2Int = r2.Next(0, 34);
                        IntVec3 positionWhatever = new IntVec3(hitThing.Position.x + i, hitThing.Position.y, hitThing.Position.z + g);
                        int beersToMake = Math.Abs(Math.Abs(g) - 5) + Math.Abs(Math.Abs(i) - 5);
                        for (int b = 0; b < beersToMake; b++)
                        {
                            GenSpawn.Spawn(thingArray[r2Int], positionWhatever, hitThing.Map);
                        }
                    }
                }
            }
            else if (this.launcher.Map != null)
            {
                GenExplosion.DoExplosion(this.Position, this.launcher.Map, 3.9f, DamageDefOf.Bomb, this, 1, 0);
                GenClamor.DoClamor(this, 3.9f, ClamorDefOf.Impact);

                int hitPosX = this.Position.x;
                int hitPosY = this.Position.y;
                for (int g = -5; g < 5; g++)
                {
                    for (int i = -5; i < 5; i++)
                    {
                        Random r3 = new Random();
                        int r3Int = r3.Next(0, 34);
                        IntVec3 positionWhatever = new IntVec3(this.Position.x + i, this.Position.y, this.Position.z + g);
                        int beersToMake = Math.Abs(Math.Abs(g) - 5) + Math.Abs(Math.Abs(i) - 5);
                        for (int b = 0; b < beersToMake; b++)
                        {
                            GenSpawn.Spawn(thingArray[r3Int], positionWhatever, this.launcher.Map);
                        }
                    }
                }
            }*/
        }
        #endregion Overrides
    }
}
