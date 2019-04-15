using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
namespace UltimateWeaponsMod
{
    public static class MiscCrap
    {
        public static bool IsBuilingHere(Map whichMap, IntVec3 location)
        {
            if (location.GetFirstBuilding(whichMap) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void FilterTraits(Pawn whichPawn)
        {
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
            if (whichPawn.IsColonist)
            {
                //whichPawn.story.traits.allTraits.Clear();
                foreach (Trait traits in whichPawn.story.traits.allTraits)
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

                whichPawn.story.traits.allTraits.Clear();

                Log.Message("cleared all traits from targetted pawn", true);

                if (traitCounter == 2)
                {
                    Log.Message("traitCounter = 2. Adding 3 traits at random", true);
                    whichPawn.story.traits.GainTrait(goodTraits[randomGoodIndex]);
                    Log.Message("1st trait is " + goodTraits[randomGoodIndex].Label, true);
                    randomGoodIndex = rnd1.Next(0, 6);
                    whichPawn.story.traits.GainTrait(goodTraits[randomGoodIndex]);
                    Log.Message("2nd trait is " + goodTraits[randomGoodIndex].Label, true);
                    randomGoodIndex = rnd1.Next(0, 6);
                    whichPawn.story.traits.GainTrait(goodTraits[randomGoodIndex]);
                    Log.Message("3rd trait is " + goodTraits[randomGoodIndex].Label, true);
                }
                else
                {
                    Log.Message("traitCounter was not equal to 2. This means that 1 or more non-terrible trait was found on the pawns original trait list", true);
                    for (int y = 0; y < traitCounter; y++)
                    {
                        whichPawn.story.traits.GainTrait(traitsToKeep[y]);
                        Log.Message("A trait to keep that is being added is" + traitsToKeep[y], true);
                    }

                    for (int u = 0; u < 2 - traitCounter; u++)
                    {
                        whichPawn.story.traits.GainTrait(goodTraits[randomGoodIndex]);
                        Log.Message("An additional good trait that is being added at random is: " + goodTraits[randomGoodIndex].Label, true);
                    }
                }
                int firstNum = 0;
                int secondNum = 0;
                Log.Message("That is the end of the trait modification part", true);
                //Trait newTrait = new Trait(TraitDefOf.Abrasive, 1, true);
                //whichPawn.story.traits.allTraits.Clear();
                //whichPawn.story.traits.GainTrait(goodTraits[randomGoodIndex]);
                whichPawn.skills.Learn(skillList[random], 20000.0f, true);
                firstNum = random;
                random = rnd.Next(0, 11);
                whichPawn.skills.Learn(skillList[random], 20000.0f, true);
                secondNum = random;
                random = rnd.Next(0, 11);
                whichPawn.skills.Learn(skillList[random], 20000.0f, true);
            }
            else
            {
                //whichPawn.mindState.mentalStateHandler.TryStartMentalState(mentalArray[random], "Because the magic tree lady told " + whichPawn.gender.GetPronoun() + " to.", true, false);
            }
        }

        public static void FullyHealPawn(Pawn thePawn)
        {
            Log.Message("this is before the headiff command", true);
            Log.Message(thePawn.health.summaryHealth.ToString(), true);
            int numHediffs = 0;
            int numGoodHediffs = 0;

            foreach (Hediff hediff in thePawn.health.hediffSet.hediffs)
            {
                Hediff localH = hediff;
                if (hediff.Label.Contains("bionic") || hediff.Label.Contains("clone") || hediff.Label.Contains("joywire"))
                {

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
            /*now we have the number of hediffs to get rid of.
            //this may be redundant and slow, but it works on my shitty ivy bridge i5 with an intel HD 3000 without
            causing lag or memory leaks so if anyone has problems, get a computer that was made during the
            current decade (2019 as of this writing) and try again                      
            */

            //if the targeted pawn is NOT in a mental state, solve their health aliaments
            if (numHediffs > 0)
            {
                int goodHeadiffIndex = 0;
                foreach (Hediff hediff in thePawn.health.hediffSet.hediffs)
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
                        //thePawn.health.hediffSet.hediffs.RemoveAll<Hediff>();

                        //don't add any of the "bad" hediffs
                    }
                }
                for (int u = 0; u < numHediffs + numGoodHediffs; u++)
                {
                    thePawn.health.hediffSet.hediffs.RemoveLast<Hediff>();
                }
                //good fucking god man. Why in the holy name of fuck did they have to use a janky ass list tree for this???
                //honest question
                for (int i = 0; i < goodHeadiffIndex; i++)
                {
                    thePawn.health.hediffSet.hediffs.Add(hediffArray[i]);
                }
            }
        }
    }
}
