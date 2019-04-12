using System;
using RimWorld;
using Verse;

namespace UltimateWeaponsMod
{
    class Projectile_SuperNukeBullet : Bullet
    {
        #region Properties
        //
        public Thingdef_SuperNukeBullet Def
        {
            get
            {
                return this.def as Thingdef_SuperNukeBullet;
            }
        }
        #endregion Properties
        #region Overrides
        protected override void Impact(Thing hitThing)
        {
            base.Impact(hitThing);
            Map thisMap = this.launcher.Map;
            int sizex = thisMap.Size.x;
            int sizey = thisMap.Size.y;
            int sizez = thisMap.Size.z;

            float explosionRadius = Def.explodeClusterSize;
            int totalRadius = Def.totalRadius;
            int clusterFrequency = Def.clusterFrequency;
            int damageToDo = Def.damageToDo;

            IntVec3 explodePosition;
            if (hitThing != null)
            {
                explodePosition = new IntVec3(hitThing.Position.x, hitThing.Position.y, hitThing.Position.z); 
            }
            else
            {
                explodePosition = new IntVec3(base.Position.x, this.launcher.Position.y, base.launcher.Position.z);
            }

            Random rnd = new Random();
            int randomChance = rnd.Next(0, 1000);
            for (int x = (explodePosition.x - totalRadius); x < (explodePosition.x + totalRadius); x++)
            {
                for (int z = (explodePosition.z - totalRadius); z < (explodePosition.z + totalRadius); z++)
                {
                    if (z > 0 && z < sizez && x > 0 && x < sizex)
                    {
                        if (randomChance > clusterFrequency)
                        {

                            GenExplosion.DoExplosion(new IntVec3(x, this.launcher.Position.y, z), this.launcher.Map, explosionRadius, DamageDefOf.Bomb, this, damageToDo, damageToDo);
                            GenClamor.DoClamor(this, explosionRadius, ClamorDefOf.Impact);
                        }

                        if (randomChance < 120)
                        {
                            //GenSpawn.Spawn(ThingDefOf.Fire, new IntVec3(x, this.launcher.Position.y, z), thisMap);
                            Fire fireToStart = (Fire)ThingMaker.MakeThing(ThingDefOf.Fire);
                            fireToStart.fireSize = Fire.MaxFireSize;
                            GenSpawn.Spawn(fireToStart, new IntVec3(x, this.launcher.Position.y, z), thisMap);
                        }
                        if (randomChance > 120 && randomChance < 130)
                        {
                            GenSpawn.Spawn(ThingDefOf.Filth_Dirt, new IntVec3(x, this.launcher.Position.y, z), thisMap);
                        }
                        if (randomChance > 130 && randomChance < 260)
                        {
                            GenSpawn.Spawn(ThingDefOf.Filth_Ash, new IntVec3(x, this.launcher.Position.y, z), thisMap);
                        }
                    }
                    randomChance = rnd.Next(0, 1000);
                }
            }
            //Log.Message("done", true);
            //well that was a pain in the ass.
            //}
        }
        #endregion Overrides
    }
}
