using System;
using RimWorld;
using Verse;

namespace UltimateWeaponsMod
{
    class Projectile_TrumpRocket : Bullet
    {
        #region Properties
        //
        public Thingdef_TrumpRocket Def
        {
            get
            {
                return this.def as Thingdef_TrumpRocket;
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

            //it doesn't matter if this hits anything. The only thing that matters is one shot = 1 wall regardless of what is being aimed at or hit

            Log.Message("map size x= " + this.launcher.Map.Size.x.ToString(), true);
            Log.Message("map size y= " + this.launcher.Map.Size.y.ToString(), true);
            Log.Message("map size z= " + this.launcher.Map.Size.z.ToString(), true);

            int sizex = this.launcher.Map.Size.x;
            int sizey = this.launcher.Map.Size.y;
            int sizez = this.launcher.Map.Size.z;

            IntVec3 mapCenter = new IntVec3(sizex / 2, sizey, sizez / 2);

            ThingDef thingToSpawn;
            thingToSpawn = ThingDefOf.Wall;
            Thing theThingToSpawn = ThingMaker.MakeThing(thingToSpawn, ThingDefOf.Steel);

            //spawns a wall 4 layers thick
            BuildingSpawner.MakeWall(this.launcher.Map, ThingDefOf.Steel, mapCenter, (sizex / 2) - 15, (sizez / 2) - 15, false, 6);
            BuildingSpawner.MakeWall(this.launcher.Map, ThingDefOf.Steel, mapCenter, (sizex / 2) - 16, (sizez / 2) - 16, false, 6);
            BuildingSpawner.MakeWall(this.launcher.Map, ThingDefOf.Steel, mapCenter, (sizex / 2) - 17, (sizez / 2) - 17, false, 6);
            BuildingSpawner.MakeWall(this.launcher.Map, ThingDefOf.Steel, mapCenter, (sizex / 2) - 18, (sizez / 2) - 18, false, 6);

        }
        #endregion Overrides
    }
}
