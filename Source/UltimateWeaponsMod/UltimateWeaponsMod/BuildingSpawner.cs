using System;
using System.Collections.Generic;
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
        /// <param name="type">0 = general purpose. 1 = freezer. 2 = prison. 3 = bedroom. 4 = workshop. 5 = hospital. 6 = wall for trump gun</param>
        public static void SpawnBuilding(Map whichMap, ThingDef stuff, IntVec3 baseLocation, bool floorIt = false, int type = 0, bool power = false)
        {
            MakeWall(whichMap, stuff, baseLocation, 5, 5, true, type, power);
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
        /// <param name="type">0 = general purpose. 1 = freezer. 2 = prison. 3 = bedroom. 4 = workshop. 5 = hospital. 6 = nothing</param>
        public static void MakeWall(Map whichMap, ThingDef stuff, IntVec3 baseLocation, int sizeX, int sizeZ, bool floorIt = false, int type = 0, bool power = false)
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
                        if (power == true)
                        {
                            GenSpawn.Spawn(ThingDefOf.PowerConduit, new IntVec3(baseLocation.x + x, baseLocation.y, baseLocation.z + z), whichMap);
                        }
                    }
                }   
            }

            //spawn a door
            ThingDef door = ThingDefOf.Door;
            Thing theDoor = ThingMaker.MakeThing(door, ThingDefOf.Steel);
            GenSpawn.Spawn(theDoor, new IntVec3(baseLocation.x + sizeX, baseLocation.y, baseLocation.z + (sizeZ - 2)), whichMap);
            if (type == 1)
            {
                //spawn another door door
                ThingDef otherdoor = ThingDefOf.Door;
                Thing theOtherDoor = ThingMaker.MakeThing(door, ThingDefOf.Steel);
                GenSpawn.Spawn(theOtherDoor, new IntVec3(baseLocation.x - sizeX, baseLocation.y, baseLocation.z + (sizeZ - 2)), whichMap);
            }

            //spawn the floor
            List<TerrainDef> listOfFloors = new List<TerrainDef>();
            TerrainDef theFloor;
            if (type == 3)
            {
                foreach (TerrainDef thingInQuestion in DefDatabase<TerrainDef>.AllDefs)
                {
                    if (thingInQuestion.IsCarpet)
                    {
                        Log.Message("item to add into list is:" + thingInQuestion.label, true);
                        listOfFloors.Add(thingInQuestion);
                    }
                }
                theFloor = listOfFloors.RandomElement<TerrainDef>();
            }
            else if (type == 5)
            {
                theFloor = DefDatabase<TerrainDef>.GetNamed("SterileTile");
            }
            else
            {
                theFloor = TerrainDefOf.Concrete;
            }
            if (floorIt == true)
            {
                for (int xx = -sizeX + 1; xx < sizeX; xx++)
                {
                    for (int zz = -sizeZ + 1; zz < sizeZ; zz++)
                    {
                        //TerrainDef theFuckingFloor = TerrainDefOf.Concrete;
                        whichMap.terrainGrid.SetTerrain(new IntVec3(baseLocation.x + xx, baseLocation.y, baseLocation.z + zz), theFloor);
                        if (type != 6)
                        {
                            whichMap.roofGrid.SetRoof(new IntVec3(baseLocation.x + xx, baseLocation.y, baseLocation.z + zz), RoofDefOf.RoofConstructed);
                        }
                        //figuring out how to do this was much more difficult than it looks
                    }
                }
            }

            if (type != 6 && type != 3 && type != 5)
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
                    //not the best way to do it, but too late, I already did it

                    //for Rot4 variables, 0 = North. 1 = East. 2 = South. 3 = West
                    GenSpawn.Spawn(chairToSpawn, new IntVec3(baseLocation.x - sizeX + 3, baseLocation.y, baseLocation.z + sizeX - 2), whichMap, new Rot4(3));
                    GenSpawn.Spawn(chairToSpawn2, new IntVec3(baseLocation.x - sizeX + 3, baseLocation.y, baseLocation.z + sizeX - 1), whichMap, new Rot4(3));

                    GenSpawn.Spawn(chairToSpawn3, new IntVec3(baseLocation.x - sizeX + 2, baseLocation.y, baseLocation.z + sizeX - 3), whichMap, new Rot4(0));
                    GenSpawn.Spawn(chairToSpawn4, new IntVec3(baseLocation.x - sizeX + 1, baseLocation.y, baseLocation.z + sizeX - 3), whichMap, new Rot4(0));

                    //the above code should spawn 4 chairs around the table wit heach chair facing the same direction

                    //now, let's spawn 3 or 4 beds
                    //using a for loop instead of doing what I did above again

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

        public static void SpawnAnEntireBase(Map whichMap, IntVec3 baseCenter)
        {
            ClearObstacles(whichMap, new IntVec3(baseCenter.x - 5, baseCenter.y, baseCenter.z + 9), 8, 16);
            SpawnBedrooms(whichMap, ThingDefOf.Steel, baseCenter, 4, true);
            SpawnBedrooms(whichMap, ThingDefOf.Steel, new IntVec3(baseCenter.x - 6, baseCenter.y, baseCenter.z + 8), 2, true, true);

            SpawnHospital(whichMap, ThingDefOf.Steel, new IntVec3(baseCenter.x - 6, baseCenter.y, baseCenter.z + 2));
            SpawnBuilding(whichMap, ThingDefOf.Steel, new IntVec3(baseCenter.x + 14, baseCenter.y, baseCenter.z), true, 1, true);

            SpawnStorageRoom(whichMap, ThingDefOf.Steel, new IntVec3(baseCenter.x - 19, baseCenter.y, baseCenter.z + 4), true, 3, true);
            SpawnRecRoom(whichMap, ThingDefOf.Steel, new IntVec3(baseCenter.x + 12, baseCenter.y, baseCenter.z + 11), 3, 6);

            SpawnWorkshop(whichMap, ThingDefOf.Steel, new IntVec3(baseCenter.x - 19, baseCenter.y, baseCenter.z + 18), 4, 5);
        }
        /// <summary>
        /// Spawns a building with bedrooms in it
        /// </summary>
        /// <param name="whichMap">args will be passed when starting this program</param>
        /// <param name="stuff">what will the walls be made out of</param>
        /// <param name="baseLocation">center location of where the building will spawn</param>
        /// <param name="numRooms">how many bedrooms will the building have</param>
        ///  <param name="floorIt">What's the first thing you do before you start the boat?</param>
        public static void SpawnBedrooms(Map whichMap, ThingDef stuff, IntVec3 baseLocation, int numRooms, bool floorIt = false, bool doorOnLeft = false)
        {
            Log.Message("got into spawn bedrooms function", true);
            ThingDef endTable = DefDatabase<ThingDef>.GetNamed("EndTable");
            ThingDef dresser = DefDatabase<ThingDef>.GetNamed("Dresser");
            ThingDef plantPot = DefDatabase<ThingDef>.GetNamed("PlantPot");


            for (int i = 0; i < numRooms; i++)
            {
                MakeWall(whichMap, stuff, new IntVec3(baseLocation.x, baseLocation.y, baseLocation.z + (i * 4)), 3, 2, true, 3, true);
                Thing bed = ThingMaker.MakeThing(ThingDefOf.Bed, ThingDefOf.Steel);
                GenSpawn.Spawn(bed, new IntVec3(baseLocation.x - 2, baseLocation.y, baseLocation.z + (i * 4) + 1), whichMap, new Rot4(1));

                //spawn the vents
                if (i > 0)
                {
                    GenSpawn.Spawn(ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("Vent")), new IntVec3(baseLocation.x - 2, baseLocation.y, baseLocation.z + (i * 4) - 2), whichMap, new Rot4(2));
                }
                if (i == numRooms - 1)
                {
                    GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.Cooler), new IntVec3(baseLocation.x - 2, baseLocation.y, baseLocation.z + (i * 4) + 2), whichMap, new Rot4(0));
                }

                GenSpawn.Spawn(ThingMaker.MakeThing(endTable, ThingDefOf.Steel), new IntVec3(baseLocation.x - 2, baseLocation.y, baseLocation.z + (i * 4) + 0), whichMap, new Rot4(1));
                GenSpawn.Spawn(ThingMaker.MakeThing(dresser, ThingDefOf.Steel), new IntVec3(baseLocation.x + 1, baseLocation.y, baseLocation.z + (i * 4) - 1), whichMap, new Rot4(0));
                GenSpawn.Spawn(ThingMaker.MakeThing(plantPot, ThingDefOf.Steel), new IntVec3(baseLocation.x - 0, baseLocation.y, baseLocation.z + (i * 4) + 1), whichMap);
                GenSpawn.Spawn(ThingDefOf.StandingLamp, new IntVec3(baseLocation.x - 0, baseLocation.y, baseLocation.z + (i * 4)), whichMap);
                GenSpawn.Spawn(ThingDefOf.Heater, new IntVec3(baseLocation.x - 0, baseLocation.y, baseLocation.z + (i * 4) - 1), whichMap);

                if (doorOnLeft == true)
                {
                    ThingDef door = ThingDefOf.Door;
                    Thing theDoor = ThingMaker.MakeThing(door, ThingDefOf.Steel);
                    GenSpawn.Spawn(ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("Vent")), new IntVec3(baseLocation.x + 3, baseLocation.y, baseLocation.z + (i * 4) + 1), whichMap, new Rot4(1));
                    GenSpawn.Spawn(theDoor, new IntVec3(baseLocation.x - 3, baseLocation.y, baseLocation.z + (i * 4) + 1), whichMap);
                }
            }
        }

        public static void SpawnHospital(Map whichMap, ThingDef stuff, IntVec3 baseLocation)
        {
            MakeWall(whichMap, stuff, baseLocation, 3, 4, true, 5, true);
            //spawn a door
            ThingDef door = ThingDefOf.Door;
            Thing theDoor = ThingMaker.MakeThing(door, ThingDefOf.Steel);
            GenSpawn.Spawn(ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("Vent")), new IntVec3(baseLocation.x + 3, baseLocation.y, baseLocation.z + (4 - 2)), whichMap, new Rot4(1));
            GenSpawn.Spawn(theDoor, new IntVec3(baseLocation.x - 3, baseLocation.y, baseLocation.z + (4 - 2)), whichMap);
            GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.Cooler), new IntVec3(baseLocation.x - 3, baseLocation.y, baseLocation.z + 1), whichMap, new Rot4(3));
            GenSpawn.Spawn(ThingDefOf.Heater, new IntVec3(baseLocation.x - 2, baseLocation.y, baseLocation.z + 1), whichMap);

            GenSpawn.Spawn(DefDatabase<ThingDef>.GetNamed("StandingLamp_Blue"), new IntVec3(baseLocation.x - 1, baseLocation.y, baseLocation.z + 3), whichMap);
            GenSpawn.Spawn(DefDatabase<ThingDef>.GetNamed("StandingLamp_Blue"), new IntVec3(baseLocation.x - 1, baseLocation.y, baseLocation.z - 1), whichMap);

            GenSpawn.Spawn(ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("HospitalBed"), ThingDefOf.Steel), new IntVec3(baseLocation.x + 2, baseLocation.y, baseLocation.z - 1), whichMap, new Rot4(3));
            GenSpawn.Spawn(ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("HospitalBed"), ThingDefOf.Steel), new IntVec3(baseLocation.x + 2, baseLocation.y, baseLocation.z + 3), whichMap, new Rot4(3));
            GenSpawn.Spawn(ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("HospitalBed"), ThingDefOf.Steel), new IntVec3(baseLocation.x + 2, baseLocation.y, baseLocation.z + 1), whichMap, new Rot4(3));
            GenSpawn.Spawn(ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("HospitalBed"), ThingDefOf.Steel), new IntVec3(baseLocation.x + 2, baseLocation.y, baseLocation.z - 3), whichMap, new Rot4(3));
            GenSpawn.Spawn(ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("VitalsMonitor")), new IntVec3(baseLocation.x, baseLocation.y, baseLocation.z - 3), whichMap, new Rot4(1));
            GenSpawn.Spawn(ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("VitalsMonitor")), new IntVec3(baseLocation.x, baseLocation.y, baseLocation.z - 1), whichMap, new Rot4(1));
            GenSpawn.Spawn(ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("VitalsMonitor")), new IntVec3(baseLocation.x, baseLocation.y, baseLocation.z + 1), whichMap, new Rot4(1));
            GenSpawn.Spawn(ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("VitalsMonitor")), new IntVec3(baseLocation.x, baseLocation.y, baseLocation.z + 3), whichMap, new Rot4(1));
        }

        public static void ClearObstacles(Map whichMap, IntVec3 position, int sizeX, int sizeZ)
        {
         for (int x = -sizeX; x < sizeX + 1; x++)
            {
                for (int z = -sizeZ; z < sizeZ + 1; z++)
                {
                    IntVec3 currentPosition = new IntVec3(position.x + x, position.y, position.z + z);
                    if (currentPosition.GetFirstBuilding(whichMap) != null)
                    {
                        currentPosition.GetFirstBuilding(whichMap).DeSpawn();
                    }
                }
            }
        }

        public static void SpawnRecRoom(Map whichMap, ThingDef stuff, IntVec3 position, int sizeX, int sizeZ)
        {
            MakeWall(whichMap, stuff, position, sizeX, sizeZ, true, 3, true);
            ThingDef door = ThingDefOf.Door;
            Thing theDoor = ThingMaker.MakeThing(door, ThingDefOf.Steel);
            GenSpawn.Spawn(theDoor, new IntVec3(position.x - sizeX, position.y, position.z + (sizeZ - 4)), whichMap);

            GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.NutrientPasteDispenser), new IntVec3(position.x + sizeX - 2, position.y, position.z - (sizeZ) - 1), whichMap, new Rot4(0));
            GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.Hopper), new IntVec3(position.x + sizeX - 2, position.y, position.z - (sizeZ) - 3), whichMap, new Rot4(0));
            GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.Hopper), new IntVec3(position.x + sizeX - 3, position.y, position.z - (sizeZ) - 3), whichMap, new Rot4(0));

            GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.Table2x2c, ThingDefOf.Steel), new IntVec3(position.x + sizeX - 2, position.y, position.z - (sizeZ) + 4), whichMap, new Rot4(0));
            GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.DiningChair, ThingDefOf.WoodLog), new IntVec3(position.x + sizeX - 1, position.y, position.z - (sizeZ) + 3), whichMap, new Rot4(0));
            GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.DiningChair, ThingDefOf.WoodLog), new IntVec3(position.x + sizeX - 2, position.y, position.z - (sizeZ) + 3), whichMap, new Rot4(0));

            GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.DiningChair, ThingDefOf.WoodLog), new IntVec3(position.x + sizeX - 1, position.y, position.z - (sizeZ) + 6), whichMap, new Rot4(2));
            GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.DiningChair, ThingDefOf.WoodLog), new IntVec3(position.x + sizeX - 2, position.y, position.z - (sizeZ) + 6), whichMap, new Rot4(2));
            GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.DiningChair, ThingDefOf.WoodLog), new IntVec3(position.x + sizeX - 3, position.y, position.z - (sizeZ) + 5), whichMap, new Rot4(1));
            GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.DiningChair, ThingDefOf.WoodLog), new IntVec3(position.x + sizeX - 3, position.y, position.z - (sizeZ) + 4), whichMap, new Rot4(1));
            GenSpawn.Spawn(ThingDefOf.Heater, new IntVec3(position.x + sizeX - 2, position.y, position.z - (sizeZ) + 7), whichMap);
            GenSpawn.Spawn(ThingDefOf.StandingLamp, new IntVec3(position.x + sizeX - 2, position.y, position.z - (sizeZ) + 8), whichMap);

            GenSpawn.Spawn(ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("TubeTelevision")), new IntVec3(position.x + sizeX - 2, position.y, position.z + (sizeZ) - 1), whichMap, new Rot4(2));
            GenSpawn.Spawn(ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("Armchair"), ThingDefOf.Cloth), new IntVec3(position.x + sizeX - 2, position.y, position.z + (sizeZ) - 3), whichMap, new Rot4(0));
            GenSpawn.Spawn(ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("Armchair"), ThingDefOf.Cloth), new IntVec3(position.x + sizeX - 3, position.y, position.z + (sizeZ) - 3), whichMap, new Rot4(0));
            GenSpawn.Spawn(ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("Armchair"), ThingDefOf.Cloth), new IntVec3(position.x + sizeX - 1, position.y, position.z + (sizeZ) - 3), whichMap, new Rot4(0));
            GenSpawn.Spawn(ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("Armchair"), ThingDefOf.Cloth), new IntVec3(position.x + sizeX - 3, position.y, position.z + (sizeZ) - 5), whichMap, new Rot4(0));

            GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.PlantPot, ThingDefOf.Cloth), new IntVec3(position.x + sizeX - 1, position.y, position.z + (sizeZ) - 5), whichMap, new Rot4(0));

            GenSpawn.Spawn(ThingMaker.MakeThing(DefDatabase<ThingDef>.GetNamed("ChessTable"), ThingDefOf.WoodLog), new IntVec3(position.x - sizeX + 1, position.y, position.z + (sizeZ) - 2), whichMap, new Rot4(0));
            GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.DiningChair, ThingDefOf.WoodLog), new IntVec3(position.x - sizeX + 1, position.y, position.z + (sizeZ) - 1), whichMap, new Rot4(2));
            GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.DiningChair, ThingDefOf.WoodLog), new IntVec3(position.x - sizeX + 1, position.y, position.z + (sizeZ) - 3), whichMap, new Rot4(0));

            GenSpawn.Spawn(ThingMaker.MakeThing(ThingDefOf.Cooler), new IntVec3(position.x - sizeX, position.y, position.z + (sizeZ) - 3), whichMap, new Rot4(3));
        }

        public static void SpawnWorkshop(Map whichMap, ThingDef stuff, IntVec3 position, int sizeX, int sizeZ)
        {
            MakeWall(whichMap, stuff, position, sizeX, sizeZ, true, 3, true);
        }

        public static void SpawnStorageRoom(Map whichMap, ThingDef stuff, IntVec3 baseLocation, bool floorIt = false, int type = 0, bool power = false)
        {
            MakeWall(whichMap, stuff, baseLocation, 5, 5, true, type, power);
        }
    }

}
