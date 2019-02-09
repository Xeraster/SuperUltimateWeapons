using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace UltimateWeaponsMod
{
    class Projectile_RaidBullet : Bullet
    {
        #region Properties
        //
        public Thingdef_RaidBullet Def
        {
            get
            {
                return this.def as Thingdef_RaidBullet;
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
           // if (Def != null && hitThing != null && hitThing is Pawn hitPawn) //Fancy way to declare a variable inside an if statement. - Thanks Erdelf.
            //{
                var rand = Rand.Value; // This is a random percentage between 0% and 100%
                //Log.Message("In the Projectile_RaidBullet code body", true);
                //DebugOutputsIncidents.RaidArrivemodeSampled();
                //DebugOutputsIncidents.RaidFactionSampled();
                //DebugOutputsIncidents.RaidStrategySampled();
                Random rand111 = new Random();
                int randomNumber = rand111.Next(50, 1000);
                randomNumber = (int)Math.Round((randomNumber * Def.raidSeverityMultiplier));
                Log.Message("points = " + randomNumber, true);
                IncidentParms ind222 = new IncidentParms();
                ind222.points = randomNumber;
                ind222.raidArrivalMode = PawnsArrivalModeDefOf.EdgeWalkIn;
                ind222.raidStrategy = RaidStrategyDefOf.ImmediateAttack;
                ind222.faction = Faction.OfAncientsHostile;
                ind222.pawnGroupMakerSeed = 122;
                ind222.target = this.Map;
                IncidentDef indicent1 = IncidentDefOf.RaidEnemy;
                bool didItWork = indicent1.Worker.TryExecute(ind222);
                Log.Message("indicent1 = " + didItWork, true);
            //}
        }
        #endregion Overrides
    }
}
