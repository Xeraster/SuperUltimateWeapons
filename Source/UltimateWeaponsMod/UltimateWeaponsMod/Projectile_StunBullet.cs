using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace UltimateWeaponsMod
{
    class Projectile_StunBullet : Bullet
    {
        #region Properties
        //
        public Thingdef_StunBullet Def
        {
            get
            {
                return this.def as Thingdef_StunBullet;
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
                /*
                 * Messages.Message flashes a message on the top of the screen. 
                 * You may be familiar with this one when a colonist dies, because
                 * it makes a negative sound and mentioneds "So and so has died of _____".
                 * 
                 * Here, we're using the "Translate" function. More on that later in
                 * the localization section.
                 */
                //Hediff newhediff = new Hediff();
                //newhediff.sourceHediffDef = HediffDefOf.Anesthetic;
                //hitPawn.health.hediffSet.hediffs.Add(newhediff);


                Hediff hediff = HediffMaker.MakeHediff(HediffDefOf.Anesthetic, hitPawn, null);
                hediff.Severity = 50;
                hitPawn.health.AddHediff(hediff, null, null);
            }
        }
        #endregion Overrides
    }
}
