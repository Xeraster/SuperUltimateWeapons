using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using RimWorld;
using Verse;

namespace UltimateWeaponsMod
{
    class Projectile_SafetyBullet : Bullet
    {
        #region Properties
        //
        public Thingdef_SafetyBullet Def
        {
            get
            {
                return this.def as Thingdef_SafetyBullet;
            }
        }
        #endregion Properties
        #region Overrides
        //public Hediff mostRecentHealedHediff;
        protected override void Impact(Thing hitThing)
        {

            /*
             * Null checking is very important in RimWorld.
             * 99% of errors reported are from NullReferenceExceptions (NREs).
             * Make sure your code checks if things actually exist, before they
             * try to use the code that belongs to said things.
             */
            Map map = this.launcher.Map;
            //Log.Message("before the impact code", true);
            //base.Impact(hitThing);
            //Log.Message("after impact code", true);
            
            if (Def != null && hitThing != null && hitThing is Pawn hitPawn) //Fancy way to declare a variable inside an if statement. - Thanks Erdelf.
            {
                //don't hurt any pawn in the player faction
                int numHediffs = 0;
                int numGoodHediffs = 0;
                if (hitPawn.IsColonist && hitPawn.Faction.def == FactionDefOf.PlayerColony)
                {
                    //if the targeted pawn is in a mental state, solve their mental state
                    if (hitPawn.InMentalState == true)
                    {
                        hitPawn.MentalState.RecoverFromState();
                        MoteMaker.ThrowText(hitThing.PositionHeld.ToVector3(), hitThing.MapHeld, hitPawn.Name.ToString() + "  is no longer in a mental state", 12f);
                    }
                    else
                    {
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
                        //if the targeted pawn has neither a mental state, nor health issues, give them a positive thought
                        else
                        {
                            Log.Message("trying to assign happy thought to pawn", true);
                            hitPawn.needs.mood.thoughts.memories.TryGainMemoryFast(UW_ThoughtDefs.mischappypoopyeahpickle);
                            //Why did I call it that? Well I called it that in the xml file to prevent possible conflicts with other mods. No one in their right mind would name their thought variable something that ridiculous
                        }

                    }

                }
                else 
                {
                    //float amount = Def.damageToDeal;
                    //Map map = base.Map;
                    //base.Impact(hitThing);
                    BattleLogEntry_RangedImpact battleLogEntry_RangedImpact = new BattleLogEntry_RangedImpact(base.launcher, hitThing, intendedTarget.Thing, base.equipmentDef, def, targetCoverDef);
                    Find.BattleLog.Add(battleLogEntry_RangedImpact);
                    if (hitThing != null)
                    {
                        DamageDef damageDef = def.projectile.damageDef;
                        float amount = Def.damageToDeal;
                        float armorPenetration = base.ArmorPenetration;
                        Vector3 eulerAngles = ExactRotation.eulerAngles;
                        float y = eulerAngles.y;
                        //Thing launcher = base.launcher;
                        //ThingDef equipmentDef = base.equipmentDef;
                        DamageInfo dinfo = new DamageInfo(damageDef, amount, armorPenetration, y, launcher, null, equipmentDef, DamageInfo.SourceCategory.ThingOrUnknown, intendedTarget.Thing);
                        hitThing.TakeDamage(dinfo).AssociateWithLog(battleLogEntry_RangedImpact);
                        //Pawn pawn = hitThing as Pawn;
                        //if (pawn != null && pawn.stances != null && pawn.BodySize <= def.projectile.StoppingPower + 0.001f)
                        //{
                          //  pawn.stances.StaggerFor(95);
                        //}
                    }
                    else
                    {
                        MoteMaker.MakeStaticMote(ExactPosition, map, ThingDefOf.Mote_ShotHit_Dirt);
                        if (base.Position.GetTerrain(map).takeSplashes)
                        {
                            MoteMaker.MakeWaterSplash(ExactPosition, map, Mathf.Sqrt(base.DamageAmount) * 1f, 4f);
                        }
                    }
                }
                /*
                 * Messages.Message flashes a message on the top of the screen. 
                 * You may be familiar with this one when a colonist dies, because
                 * it makes a negative sound and mentioneds "So and so has died of _____".
                 * 
                 * Here, we're using the "Translate" function. More on that later in
                 * the localization section.
                 */
            }
            else if(hitThing != null)
            {
                //float amount = Def.damageToDeal;
                //Map map = base.Map;
                //base.Impact(hitThing);
                BattleLogEntry_RangedImpact battleLogEntry_RangedImpact = new BattleLogEntry_RangedImpact(base.launcher, hitThing, intendedTarget.Thing, base.equipmentDef, def, targetCoverDef);
                Find.BattleLog.Add(battleLogEntry_RangedImpact);
                if (hitThing != null)
                {
                    DamageDef damageDef = def.projectile.damageDef;
                    float amount = Def.damageToDeal;
                    float armorPenetration = base.ArmorPenetration;
                    Vector3 eulerAngles = ExactRotation.eulerAngles;
                    float y = eulerAngles.y;
                    //Thing launcher = base.launcher;
                    //ThingDef equipmentDef = base.equipmentDef;
                    DamageInfo dinfo = new DamageInfo(damageDef, amount, armorPenetration, y, launcher, null, equipmentDef, DamageInfo.SourceCategory.ThingOrUnknown, intendedTarget.Thing);
                    hitThing.TakeDamage(dinfo).AssociateWithLog(battleLogEntry_RangedImpact);
                    //Pawn pawn = hitThing as Pawn;
                    //if (pawn != null && pawn.stances != null && pawn.BodySize <= def.projectile.StoppingPower + 0.001f)
                    //{
                    //  pawn.stances.StaggerFor(95);
                    //}
                }
                else
                {
                    MoteMaker.MakeStaticMote(ExactPosition, map, ThingDefOf.Mote_ShotHit_Dirt);
                    if (base.Position.GetTerrain(map).takeSplashes)
                    {
                        MoteMaker.MakeWaterSplash(ExactPosition, map, Mathf.Sqrt(base.DamageAmount) * 1f, 4f);
                    }
                }
            }
            this.Destroy();
        }
        #endregion Overrides
    }
}
