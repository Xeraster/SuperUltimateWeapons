using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace UltimateWeaponsMod
{
    class Projectile_BeerRocket : Bullet
    {
        #region Properties
        //
        public Thingdef_BeerRocket Def
        {
            get
            {
                return this.def as Thingdef_BeerRocket;
            }
        }
        #endregion Properties
        #region Overrides
        protected override void Impact(Thing hitThing)
        {
            //base.Impact(hitThing);

            /*
             * Null checking is very important in RimWorld.
             * 99% of errors reported are from NullReferenceExceptions (NREs).
             * Make sure your code checks if things actually exist, before they
             * try to use the code that belongs to said things.
             */
            if (hitThing != null) //Fancy way to declare a variable inside an if statement. - Thanks Erdelf.
            {
                Log.Message("got into the if statement", true);
                Log.Warning("hitThing = " + hitThing.ToString() + "also, 42", true);
                Log.Warning("base.Position = " + base.Position.ToString(), true);
                Log.Warning("this.launcher.Map = " + this.launcher.Map.ToString(), true);
                Log.Warning("DamageDefOf.Bomb = " + DamageDefOf.Bomb.ToString(), true);
                Log.Warning("this.launcher = " + this.launcher.ToString(), true);
                //Log.Warning("Def.damageAmountBase = " + Def.damageAmountBase.ToString(), true);
                //var rand = Rand.Value; // This is a random percentage between 0% and 100%
                GenExplosion.DoExplosion(base.Position, this.launcher.Map, 3.9f, DamageDefOf.Bomb, this.launcher, 0, 0);
                //GenClamor.DoClamor(hitThing, 3.9f, ClamorDefOf.Impact);
                Log.Warning("executed explosion line. about to go to for loop", true);

                //int hitPosX = hitThing.Position.x;
                //int hitPosY = hitThing.Position.y;
                for (int g = -3; g < 3; g++)
                {
                    for (int i = -3; i < 3; i++)
                    {
                        IntVec3 positionWhatever = new IntVec3(base.Position.x + i, base.Position.y, base.Position.z + g);
                        int beersToMake = Math.Abs(Math.Abs(g) - 3) + Math.Abs(Math.Abs(i) - 3);
                        for (int b = 0; b < beersToMake; b++)
                        {
                            GenSpawn.Spawn(ThingDefOf.Beer, positionWhatever, hitThing.Map);
                        }
                    }
                }
                Log.Warning("exited for loop. About to run destroy", true);
                this.Destroy();
            }
            else/*(this.launcher.Map != null)*/
            {
                Log.Message("In the \"if hitthing == null\" loop", true);
                GenExplosion.DoExplosion(base.Position, this.launcher.Map, 3.9f, DamageDefOf.Bomb, this.launcher, 0, 0);
                GenClamor.DoClamor(this, 3.9f, ClamorDefOf.Impact);

                int hitPosX = this.Position.x;
                int hitPosY = this.Position.y;
                for (int g = -3; g < 3; g++)
                {
                    for (int i = -3; i < 3; i++)
                    {
                        IntVec3 positionWhatever = new IntVec3(base.Position.x + i, base.Position.y, base.Position.z + g);
                        int beersToMake = Math.Abs(Math.Abs(g) - 3) + Math.Abs(Math.Abs(i) - 3);
                        for (int b = 0; b < beersToMake; b++)
                        {
                            GenSpawn.Spawn(ThingDefOf.Beer, positionWhatever, this.launcher.Map);
                        }
                    }
                }
                this.Destroy();
            }
            //this.Destroy();

        }
        #endregion Overrides
    }
}
