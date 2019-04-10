using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace UltimateWeaponsMod
{
    class Projectile_SkillBullet : Bullet
    {
        #region Properties
        //
        public Thingdef_SkillBullet Def
        {
            get
            {
                return this.def as Thingdef_SkillBullet;
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
                //Messages.Message("def name= " + UW_ThoughtDefs.UltimateWeaponsMod_ifarted.defName.ToString(), MessageTypeDefOf.PositiveEvent);
                // //Messages.Message("thought class= " + UW_ThoughtDefs.UltimateWeaponsMod_ifarted.thoughtClass.ToString(), MessageTypeDefOf.PositiveEvent);
                //Messages.Message("def package= " + UW_ThoughtDefs.UltimateWeaponsMod_ifarted.defPackage.ToString(), MessageTypeDefOf.PositiveEvent);
                //Messages.Message("Test", MessageTypeDefOf.NegativeHealthEvent);
                //Console.WriteLine("This is console writeline");

                Trait[] goodTraits = new Trait[7];
                TraitDef[] badTraitDefs = new TraitDef[16];

                goodTraits[0] = new Trait(TraitDefOf.Beauty, 1, true);
                goodTraits[1] = new Trait(TraitDefOf.GreatMemory, 0, true);
                goodTraits[2] = new Trait(TraitDefOf.Industriousness, 1, true);
                goodTraits[3] = new Trait(TraitDefOf.NaturalMood, 1, true);
                goodTraits[4] = new Trait(TraitDefOf.Tough, 0, true);
                goodTraits[5] = new Trait(TraitDefOf.Undergrounder, 0, true);
                goodTraits[6] = new Trait(TraitDefOf.Kind, 0, true);
                Random rnd1 = new Random();
                int randomGoodIndex = rnd1.Next(0, 6);

                badTraitDefs[0] = TraitDefOf.Abrasive;
                badTraitDefs[1] = TraitDefOf.AnnoyingVoice;
                badTraitDefs[2] = TraitDefOf.BodyPurist;
                badTraitDefs[3] = TraitDefOf.Brawler;
                badTraitDefs[4] = TraitDefOf.CreepyBreathing;
                badTraitDefs[5] = TraitDefOf.DislikesMen;
                badTraitDefs[6] = TraitDefOf.DislikesWomen;
                badTraitDefs[7] = TraitDefOf.DrugDesire;
                badTraitDefs[8] = TraitDefOf.Greedy;
                badTraitDefs[9] = TraitDefOf.Nerves;
                badTraitDefs[10] = TraitDefOf.PsychicSensitivity;
                badTraitDefs[11] = TraitDefOf.Pyromaniac;
                badTraitDefs[12] = TraitDefOf.SpeedOffset;
                badTraitDefs[13] = TraitDefOf.TooSmart;
                badTraitDefs[14] = TraitDefOf.Bloodlust;
                badTraitDefs[15] = TraitDefOf.NaturalMood;
                Random rnd2 = new Random();
                int randomBadIndex = rnd2.Next(0, 14);

                SkillDef[] skillList = new SkillDef[12];
                skillList[0] = SkillDefOf.Animals;
                skillList[1] = SkillDefOf.Artistic;
                skillList[2] = SkillDefOf.Construction;
                skillList[3] = SkillDefOf.Cooking;
                skillList[4] = SkillDefOf.Crafting;
                skillList[5] = SkillDefOf.Intellectual;
                skillList[6] = SkillDefOf.Medicine;
                skillList[7] = SkillDefOf.Melee;
                skillList[8] = SkillDefOf.Mining;
                skillList[9] = SkillDefOf.Plants;
                skillList[10] = SkillDefOf.Shooting;
                skillList[11] = SkillDefOf.Social;

                Random rnd = new Random();
                int random = rnd.Next(0, 11);

                Trait[] traitsToKeep = new Trait[5];
                int traitCounter = 0;
                if (hitPawn.IsColonist)
                {
                    //hitPawn.story.traits.allTraits.Clear();
                    foreach (Trait traits in hitPawn.story.traits.allTraits)
                    {
                        Log.Message("current trait is " + traits.Label, true);
                        bool everEqual = false;
                        for (int i = 0; i < 16; i++)
                        {
                            if (traits.def == badTraitDefs[i])
                            {
                                everEqual = true;
                            }
                            Log.Message("i = " + i, true);
                        }

                        if (everEqual == false)
                        {
                            if (traits.def == TraitDefOf.Tough)
                            {
                                traitsToKeep[traitCounter] = new Trait(TraitDefOf.Tough, 0, true);
                            }
                            else
                            {
                                traitsToKeep[traitCounter] = traits;
                            }
                            Log.Message("added " + traits.Label + " to the array. trait counter is " + traitCounter, true);
                            traitCounter++;
                        }
                    }
                    Log.Message("trait counter = " + traitCounter, true);

                    hitPawn.story.traits.allTraits.Clear();

                    Log.Message("cleared all traits from targetted pawn", true);

                    if (traitCounter == 2)
                    {
                        Log.Message("traitCounter = 2. Adding 3 traits at random", true);
                        hitPawn.story.traits.GainTrait(goodTraits[randomGoodIndex]);
                        Log.Message("1st trait is " + goodTraits[randomGoodIndex].Label, true);
                        randomGoodIndex = rnd1.Next(0, 6);
                        hitPawn.story.traits.GainTrait(goodTraits[randomGoodIndex]);
                        Log.Message("2nd trait is " + goodTraits[randomGoodIndex].Label, true);
                        randomGoodIndex = rnd1.Next(0, 6);
                        hitPawn.story.traits.GainTrait(goodTraits[randomGoodIndex]);
                        Log.Message("3rd trait is " + goodTraits[randomGoodIndex].Label, true);
                    }
                    else
                    {
                        Log.Message("traitCounter was not equal to 2. This means that 1 or more non-terrible trait was found on the pawns original trait list", true);
                        for (int y = 0; y < traitCounter; y++)
                        {
                            hitPawn.story.traits.GainTrait(traitsToKeep[y]);
                            Log.Message("A trait to keep that is being added is" + traitsToKeep[y], true);
                        }

                        for (int u = 0; u < 2 - traitCounter; u++)
                        {
                            hitPawn.story.traits.GainTrait(goodTraits[randomGoodIndex]);
                            Log.Message("An additional good trait that is being added at random is: " + goodTraits[randomGoodIndex].Label, true);
                        }
                    }
                    int firstNum = 0;
                    int secondNum = 0;
                    Log.Message("That is the end of the trait modification part", true);
                    //Trait newTrait = new Trait(TraitDefOf.Abrasive, 1, true);
                    //hitPawn.story.traits.allTraits.Clear();
                    //hitPawn.story.traits.GainTrait(goodTraits[randomGoodIndex]);
                    hitPawn.skills.Learn(skillList[random], 20000.0f, true);
                    firstNum = random;
                    random = rnd.Next(0, 11);
                    hitPawn.skills.Learn(skillList[random], 20000.0f, true);
                    secondNum = random;
                    random = rnd.Next(0, 11);
                    hitPawn.skills.Learn(skillList[random], 20000.0f, true);
                    MoteMaker.ThrowText(hitThing.PositionHeld.ToVector3(), hitThing.MapHeld, hitPawn.Name.ToString() + " has gain a bunch of xp in " + skillList[firstNum] + ", " + skillList[secondNum] + " and " + skillList[random], 12f);
                    }
                    else
                    {
                        //hitPawn.mindState.mentalStateHandler.TryStartMentalState(mentalArray[random], "Because the magic tree lady told " + hitPawn.gender.GetPronoun() + " to.", true, false);
                        MoteMaker.ThrowText(hitThing.PositionHeld.ToVector3(), hitThing.MapHeld, "this pawn is not a valid humanlike. The gun had no effect", 12f);
                    }
            }
        }
        #endregion Overrides
    }
}
