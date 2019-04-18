using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace UltimateWeaponsMod
{
    class Projectile_HealthBullet : Bullet
    {
        #region Properties
        //
        public Thingdef_HealthBullet Def
        {
            get
            {
                return this.def as Thingdef_HealthBullet;
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
                /*
                 * Messages.Message flashes a message on the top of the screen. 
                 * You may be familiar with this one when a colonist dies, because
                 * it makes a negative sound and mentioneds "So and so has died of _____".
                 * 
                 * Here, we're using the "Translate" function. More on that later in
                 * the localization section.
                 */

                Log.Message("this is before the headiff command", true);
                Log.Message(hitPawn.health.summaryHealth.ToString(), true);
                int numHediffs = 0;
                int numGoodHediffs = 0;

                foreach (Hediff hediff in hitPawn.health.hediffSet.hediffs)
                {
                    Hediff localH = hediff;
                    if (hediff.Label.Contains("bionic") || hediff.Label.Contains("clone") || hediff.Label.Contains("joywire"))
                    {
                        Log.Message("found good hediff. It's: " + hediff.Label, true);
                        numGoodHediffs++;
                        //don't count these. We WANT them in pretty much any situation (when using this gun at least)
                        //this is important so that some idiot pawn doesn't "heal" someone with a bionic eye or a joywire
                    }
                    else
                    {
                        //Log.Message("found hediff. It's: " + hediff.Label, true);
                        numHediffs++;
                    }
                    //numHediffs++;
                }

                Hediff[] hediffArray = new Hediff[numGoodHediffs];
                Log.Message("found " + numHediffs + "undesirable headiffs", true);
                /*now we have the number of hediffs to get rid of.
                //this may be redundant and slow, but it works on my shitty ivy bridge i5 with an intel HD 3000 without
                causing lag or memory leaks so if anyone has problems, get a computer that was made during the
                current decade (2019 as of this writing) and try again                      
                */

                //if the targeted pawn is NOT in a mental state, solve their health aliaments
                if (numHediffs > 0)
                {
                    int goodHeadiffIndex = 0;
                    foreach (Hediff hediff in hitPawn.health.hediffSet.hediffs)
                    {
                        Hediff localH = hediff;
                        if (hediff.Label.Contains("bionic") || hediff.Label.Contains("clone") || hediff.Label.Contains("joywire"))
                        {
                            hediffArray[goodHeadiffIndex] = hediff;
                            goodHeadiffIndex++;
                            //don't count these. We WANT them in pretty much any situation (when using this gun at least)
                            //this is important so that some idiot pawn doesn't "heal" someone with a bionic eye or a joywire
                        }
                        else
                        {
                            //Log.Message("removing " + hediff.Label, true);
                            //hediff.Heal(100.0f);
                            //hitPawn.health.hediffSet.hediffs.RemoveAll<Hediff>();

                            //don't add any of the "bad" hediffs
                        }
                    }
                    for (int u = 0; u < numHediffs + numGoodHediffs; u++)
                    {
                        Log.Message("removing a hediff", true);
                        hitPawn.health.hediffSet.hediffs.RemoveLast<Hediff>();
                    }
                    //good fucking god man. Why in the holy name of fuck did they have to use a janky ass list tree for this???
                    //honest question
                    for (int i = 0; i < goodHeadiffIndex; i++)
                    {
                        Log.Message("adding:" + hediffArray[i].Label, true);
                        hitPawn.health.hediffSet.hediffs.Add(hediffArray[i]);
                    }
                    MoteMaker.ThrowText(hitThing.PositionHeld.ToVector3(), hitThing.MapHeld, "Healed of everything", 12f);
                }

                /*foreach(Hediff hediff in hitPawn.health.hediffSet.hediffs)
                {
                    Hediff localH = hediff;
                    numHediffs++;
                }
                if (numHediffs > 0)
                {
                    mostRecentHealedHediff = hitPawn.health.hediffSet.hediffs.Last<Hediff>();
                    hitPawn.health.hediffSet.hediffs.RemoveLast<Hediff>();
                    MoteMaker.ThrowText(hitThing.PositionHeld.ToVector3(), hitThing.MapHeld, "Healed " + hitPawn.Name.ToString() + " of " + mostRecentHealedHediff.Label, 12f);
                }
                else
                {
                    MoteMaker.ThrowText(hitThing.PositionHeld.ToVector3(), hitThing.MapHeld, hitPawn.Name.ToString() + " is in perfect health!", 12f);
                }
                Log.Message("this is after the hediff command got executed", true);
                //Log.Message("this is after the thought command", true);
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
                }*/
            }
        }
        #endregion Overrides
    }
}
