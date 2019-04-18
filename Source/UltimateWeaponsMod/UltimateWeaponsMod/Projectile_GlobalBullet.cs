using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace UltimateWeaponsMod
{
    class Projectile_GlobalBullet : Bullet
    {
        private static readonly Color FadeColor = Color.white;
        #region Properties
        //
        public Thingdef_GlobalBullet Def
        {
            get
            {
                return this.def as Thingdef_GlobalBullet;
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
            if (Def != null && hitThing != null && hitThing is Pawn hitPawn) //Fancy way to declare a variable inside an if statement. - Thanks Erdelf.
            {

            }
            SoundDefOf.PlanetkillerImpact.PlayOneShotOnCamera();
            ScreenFader.StartFade(FadeColor, 1f);
            ScreenFader.SetColor(Color.clear);
            GenGameEnd.EndGameDialogMessage("GameOverPlanetkillerImpact".Translate(Find.World.info.name), allowKeepPlaying: false, screenFillColor: Color.white);


        }

        public void JustWork()
        {

        }
        #endregion Overrides
    }
}
