using System;
using RimWorld;
using Verse;

namespace UltimateWeaponsMod
{
    public static class BuildingSpawner
    {
        /// <summary>
        /// Spawns a building with the given parameters
        /// </summary>
        /// <param name="whichMap">args will be passed when starting this program</param>
        /// <param name="stuff">what will the wall be made out of</param>
        /// <param name="baseLocation">center location of the wall spawning coordinates</param>
        ///  <param name="floorIt">What's the first thing you do before you start the boat?</param>
        /// <param name="type">0 = general purpose. 1 = freezer. 2 = prison. 3 = bedroom. 4 = workshop. 5 = greenhouse. 6 = wall for trump gun</param>
        public static void SpawnBuilding(Map whichMap, ThingDef stuff, IntVec3 baseLocation, bool floorIt = false, int type = 0)
        {
            MakeWall(whichMap, stuff, baseLocation, 5, 5, true, type);
        }

        /// <summary>
        /// Makes a wall of specified size
        /// </summary>
        /// <param name="whichMap">args will be passed when starting this program</param>
        /// <param name="whichMap">args will be passed when starting this program</param>
        /// <param name="stuff">what will the wall be made out of</param>
        /// <param name="baseLocation">center location of the wall spawning coordinates</param>
        /// <param name="sizeX">the x size</param>
        /// <param name="sizeZ">the z size</param>
        ///  <param name="floorIt">What's the first thing you do before you start the boat?</param>
        /// <param name="type">0 = general purpose. 1 = kitchen. 2 = prison. 3 = bedroom. 4 = workshop. 5 = greenhouse. 6 = nothing</param>
        public static void MakeWall(Map whichMap, ThingDef stuff, IntVec3 baseLocation, int sizeX, int sizeZ, bool floorIt = false, int type = 0)
        {

            //This part spawns the walls
            for (int x = -sizeX; x < sizeX + 1; x++)
            {
                for (int z = -sizeZ; z < sizeZ + 1; z++)
                {
                    if (Math.Abs(z) == sizeZ || Math.Abs(x) == sizeX)
                        {
                        ThingDef thingToSpawn;
                        thingToSpawn = ThingDefOf.Wall;
                        Thing theThingToSpawn = ThingMaker.MakeThing(thingToSpawn, ThingDefOf.Steel);
                        GenSpawn.Spawn(theThingToSpawn, new IntVec3(baseLocation.x + x, baseLocation.y, baseLocation.z + z), whichMap);
                        if (type != 6)
                        {
                            whichMap.roofGrid.SetRoof(new IntVec3(baseLocation.x + x, baseLocation.y, baseLocation.z + z), RoofDefOf.RoofConstructed);
                        }
                    }
                }   
            }

            //spawn a door
            ThingDef door = ThingDefOf.Door;
            Thing theDoor = ThingMaker.MakeThing(door, ThingDefOf.Steel);
            GenSpawn.Spawn(theDoor, new IntVec3(baseLocation.x + sizeX, baseLocation.y, baseLocation.z + (sizeZ - 2)), whichMap);

            //spawn the floor
            if (floorIt == true)
            {
                for (int xx = -sizeX + 1; xx < sizeX; xx++)
                {
                    for (int zz = -sizeZ + 1; zz < sizeZ; zz++)
                    {
                        TerrainDef theFuckingFloor = TerrainDefOf.Concrete;
                        whichMap.terrainGrid.SetTerrain(new IntVec3(baseLocation.x + xx, baseLocation.y, baseLocation.z + zz), theFuckingFloor);
                        if (type != 6)
                        {
                            whichMap.roofGrid.SetRoof(new IntVec3(baseLocation.x + xx, baseLocation.y, baseLocation.z + zz), RoofDefOf.RoofConstructed);
                        }
                        //figuring out how to do this was much more difficult than it looks
                    }
                }
            }

            if (type != 6)
            {
                //spawn furniture if applicable. Do this for ALL room types
                ThingDef ac = ThingDefOf.Cooler;
                GenSpawn.Spawn(ac, new IntVec3(baseLocation.x + sizeX - 2, baseLocation.y, baseLocation.z + sizeZ), whichMap);
                GenSpawn.Spawn(ThingDefOf.StandingLamp, new IntVec3(baseLocation.x, baseLocation.y, baseLocation.z + 1), whichMap);

                if (type == 1)
                {
                    GenSpawn.Spawn(ac, new IntVec3(baseLocation.x + sizeX - 1, baseLocation.y, baseLocation.z + sizeZ), whichMap);
                    GenSpawn.Spawn(ac, new IntVec3(baseLocation.x + sizeX - 3, baseLocation.y, baseLocation.z + sizeZ), whichMap);
                }
                if (type != 1)
                {
                    ThingDef heater = ThingDefOf.Heater;
                    GenSpawn.Spawn(heater, new IntVec3(baseLocation.x, baseLocation.y, baseLocation.z), whichMap);

                    //spawn a table
                    ThingDef tableToSpawn;
                    tableToSpawn = ThingDefOf.Table2x2c;
                    Thing theTableToSpawn = ThingMaker.MakeThing(tableToSpawn, ThingDefOf.Steel);
                    GenSpawn.Spawn(theTableToSpawn, new IntVec3(baseLocation.x - sizeX + 1, baseLocation.y, baseLocation.z + sizeX - 2), whichMap);

                    //spawn chairs
                    ThingDef chair;
                    chair = ThingDefOf.DiningChair;
                    Thing chairToSpawn = ThingMaker.MakeThing(chair, ThingDefOf.WoodLog);

                    ThingDef chair2;
                    chair2 = ThingDefOf.DiningChair;
                    Thing chairToSpawn2 = ThingMaker.MakeThing(chair2, ThingDefOf.WoodLog);

                    ThingDef chair3;
                    chair3 = ThingDefOf.DiningChair;
                    Thing chairToSpawn3 = ThingMaker.MakeThing(chair3, ThingDefOf.WoodLog);

                    ThingDef chair4;
                    chair4 = ThingDefOf.DiningChair;
                    Thing chairToSpawn4 = ThingMaker.MakeThing(chair4, ThingDefOf.WoodLog);
                    //I can't fucking believe you have to do this 4 fucking times (one for each spawn instance). What kind of crack were the developers smoking
                    //It's actually quite impressive actually. How the fuck can this anomoly be reproduced? How TF does the game know it's the same chair variable? (if I try to spawn the same chair 4 time)

                    //for Rot4 variables, 0 = North. 1 = East. 2 = South. 3 = West
                    GenSpawn.Spawn(chairToSpawn, new IntVec3(baseLocation.x - sizeX + 3, baseLocation.y, baseLocation.z + sizeX - 2), whichMap, new Rot4(3));
                    GenSpawn.Spawn(chairToSpawn2, new IntVec3(baseLocation.x - sizeX + 3, baseLocation.y, baseLocation.z + sizeX - 1), whichMap, new Rot4(3));

                    GenSpawn.Spawn(chairToSpawn3, new IntVec3(baseLocation.x - sizeX + 2, baseLocation.y, baseLocation.z + sizeX - 3), whichMap, new Rot4(0));
                    GenSpawn.Spawn(chairToSpawn4, new IntVec3(baseLocation.x - sizeX + 1, baseLocation.y, baseLocation.z + sizeX - 3), whichMap, new Rot4(0));

                    //the above code should spawn 4 chairs around the table wit heach chair facing the same direction

                    //now, let's spawn 3 or 4 beds
                    //using a for loop as a workaround to the "you can't use the same variable twice" bullshit as seen above
                    //I still want to know what kind of fucking crack the devs were on tho

                    for (int g = 0; g < 4; g++)
                    {
                        Log.Message("in loop. g = " + g, true);
                        ThingDef dynamicBed;
                        dynamicBed = ThingDefOf.Bed;
                        Thing bedToSpawn = ThingMaker.MakeThing(dynamicBed, ThingDefOf.Steel);
                        GenSpawn.Spawn(bedToSpawn, new IntVec3(baseLocation.x - sizeX + 1 + g, baseLocation.y, baseLocation.z - sizeZ + 1), whichMap);
                    }
                }

            }
        }
    }

}
