using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace UltimateWeaponsMod
{
    class Projectile_UsefulRocket : Bullet
    {
        #region Properties
        //
        public Thingdef_UsefulRocket Def
        {
            get
            {
                return this.def as Thingdef_UsefulRocket;
            }
        }
        #endregion Properties
        #region Overrides
        protected override void Impact(Thing hitThing)
        {
            base.Impact(hitThing);

            int itemDefArraySize = DefDatabase<ThingDef>.DefCount;
            Log.Message("the number of item defs in the thing def database is: " + itemDefArraySize, true);
            Log.Message("About to sort through " + itemDefArraySize + " things in DefDatabase<ThingDef>.All", true);
            List<ThingDef> listOfThings = new List<ThingDef>();
            int thingsCount = 0;
            foreach (ThingDef currentThing in DefDatabase<ThingDef>.AllDefs)
            {
                if (currentThing.IsWithinCategory(ThingCategoryDefOf.Drugs) || currentThing.IsWithinCategory(ThingCategoryDefOf.FoodMeals)
                    || currentThing.IsWithinCategory(ThingCategoryDefOf.Foods) || currentThing.IsWithinCategory(ThingCategoryDefOf.Leathers)
                || currentThing.IsWithinCategory(ThingCategoryDefOf.Manufactured) || currentThing.IsWithinCategory(ThingCategoryDefOf.MeatRaw)
                    || currentThing.IsWithinCategory(ThingCategoryDefOf.Medicine) || currentThing.IsWithinCategory(ThingCategoryDefOf.PlantFoodRaw)
                || currentThing.IsWithinCategory(ThingCategoryDefOf.ResourcesRaw) || currentThing.IsWithinCategory(ThingCategoryDefOf.StoneBlocks)
                    || currentThing.IsWithinCategory(ThingCategoryDefOf.StoneChunks))
                {
                    //Log.Message("- " + currentThing.label, true);
                    /*I don't understand lists, have never used them in a real world scenario to accomplish anything meaningful so I
                     really hope that this doesn't cause a zillion null exceptions in Rimworld*/
                    if (!currentThing.label.Contains("Unfinished") && !currentThing.label.Contains("persona") && !currentThing.label.Contains("doomsday")
                    && !currentThing.label.Contains("unfinished") && !currentThing.label.Contains("Psychic") && !currentThing.label.Contains("subpersona"))
                    {
                                listOfThings.Add(currentThing);
                                thingsCount++;
                                Log.Message("- " + currentThing.label, true);
 
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


            //ok on to the other shit
            if (hitThing != null || this.launcher.Map != null) //Fancy way to declare a variable inside an if statement. - Thanks Erdelf.
            {
                //Log.Warning("hitThing = " + hitThing.ToString(), true);
                var rand = Rand.Value; // This is a random percentage between 0% and 100%
                //GenExplosion.DoExplosion(hitThing.Position, hitThing.Map, 3.9f, DamageDefOf.Bomb, this, 15, 0);
                //GenClamor.DoClamor(this, 3.9f, ClamorDefOf.Impact);

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
