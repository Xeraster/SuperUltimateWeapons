using System;
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
            //Log.Message("line 1", true);
            int randomNumber = rand111.Next(50, 1000);
            randomNumber = (int)Math.Round((randomNumber * 1.0f));
            //Log.Message("points = " + randomNumber, true);
            IncidentParms ind222 = new IncidentParms();
            //Log.Message("line 2", true);
            ind222.points = randomNumber;
            ind222.raidArrivalMode = PawnsArrivalModeDefOf.EdgeWalkIn;
            //Log.Message("line 3", true);
            ind222.raidStrategy = RaidStrategyDefOf.ImmediateAttack;
            //Log.Message("line 4", true);
            ind222.faction = Find.FactionManager.RandomEnemyFaction();
            ind222.pawnGroupMakerSeed = 122;
            //Log.Message("line 5", true);
            ind222.target = hitThing.Map;
            //Log.Message("this map to string = " + hitThing.Map.ToString(), true);
            //Log.Message("line 6", true);
            //IncidentDef indicent1 = IncidentDefOf.RaidEnemy;
            //Log.Message("line 7", true);
            //ind222.ExposeData();
            ind222.forced = true;
            IncidentDef indicent1 = (!ind222.faction.HostileTo(Faction.OfPlayer)) ? IncidentDefOf.RaidFriendly : IncidentDefOf.RaidEnemy;
            indicent1.baseChance = 1.0f;
            indicent1.Worker.TryExecute(ind222);
            //Log.Message("done", true);
            //well that was a pain in the ass.
            //}
        }
        #endregion Overrides
    }
}
