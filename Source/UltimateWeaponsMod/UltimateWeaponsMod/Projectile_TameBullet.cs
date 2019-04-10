using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;

namespace UltimateWeaponsMod
{
    class Projectile_TameBullet : Bullet
    {
        #region Properties
        //
        public Thingdef_TameBullet Def
        {
            get
            {
                return this.def as Thingdef_TameBullet;
            }
        }
        #endregion Properties
        #region Overrides
        protected override void Impact(Thing hitThing)
        {   
            //no need to run this since this isn't technically a firearm as far as the mechanics of how it work within the game code are concerned
            /*Remember to include Destroy(this); at the bottom though since that is included in the Impact() fuction
            and is required to not cause game-breaking glitches */           
            //base.Impact(hitThing);

            /*
             * Null checking is very important in RimWorld.
             * 99% of errors reported are from NullReferenceExceptions (NREs).
             * Make sure your code checks if things actually exist, before they
             * try to use the code that belongs to said things.
             */
            if (Def != null && hitThing != null && hitThing is Pawn hitPawn) //Fancy way to declare a variable inside an if statement. - Thanks Erdelf.
            {

                hitThing.SetFaction(Faction.OfPlayer);

            }

            //whether an actual pawn is hit or not, tame them all within the specified block
            if (this.launcher.Map != null && this.Position != null)
            {
                for (int g = -5; g < 5; g++)
                {
                    for (int i = -5; i < 5; i++)
                    {
                        IntVec3 positionWhatever = new IntVec3(this.Position.x + i, this.Position.y, this.Position.z + g);
                        Pawn pawnToTame = positionWhatever.GetFirstPawn(this.launcher.Map);
                        if (pawnToTame != null)
                        {
                            pawnToTame.SetFaction(Faction.OfPlayer);
                        }
                    }
                }
            }

            /*keep it from trying to run the code multiple times, for example once it hits the edge of the map or something
            Doing this is easier on the cpu (not that the impact function is all that much heavier but it makes a difference on my shit i5)*/           
            this.Destroy();
        }
        #endregion Overrides
    }
}
