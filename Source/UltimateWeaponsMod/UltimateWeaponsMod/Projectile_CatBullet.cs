using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace UltimateWeaponsMod
{
    class Projectile_CatBullet : Bullet
    {
        #region Properties
        //
        public Thingdef_CatBullet Def
        {
            get
            {
                return this.def as Thingdef_CatBullet;
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
                    /*
                     * Messages.Message flashes a message on the top of the screen. 
                     * You may be familiar with this one when a colonist dies, because
                     * it makes a negative sound and mentioneds "So and so has died of _____".
                     * 
                     * Here, we're using the "Translate" function. More on that later in
                     * the localization section.
                     */

                Map thisMap = hitThing.Map;
                PawnKindDef pawnKindDef = (from a in thisMap.Biome.AllWildAnimals
                                           select a).RandomElementByWeight((PawnKindDef def) => thisMap.Biome.CommonalityOfAnimal(def) / def.wildGroupSize.Average);

                foreach (PawnKindDef kindDef in DefDatabase<PawnKindDef>.AllDefs)
                {
                    //Log.Message("pawn kid (for each loop) = " + kindDef.ToString(), true);
                    if (kindDef.ToString() == "Cat")
                    {
                        pawnKindDef = kindDef;
                    }
                }

                //Log.Message("pawnKindDef.ToString() = " + pawnKindDef.ToString(), true);
                Pawn newThing = PawnGenerator.GeneratePawn(pawnKindDef);
                GenSpawn.Spawn(newThing, hitThing.Position, thisMap);
                Log.Message("newThing kind def = " + newThing.kindDef.ToString());
                //hitThing.Map.wildAnimalSpawner.SpawnRandomWildAnimalAt(hitThing.Position);
                hitPawn.DeSpawn();
                //pawnToCreate.ChangeType
                    //Log.Message("this is after the hediff command got executed", true);
                    //Log.Message("this is after the thought command", true);
                    //This checks to see if the character has a heal differential, or hediff on them already.
            }
        }
        #endregion Overrides
    }
}
