using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace UltimateWeaponsMod
{
    class Projectile_GunRocket : Bullet
    {
        #region Properties
        //
        public Thingdef_GunRocket Def
        {
            get
            {
                return this.def as Thingdef_GunRocket;
            }
        }
        #endregion Properties
        #region Overrides
        protected override void Impact(Thing hitThing)
        {
            base.Impact(hitThing);

            Log.Message("about to attemp to load the def variables..", true);
            int limitGunValue = Def.limitGunValue;
            int effectRadius = Def.effectRadius;
            bool excludeOpGuns = Def.excludeOpGuns;
            bool spawnFewMelee = Def.spawnFewMelee;
            Log.Message("If you are reading this, the def variables sucessfully got read and loaded", true);
            int itemDefArraySize = DefDatabase<ThingDef>.DefCount;
            Log.Message("the number of item defs in the thing def database is: " + itemDefArraySize, true);
            Log.Message("About to sort through " + itemDefArraySize + " things in DefDatabase<ThingDef>.All", true);
            List<ThingDef> listOfThings = new List<ThingDef>();
            int thingsCount = 0;
            foreach (ThingDef currentThing in DefDatabase<ThingDef>.AllDefs)
            {
                if (currentThing.IsWithinCategory(ThingCategoryDefOf.Weapons))
                {
                    //Log.Message("- " + currentThing.label, true);
                    /*I don't understand lists, have never used them in a real world scenario to accomplish anything meaningful so I
                     really hope that this doesn't cause a zillion null exceptions in Rimworld*/
                    if (!currentThing.label.Contains("Unfinished") && !currentThing.label.Contains("uranium") && !currentThing.label.Contains("unfinished") && !currentThing.label.Contains("Psychic") && !currentThing.label.Contains("subpersona") && !currentThing.label.Contains("turret"))
                    {
                        if (limitGunValue == 0 || currentThing.BaseMarketValue < limitGunValue)
                        {
                            listOfThings.Add(currentThing);
                            thingsCount++;
                        }
                        //Log.Message("- " + currentThing.label, true);

                    }
                }
            }

            //strangely, there are some items NOT contained in the thing def database. My list only excludes buildings and some other pointless junk
            //I can add the excluded things in manually. Most of them are in the already decalred in the ThingDefOf class

            //Log.Message("Done. wrote " + thingsCount + " things to a bigass list", true);


            //ok on to the other shit
            if (hitThing != null || base.Position != null) //Fancy way to declare a variable inside an if statement. - Thanks Erdelf.
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
                    hitPosX = base.Position.x;
                    hitPosY = base.Position.y;
                    hitPosZ = base.Position.z;
                }
                else
                {
                    hitPosX = hitThing.Position.x;
                    hitPosY = hitThing.Position.y;
                    hitPosZ = hitThing.Position.z;
                }
                //Log.Message("about to spawn a zillion items in for loop 1", true);
                for (int g = -effectRadius; g < effectRadius; g++)
                {
                    for (int i = -effectRadius; i < effectRadius; i++)
                    {
                        //Random r2 = new Random();
                        //int r2Int = r2.Next(0, 34);
                        IntVec3 positionWhatever = new IntVec3(hitPosX + i, hitPosY, hitPosZ + g);
                        ThingDef thingyWhateverFoo = listOfThings.RandomElement<ThingDef>();
                        Thing thingThatsAboutToBeSpawned;
                        int numThingsToSpawn = 1;

                        if (!thingyWhateverFoo.IsRangedWeapon)
                        {
                            //bool meleeOK;
                            if (new Random().Next(0, 1000) < 50 && spawnFewMelee == true)
                            {
                                //cool. do nothing then
                            }
                            else
                            {
                                //bool foundRanged = false;
                                while (!thingyWhateverFoo.IsRangedWeapon)
                                {
                                    thingyWhateverFoo = listOfThings.RandomElement<ThingDef>();
                                }
                            }
                        }

                        if (thingyWhateverFoo.MadeFromStuff)
                        {
                            thingThatsAboutToBeSpawned = ThingMaker.MakeThing(thingyWhateverFoo, ThingDefOf.Steel);
                            numThingsToSpawn = 1;
                        }
                        else
                        {
                            thingThatsAboutToBeSpawned = ThingMaker.MakeThing(thingyWhateverFoo);
                            //numThingsToSpawn = Math.Abs(Math.Abs(g) - 5) + Math.Abs(Math.Abs(i) - 5);
                            numThingsToSpawn = 1;
                        }
                        int qtyMult = 1;

                        if (!MiscCrap.IsBuilingHere(this.launcher.Map, positionWhatever))
                        {
                            for (int b = 0; b < numThingsToSpawn * qtyMult; b++)
                            {
                                //Log.Message("r2Int = " + r2Int, true);

                                //don't spawn over a building. This causes it to deconstruct once you save and reload
                                if (thingyWhateverFoo.MadeFromStuff)
                                {
                                    GenSpawn.Spawn(thingThatsAboutToBeSpawned, positionWhatever, this.launcher.Map);
                                }
                                else
                                {
                                    GenSpawn.Spawn(thingyWhateverFoo, positionWhatever, this.launcher.Map);
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
