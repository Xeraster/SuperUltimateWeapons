using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace UltimateWeaponsMod
{
    class Projectile_MentalHealthBullet : Bullet
    {
        #region Properties
        //
        public Thingdef_MentalHealthBullet Def
        {
            get
            {
                return this.def as Thingdef_MentalHealthBullet;
            }
        }
        #endregion Properties
        #region Overrides
        public Hediff mostRecentHealedHediff;
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
                var rand = Rand.Value; // This is a random percentage between 0% and 100%
                if (rand <= Def.AddHediffChance) // If the percentage falls under the chance, success!
                {
                    /*
                     * Messages.Message flashes a message on the top of the screen. 
                     * You may be familiar with this one when a colonist dies, because
                     * it makes a negative sound and mentioneds "So and so has died of _____".
                     * 
                     * Here, we're using the "Translate" function. More on that later in
                     * the localization section.
                     */
                    //Messages.Message("def name= " + UW_ThoughtDefs.UltimateWeaponsMod_ifarted.defName.ToString(), MessageTypeDefOf.PositiveEvent);
                    // //Messages.Message("thought class= " + UW_ThoughtDefs.UltimateWeaponsMod_ifarted.thoughtClass.ToString(), MessageTypeDefOf.PositiveEvent);
                    //Messages.Message("def package= " + UW_ThoughtDefs.UltimateWeaponsMod_ifarted.defPackage.ToString(), MessageTypeDefOf.PositiveEvent);
                    //Messages.Message("Test", MessageTypeDefOf.NegativeHealthEvent);
                    //Console.WriteLine("This is console writeline");
                    Random rnd = new Random();
                    int random = rnd.Next(0, 5);
                    MentalStateDef[] mentalArray = new MentalStateDef[6];
                    mentalArray[0] = MentalStateDefOf.Manhunter;
                    mentalArray[1] = MentalStateDefOf.SocialFighting;
                    mentalArray[2] = MentalStateDefOf.Wander_OwnRoom;
                    mentalArray[3] = MentalStateDefOf.Wander_Psychotic;
                    mentalArray[4] = MentalStateDefOf.Wander_Sad;
                    mentalArray[5] = MentalStateDefOf.Berserk;
                    if (hitPawn.InMentalState == true)
                    {
                        hitPawn.MentalState.RecoverFromState();
                        MoteMaker.ThrowText(hitThing.PositionHeld.ToVector3(), hitThing.MapHeld, hitPawn.Name.ToString() + "  is no longer in a mental state", 12f);
                    }
                    else
                    {
                        hitPawn.mindState.mentalStateHandler.TryStartMentalState(mentalArray[random], "Because the magic tree lady told " + hitPawn.gender.GetPronoun() + " to.", true, false);
                        MoteMaker.ThrowText(hitThing.PositionHeld.ToVector3(), hitThing.MapHeld, hitPawn.Name.ToString() + " has been affected with " + mentalArray[random].defName, 12f);
                    }
                }
                else //failure!
                {
                    /*
                     * Motes handle all the smaller visual effects in RimWorld.
                     * Dust plumes, symbol bubbles, and text messages floating next to characters.
                     * This mote makes a small text message next to the character.
                     */
                    MoteMaker.ThrowText(hitThing.PositionHeld.ToVector3(), hitThing.MapHeld, "missed", 12f);
                }
            }
        }
        #endregion Overrides
    }
}
