using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace UltimateWeaponsMod
{
    class Projectile_AnimalRocket : Bullet
    {
        #region Properties
        //
        public Thingdef_AnimalRocket Def
        {
            get
            {
                return this.def as Thingdef_AnimalRocket;
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
            if (hitThing != null) //Fancy way to declare a variable inside an if statement. - Thanks Erdelf.
            {
                Log.Message("got into the if statement", true);
                Log.Warning("hitThing = " + hitThing.ToString(), true);
                var rand = Rand.Value; // This is a random percentage between 0% and 100%
                GenExplosion.DoExplosion(hitThing.Position, hitThing.Map, 3.9f, DamageDefOf.Bomb, this, 0, 0);
                GenClamor.DoClamor(this, 3.9f, ClamorDefOf.Impact);

                Map thisMap = hitThing.Map;
                PawnKindDef[] pawnKindDefArray;
                pawnKindDefArray = new PawnKindDef[DefDatabase<PawnKindDef>.DefCount];
                //PawnKindDef pawnKindDef = (from a in thisMap.Biome.AllWildAnimals
                //select a).RandomElementByWeight((PawnKindDef def) => thisMap.Biome.CommonalityOfAnimal(def) / def.wildGroupSize.Average);

                int pawnsCount = 0;
                foreach (PawnKindDef kindDef in DefDatabase<PawnKindDef>.AllDefs)
                {
                    if (!kindDef.ToString().Contains("Mech_") && !kindDef.ToString().Contains("Mercenary_") && !kindDef.ToString().Contains("Grenadier_") && !kindDef.ToString().Contains("Tribal_")
                        && !kindDef.ToString().Contains("Town") && !kindDef.ToString().Contains("Drifter") && kindDef.ToString() != "WildMan" && kindDef.ToString() != "Scavenger" && kindDef.ToString() != "Thrasher"
                        && !kindDef.ToString().Contains("Pirate") && kindDef.ToString() != "Colonist" && kindDef.ToString() != "Slave" && kindDef.ToString() != "AncientSoldier" 
                        && kindDef.ToString() != "Villager" && kindDef.ToString() != "Tribesperson" && kindDef.ToString() != "SpaceRefugee" && kindDef.ToString() != "StrangerInBlack")
                    {

                        Log.Message("pawn kid (for each loop) = " + kindDef.ToString(), true);
                        //if (kindDef.ToString() == "Cat")
                        //{
                        pawnKindDefArray[pawnsCount] = kindDef;
                        pawnsCount++;
                    }
                    //}
                }
                Random r = new Random();
                int rInt = r.Next(0, pawnsCount - 2);
                int hitPosX = hitThing.Position.x;
                int hitPosY = hitThing.Position.y;
                for (int g = -3; g < 3; g++)
                {
                    for (int i = -3; i < 3; i++)
                    {
                        IntVec3 positionWhatever = new IntVec3(hitThing.Position.x + i, hitThing.Position.y, hitThing.Position.z + g);
                        Pawn newThing = PawnGenerator.GeneratePawn(pawnKindDefArray[rInt]);
                        GenSpawn.Spawn(newThing, positionWhatever, hitThing.Map);
                        rInt = r.Next(0, pawnsCount - 1);
                    }
                }
            }
            else if (this.launcher.Map != null)
            {
                var rand = Rand.Value; // This is a random percentage between 0% and 100%
                GenExplosion.DoExplosion(this.Position, this.launcher.Map, 3.9f, DamageDefOf.Bomb, this, 0, 0);
                GenClamor.DoClamor(this, 3.9f, ClamorDefOf.Impact);

                Map thisMap = this.launcher.Map;
                PawnKindDef[] pawnKindDefArray;
                pawnKindDefArray = new PawnKindDef[DefDatabase<PawnKindDef>.DefCount];
                //PawnKindDef pawnKindDef = (from a in thisMap.Biome.AllWildAnimals
                //select a).RandomElementByWeight((PawnKindDef def) => thisMap.Biome.CommonalityOfAnimal(def) / def.wildGroupSize.Average);

                int pawnsCount = 0;
                foreach (PawnKindDef kindDef in DefDatabase<PawnKindDef>.AllDefs)
                {
                    if (!kindDef.ToString().Contains("Mech_") && !kindDef.ToString().Contains("Mercenary_") && !kindDef.ToString().Contains("Grenadier_") && !kindDef.ToString().Contains("Tribal_")
                        && !kindDef.ToString().Contains("Town") && !kindDef.ToString().Contains("Drifter") && kindDef.ToString() != "WildMan" && kindDef.ToString() != "Scavenger" && kindDef.ToString() != "Thrasher"
                        && !kindDef.ToString().Contains("Pirate") && kindDef.ToString() != "Colonist" && kindDef.ToString() != "Slave" && kindDef.ToString() != "AncientSoldier"
                        && kindDef.ToString() != "Villager" && kindDef.ToString() != "Tribesperson" && kindDef.ToString() != "SpaceRefugee" && kindDef.ToString() != "StrangerInBlack")
                    {

                        Log.Message("pawn kid (for each loop) = " + kindDef.ToString(), true);
                        //if (kindDef.ToString() == "Cat")
                        //{
                        pawnKindDefArray[pawnsCount] = kindDef;
                        pawnsCount++;
                    }
                    //}
                }
                Random r = new Random();
                int rInt = r.Next(0, pawnsCount - 1);
                int hitPosX = this.Position.x;
                int hitPosY = this.Position.y;
                for (int g = -3; g < 3; g++)
                {
                    for (int i = -3; i < 3; i++)
                    {
                        IntVec3 positionWhatever = new IntVec3(this.Position.x + i, this.Position.y, this.Position.z + g);
                        Pawn newThing = PawnGenerator.GeneratePawn(pawnKindDefArray[rInt]);
                        GenSpawn.Spawn(newThing, positionWhatever, thisMap);
                        rInt = r.Next(0, pawnsCount - 2);
                    }
                }
            }
        }
        #endregion Overrides
    }
}
