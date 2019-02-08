using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using RimWorld;
using Verse;
using UnityEngine;
using Verse.Sound;

namespace UltimateWeaponsMod
{
    class Projectile_TestBullet : Bullet
    {
        #region Properties
        //
        public Thingdef_TestBullet Def
        {
            get
            {
                return this.def as Thingdef_TestBullet;
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
                    //Log.Message("this is before the thought command", true);
                    //ImpactSoundUtility.PlayImpactSound(hitThing, UW_SoundDefOf.healthSound, hitPawn.Map);

                    //the code below works, too. Only thing is that you have to find a way to tell it where each sound file is
                    //Log.Warning("current directory is: " + System.IO.Directory.GetCurrentDirectory(), true);
                    //System.Media.SoundPlayer player = new System.Media.SoundPlayer("fartsound.wav");
                    //player.Play();

                    //play the sound
                    UW_SoundDefOf.fartSound.PlayOneShot(new TargetInfo(base.Position, hitPawn.Map));
                    //SoundDefOf.Clock_Fast.PlayOneShot(new TargetInfo(base.Position, hitPawn.Map));
                    hitPawn.needs.mood.thoughts.memories.TryGainMemory(UW_ThoughtDefs.ifarted);
                    Log.Message("this is after the thought command", true);
                    //This checks to see if the character has a heal differential, or hediff on them already.
                    var plagueOnPawn = hitPawn?.health?.hediffSet?.GetFirstHediffOfDef(Def.HediffToAdd);
                    var randomSeverity = Rand.Range(0.15f, 0.30f);
                    if (plagueOnPawn != null)
                    {
                        //If they already have plague, add a random range to its severity.
                        //If severity reaches 1.0f, or 100%, plague kills the target.
                        //plagueOnPawn.Severity += randomSeverity;
                    }
                    else
                    {
                        //These three lines create a new health differential or Hediff,
                        //put them on the character, and increase its severity by a random amount.
                        //Hediff hediff = HediffMaker.MakeHediff(Def.HediffToAdd, hitPawn, null);
                        //hediff.Severity = randomSeverity;
                        //hitPawn.health.AddHediff(hediff, null, null);
                    }
                }
                else //failure!
                {
                    /*
                     * Motes handle all the smaller visual effects in RimWorld.
                     * Dust plumes, symbol bubbles, and text messages floating next to characters.
                     * This mote makes a small text message next to the character.
                     */
                    MoteMaker.ThrowText(hitThing.PositionHeld.ToVector3(), hitThing.MapHeld, "TST_TestBullet_FailureMote".Translate(Def.AddHediffChance), 12f);
                }
            }
        }
        #endregion Overrides
    }

}
