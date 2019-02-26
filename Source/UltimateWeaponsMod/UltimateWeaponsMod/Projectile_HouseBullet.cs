using System;
using RimWorld;
using Verse;

namespace UltimateWeaponsMod
{
    class Projectile_HouseBullet : Bullet
    {
        #region Properties
        //
        public Thingdef_HouseBullet Def
        {
            get
            {
                return this.def as Thingdef_HouseBullet;
            }
        }
        #endregion Properties
        #region Overrides
        protected override void Impact(Thing hitThing)
        {
            base.Impact(hitThing);

            /*
             * Null checking is very important in RimWorld.
             * 99% of errors reported are from NullReferenceExceptions (NREs).
             * Make sure your code checks if things actually exist, before they
             * try to use the code that belongs to said things.
             */
            // if (Def != null && hitThing != null && hitThing is Pawn hitPawn) //Fancy way to declare a variable inside an if statement. - Thanks Erdelf.
            //{
            var rand = Rand.Value; // This is a random percentage between 0% and 100%
                                   //Log.Message("In the Projectile_RaidBullet code body", true);
                                   //DebugOutputsIncidents.RaidArrivemodeSampled();
                                   //DebugOutputsIncidents.RaidFactionSampled();
                                   //DebugOutputsIncidents.RaidStrategySampled();
            Random rand111 = new Random();
            //Log.Message("line 1", true);
            int randomNumber = rand111.Next(50, 1000);
            randomNumber = (int)Math.Round((randomNumber * 1.0f));
            //Log.Message("points = " + randomNumber, tru
            //}
            int hitPosX;
            int hitPosY;
            if (hitThing != null)
            {
                hitPosX = hitThing.Position.x;
                hitPosY = hitThing.Position.y;
            }
            else if (this.launcher.Map != null)
            {
                hitPosX = this.Position.x;
                hitPosY = this.Position.y;
            }
            ThingDef thingToSpawn;
            thingToSpawn = ThingDefOf.Wall;
            //Thing theThingToSpawn = ThingMaker.MakeThing(thingToSpawn);
            //Thing theThingToSpawn = new Thing();
            //theThingToSpawn.SetStuffDirect(ThingDefOf.Steel);
            Thing theThingToSpawn = ThingMaker.MakeThing(thingToSpawn, ThingDefOf.Steel);
            //theThingToSpawn.SetStuffDirect(ThingDefOf.Steel);
            Log.Message("stuff = " + theThingToSpawn.Stuff.ToString(), true);
            //gotta figure out how to designate this wall a material or else the Rimworld error log will have a field day
            IntVec3 positionWhatever = new IntVec3(this.Position.x, this.Position.y, this.Position.z);
            GenSpawn.Spawn(theThingToSpawn, positionWhatever, this.launcher.Map);
            BuildingSpawner.SpawnBuilding(this.launcher.Map, ThingDefOf.Steel, positionWhatever, true, 0);
            #endregion Overrides
        }
    }
}