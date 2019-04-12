using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace UltimateWeaponsMod
{
    class Projectile_EverythingRocket : Bullet
    {
        #region Properties
        //
        public Thingdef_EverythingRocket Def
        {
            get
            {
                return this.def as Thingdef_EverythingRocket;
            }
        }
        #endregion Properties
        #region Overrides
        protected override void Impact(Thing hitThing)
        {
            base.Impact(hitThing);

            bool limitValue = Def.limitItemValue;
            bool limitQtyBodyParts = Def.limitQtyBodyParts;
            Log.Message("did the first one", true);
            int maxValueLimit = Def.maxValueLimit;

            Log.Message("Limitvalue = " + limitValue, true);
            Log.Message("value limit = " + maxValueLimit, true);
            int itemDefArraySize = DefDatabase<ThingDef>.DefCount;
            Log.Message("the number of item defs in the thing def database is: " + itemDefArraySize, true);
            Log.Message("About to sort through " + itemDefArraySize + " things in DefDatabase<ThingDef>.All", true);
            List<ThingDef> listOfThings = new List<ThingDef>();
            int thingsCount = 0;
            foreach(ThingDef currentThing in DefDatabase<ThingDef>.AllDefs)
            {
                if (currentThing.IsWithinCategory(ThingCategoryDefOf.Items)
                || currentThing.IsWithinCategory(ThingCategoryDefOf.Drugs) || currentThing.IsWithinCategory(ThingCategoryDefOf.FoodMeals)
                    || currentThing.IsWithinCategory(ThingCategoryDefOf.Foods) || currentThing.IsWithinCategory(ThingCategoryDefOf.Leathers)
                || currentThing.IsWithinCategory(ThingCategoryDefOf.Manufactured) || currentThing.IsWithinCategory(ThingCategoryDefOf.MeatRaw)
                    || currentThing.IsWithinCategory(ThingCategoryDefOf.Medicine) || currentThing.IsWithinCategory(ThingCategoryDefOf.PlantFoodRaw)
                || currentThing.IsWithinCategory(ThingCategoryDefOf.ResourcesRaw) || currentThing.IsWithinCategory(ThingCategoryDefOf.StoneBlocks)
                    || currentThing.IsWithinCategory(ThingCategoryDefOf.StoneChunks) || currentThing.IsWithinCategory(ThingCategoryDefOf.Weapons))
                {
                    //Log.Message("- " + currentThing.label, true);
                    /*I don't understand lists, have never used them in a real world scenario to accomplish anything meaningful so I
                     really hope that this doesn't cause a zillion null exceptions in Rimworld*/
                    if (!currentThing.label.Contains("Unfinished") && !currentThing.label.Contains("persona") && !currentThing.label.Contains("doomsday")
                    && !currentThing.label.Contains("unfinished") && !currentThing.label.Contains("Psychic") && !currentThing.label.Contains("subpersona"))
                    {
                        if (limitValue == true)
                        {
                            if (currentThing.BaseMarketValue < maxValueLimit || currentThing.IsWithinCategory(ThingCategoryDefOf.BodyParts))
                            {
                                listOfThings.Add(currentThing);
                                thingsCount++;
                                Log.Message("- " + currentThing.label, true);
                            }
                        }
                        else
                        {
                            listOfThings.Add(currentThing);
                            thingsCount++;
                        }
                    }
                }
            }

            //strangely, there are some items NOT contained in the thing def database. My list only excludes buildings and some other pointless junk
            //I can add the excluded things in manually. Most of them are in the already decalred in the ThingDefOf class
            listOfThings.Add(ThingDefOf.Steel); thingsCount++;
            listOfThings.Add(ThingDefOf.BlocksGranite); thingsCount++;
            listOfThings.Add(ThingDefOf.Gold); thingsCount++;
            listOfThings.Add(ThingDefOf.Silver); thingsCount++;
            listOfThings.Add(ThingDefOf.Uranium); thingsCount++;
            listOfThings.Add(ThingDefOf.Plasteel); thingsCount++;
            listOfThings.Add(ThingDefOf.ChunkSlagSteel); thingsCount++;

            Log.Message("Done. wrote " + thingsCount + " things to a bigass list", true);

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
            PawnKindDef[] pawnKindDefArray;
            pawnKindDefArray = new PawnKindDef[DefDatabase<PawnKindDef>.DefCount];

            int pawnsCount = 0;
            foreach (PawnKindDef kindDef in DefDatabase<PawnKindDef>.AllDefs)
            {
                if (!kindDef.ToString().Contains("Mech_") && !kindDef.ToString().Contains("Mercenary_") && !kindDef.ToString().Contains("Grenadier_") && !kindDef.ToString().Contains("Tribal_")
                    && !kindDef.ToString().Contains("Town") /*&& !kindDef.ToString().Contains("Drifter")*/ && kindDef.ToString() != "WildMan" && kindDef.ToString() != "Scavenger" && kindDef.ToString() != "Thrasher"
                    && !kindDef.ToString().Contains("Pirate") && kindDef.ToString() != "Slave" && kindDef.ToString() != "AncientSoldier"
                    && kindDef.ToString() != "Villager" && kindDef.ToString() != "Tribesperson" && kindDef.ToString() != "StrangerInBlack" && kindDef.ToString() != "SpaceRefugee")
                {

                    pawnKindDefArray[pawnsCount] = kindDef;
                    pawnsCount++;
                }
            }
            //Log.Message("declared all the pawns", true);
            Random r = new Random();
            int rInt = r.Next(0, pawnsCount - 1);
            int hitPos3X = this.Position.x;
            int hitPos3Y = this.Position.y;

            for (int g = -3; g < 3; g++)
            {
                for (int i = -3; i < 3; i++)
                {
                    IntVec3 positionWhatever = new IntVec3(this.Position.x + i, this.Position.y, this.Position.z + g);
                    Pawn newThing = PawnGenerator.GeneratePawn(pawnKindDefArray[rInt]);
                    GenSpawn.Spawn(newThing, positionWhatever, thisMap);
                    rInt = r.Next(0, pawnsCount - 2);
                }
            }
            //Log.Message("done with pawn spawning", true);

            //ok on to the other shit
            if (hitThing != null || this.launcher.Map != null) //Fancy way to declare a variable inside an if statement. - Thanks Erdelf.
            {
                //Log.Warning("hitThing = " + hitThing.ToString(), true);
                var rand = Rand.Value; // This is a random percentage between 0% and 100%
                GenExplosion.DoExplosion(hitThing.Position, hitThing.Map, 3.9f, DamageDefOf.Bomb, this, 15, 0);
                GenClamor.DoClamor(this, 3.9f, ClamorDefOf.Impact);

                int hitPosX;// = hitThing.Position.x;
                int hitPosY;// = hitThing.Position.y;
                int hitPosZ;// = hitThing.Position.z;
                if (hitThing == null)
                {
                    hitPosX = this.launcher.Position.x;
                    hitPosY = this.launcher.Position.y;
                    hitPosZ = this.launcher.Position.z;
                }
                else
                {
                    hitPosX = hitThing.Position.x;
                    hitPosY = hitThing.Position.y;
                    hitPosZ = hitThing.Position.z;
                }
                //Log.Message("about to spawn a zillion items in for loop 1", true);
                for (int g = -5; g < 5; g++)
                {
                    for (int i = -5; i < 5; i++)
                    {
                        //Random r2 = new Random();
                        //int r2Int = r2.Next(0, 34);
                        IntVec3 positionWhatever = new IntVec3(hitPosX + i, hitPosY, hitPosZ + g);
                        ThingDef thingyWhateverFoo = listOfThings.RandomElement<ThingDef>();
                        Thing thingThatsAboutToBeSpawned;
                        int numThingsToSpawn;

                        //this is meant to combat the issue of there being too many body parts spawning everywhere
                        //It's only so funny for so long, you know ?
                        //I left it possible for the user to toggle this feature on and off via the xml file
                        if (thingyWhateverFoo.MadeFromStuff)
                        {
                            thingThatsAboutToBeSpawned = ThingMaker.MakeThing(thingyWhateverFoo, ThingDefOf.Steel);
                            numThingsToSpawn = 1;
                        }
                        else
                        {
                            thingThatsAboutToBeSpawned = ThingMaker.MakeThing(thingyWhateverFoo);
                            numThingsToSpawn = Math.Abs(Math.Abs(g) - 5) + Math.Abs(Math.Abs(i) - 5);
                        }
                        int qtyMult = 1;
                        if (thingyWhateverFoo.IsWithinCategory(ThingCategoryDefOf.Leathers) || thingyWhateverFoo.IsWithinCategory(ThingCategoryDefOf.Foods)
                        || thingyWhateverFoo.IsWithinCategory(ThingCategoryDefOf.Drugs) || thingyWhateverFoo.IsWithinCategory(ThingCategoryDefOf.ResourcesRaw)
                            || thingyWhateverFoo.IsWithinCategory(ThingCategoryDefOf.PlantMatter) || thingyWhateverFoo == ThingDefOf.Steel
                        || thingyWhateverFoo == ThingDefOf.Gold || thingyWhateverFoo == ThingDefOf.Silver || thingyWhateverFoo == ThingDefOf.Plasteel
                            || thingyWhateverFoo == ThingDefOf.Uranium || thingyWhateverFoo == ThingDefOf.BlocksGranite)
                        {
                            qtyMult = new Random().Next(6, 20);
                            Log.Message("Found an item to change to spawn amount of. It's " + thingyWhateverFoo.label + " and the spawn amount will be " + (numThingsToSpawn + qtyMult), true);
                        }
                        else
                            if (thingyWhateverFoo.IsWithinCategory(ThingCategoryDefOf.BodyParts) && limitQtyBodyParts == true)
                        {
                            numThingsToSpawn = new Random().Next(1, 2);
                            qtyMult = 1;
                        }
                        if (!MiscCrap.IsBuilingHere(this.launcher.Map, positionWhatever))
                        {
                            for (int b = 0; b < numThingsToSpawn * qtyMult; b++)
                            {
                                //Log.Message("r2Int = " + r2Int, true);
                                if (thingyWhateverFoo.MadeFromStuff)
                                {
                                    GenSpawn.Spawn(thingThatsAboutToBeSpawned, positionWhatever, hitThing.Map);
                                }
                                else
                                {
                                    GenSpawn.Spawn(thingyWhateverFoo, positionWhatever, hitThing.Map);
                                }
                            }
                        }
                    }
                }
                //Log.Message("done", true);
            }
            /*else if (this.launcher.Map != null)
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
