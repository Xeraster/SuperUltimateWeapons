using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace UltimateWeaponsMod
{
    class Projectile_CloneBullet : Bullet
    {
        #region Properties
        //
        public Thingdef_CloneBullet Def
        {
            get
            {
                return this.def as Thingdef_CloneBullet;
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

            //only do stuff if it is a pawn.
            if (Def != null && hitThing != null && hitThing is Pawn hitPawn) //Fancy way to declare a variable inside an if statement. - Thanks Erdelf.
            {
                var rand = Rand.Value; // This is a random percentage between 0% and 100%

                Map thisMap = hitThing.Map;
                PawnKindDef clonedPawnKindDef = hitPawn.kindDef;

                //Log.Message("pawnKindDef.ToString() = " + pawnKindDef.ToString(), true);
                Pawn newThing = PawnGenerator.GeneratePawn(clonedPawnKindDef);
                //newThing.gender = hitPawn.gender;
                Log.Message("ishuman = " + newThing.RaceProps.Humanlike, true);
                if (hitPawn.RaceProps.Humanlike)
                {
                    //newThing.kindDef.race = hitPawn.kindDef.race;
                    //make an exact copy of the hit pawn. I can't just set the pawn variables equal to each other because of random complications
                    //it's easier to do this than to work around the complications causing just "newthing = hitpawn" to not work
                    newThing.inventory = hitPawn.inventory;
                    newThing.RaceProps.body = hitPawn.RaceProps.body;
                    newThing.gender = hitPawn.gender;
                    newThing.skills = hitPawn.skills;
                    newThing.story.traits = hitPawn.story.traits;
                    newThing.apparel = hitPawn.apparel;
                    newThing.ageTracker = hitPawn.ageTracker;
                    newThing.SetFaction(hitPawn.Faction);
                    newThing.equipment = hitPawn.equipment;
                    newThing.story.hairDef = hitPawn.story.hairDef;
                    newThing.story.bodyType = hitPawn.story.bodyType;
                    newThing.story.melanin = hitPawn.story.melanin;
                    newThing.story.crownType = hitPawn.story.crownType;
                    newThing.story.hairColor = hitPawn.story.hairColor;
                    newThing.story.adulthood = hitPawn.story.adulthood;
                    newThing.story.childhood = hitPawn.story.childhood;

                    //fully heal the pawn. Give them a fresh start!
                    MiscCrap.FullyHealPawn(newThing);

                }
                else
                {
                    newThing.gender = hitPawn.gender;
                    newThing.SetFaction(hitPawn.Faction);
                }
                //Pawn newThing = hitPawn;
                //hitPawn.Name = NameGenerator.GenerateName()
                //newThing.records.AccumulateStoryEvent(StoryEventDef)
                IntVec3 clonedPawnPosition = new IntVec3(hitPawn.Position.x + (new Random().Next(-1, 1)), hitPawn.Position.y, hitPawn.Position.z + (new Random().Next(-1, 1)));
                GenSpawn.Spawn(newThing, clonedPawnPosition, thisMap);
                //Log.Message("newThing kind def = " + newThing.kindDef.ToString());
                //hitThing.Map.wildAnimalSpawner.SpawnRandomWildAnimalAt(hitThing.Position);
                //hitPawn.DeSpawn();
            }
            else
            {
                Pawn newPawn = PawnGenerator.GeneratePawn(DefDatabase<PawnKindDef>.GetNamed("Colonist"));
                if (this.launcher.Position.GetFirstPawn(this.launcher.Map) != null)
                {
                    newPawn.SetFaction(Faction.OfPlayer, this.launcher.Position.GetFirstPawn(this.launcher.Map));
                }
                else
                {
                    newPawn.SetFaction(Faction.OfPlayer);
                }
                MiscCrap.FilterTraits(newPawn);
                MiscCrap.FullyHealPawn(newPawn);
                GenSpawn.Spawn(newPawn, new IntVec3(base.Position.x + (new Random().Next(-1, 1)), base.Position.y, base.Position.z + (new Random().Next(-1, 1))), this.launcher.Map);
            }
        }
        #endregion Overrides
    }
}
