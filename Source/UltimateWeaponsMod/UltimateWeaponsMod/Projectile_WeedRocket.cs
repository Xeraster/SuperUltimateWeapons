using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace UltimateWeaponsMod
{
    class Projectile_WeedRocket : Bullet
    {
        #region Properties
        //
        public Thingdef_WeedRocket Def
        {
            get
            {
                return this.def as Thingdef_WeedRocket;
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
            if (hitThing != null) //Fancy way to declare a variable inside an if statement. - Thanks Erdelf.
            {
                Log.Message("got into the if statement", true);
                Log.Warning("hitThing = " + hitThing.ToString(), true);
                var rand = Rand.Value; // This is a random percentage between 0% and 100%
                GenExplosion.DoExplosion(hitThing.Position, hitThing.Map, 3.9f, DamageDefOf.Bomb, this, 15, 0);
                GenClamor.DoClamor(this, 3.9f, ClamorDefOf.Impact);

                int hitPosX = hitThing.Position.x;
                int hitPosY = hitThing.Position.y;
                for (int g = -3; g < 3; g++)
                {
                    for (int i = -3; i < 3; i++)
                    {
                        IntVec3 positionWhatever = new IntVec3(hitThing.Position.x + i, hitThing.Position.y, hitThing.Position.z + g);
                        int beersToMake = Math.Abs(Math.Abs(g) - 3) + Math.Abs(Math.Abs(i) - 3);
                        for (int b = 0; b < beersToMake; b++)
                        {
                            GenSpawn.Spawn(ThingDefOf.SmokeleafJoint, positionWhatever, hitThing.Map);
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
                for (int g = -3; g < 3; g++)
                {
                    for (int i = -3; i < 3; i++)
                    {
                        IntVec3 positionWhatever = new IntVec3(this.Position.x + i, this.Position.y, this.Position.z + g);
                        int beersToMake = Math.Abs(Math.Abs(g) - 3) + Math.Abs(Math.Abs(i) - 3);
                        for (int b = 0; b < beersToMake; b++)
                        {
                            GenSpawn.Spawn(ThingDefOf.SmokeleafJoint, positionWhatever, this.launcher.Map);
                        }
                    }
                }
            }
        }
        #endregion Overrides
    }
}
