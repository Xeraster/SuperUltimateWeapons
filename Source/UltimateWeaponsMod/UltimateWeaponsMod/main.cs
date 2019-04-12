using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

namespace UltimateWeaponsMod
{
    public class Thingdef_TestBullet : ThingDef
    {
        public float AddHediffChance = 0.95f; //The default chance of adding a hediff.
        public HediffDef HediffToAdd = HediffDefOf.Plague;
    }
    public class Thingdef_HealthBullet : ThingDef
    {
        public float AddHediffChance = 0.95f; //The default chance of adding a hediff.
        public HediffDef HediffToAdd = HediffDefOf.Plague;
    }

    public class Thingdef_StunBullet : ThingDef
    {
        public float DeathChance = 0.01f;
    }

    public class Thingdef_KillBullet : ThingDef
    {
        public float DeathChance = 0.01f;//doesn't actually do anything
    }

    public class Thingdef_TameBullet : ThingDef
    {
        public int TameRadius = 5;//doesn't actually do anything
    }

    public class Thingdef_SafetyBullet : ThingDef
    {
        public float OP_ness = 0.95f; //The degree of how op this is
        public int HealDegree = 1;//description pending
        public float damageToDeal = 10.0f;
    }

    public class Thingdef_RaidBullet : ThingDef
    {
        public float raidSeverityMultiplier = 1.00f; //base multiplier to be applied when calculating a random.
    }

    public class Thingdef_TrumpRocket : ThingDef
    {
        public int wallVariable = 1; //this literally doesn't do anything. I just put it there in case I decide to use it for something later
    }

    public class Thingdef_HouseBullet : ThingDef
    {
        public float buildingWealthMultiplier = 1.00f; //base multiplier to be applied when calculating a random.
    }

    public class Thingdef_MentalHealthBullet : ThingDef
    {
        public float AddHediffChance = 0.95f; //The default chance of adding a hediff.
        public HediffDef HediffToAdd = HediffDefOf.Plague;
    }

    public class Thingdef_SkillBullet : ThingDef
    {
        //public float AddHediffChance = 0.95f; //The default chance of adding a hediff.
        //public HediffDef HediffToAdd = HediffDefOf.Plague;
    }

    public class Thingdef_CatBullet : ThingDef
    {
        public int catSpawning = 1;//1 = "explode" cats everywhere upon impact. 0 = do not "explode" cats everywhere upon impact
        public int catRadius = 3;//INTEGERS ONLY (this means whole numbers kiddies). Roughly the radius of which to spawn a shit ton of cats
    }

    public class Thingdef_BeerRocket : ThingDef
    {
        public string damageDef = "Bomb";
        public int damageAmountBase = 0;
        public float explosionRadius = 2.9f;
        public int speed = 50;
    }

    public class Thingdef_WeedRocket : ThingDef
    {
        public string damageDef = "Bomb";
        public int damageAmountBase = 15;
        public float explosionRadius = 2.9f;
        public int speed = 50;
    }

    public class Thingdef_AnimalRocket : ThingDef
    {
        public string damageDef = "Bomb";
        public int damageAmountBase = 15;
        public float explosionRadius = 2.9f;
        public int speed = 50;
    }

    public class Thingdef_EverythingRocket : ThingDef
    {
        public string damageDef = "Bomb";
        public int damageAmountBase = 15;
        public float explosionRadius = 2.9f;
        public int speed = 50;

        //these are the 2 that actually do something
        public bool limitItemValue = true;//whether or not to put a limit on the max monetary value per item that can be spawned
        public int maxValueLimit = 2000;/*The max value an item can be and still be allowed to be spawned. I had to impose a tunable limit
        because otherwise, the gun spawns a bunch of orbital bombardment remotes, AI cores and Super Nukes making the base value go
        up to a gazillion on the first shot. This causes the raids to get ridiculous*/
        //body parts and the joywire are immune to this limit

        public bool limitQtyBodyParts = true;
    }

    public class Thingdef_UsefulRocket : ThingDef
    {
        public int damageAmountBase = 15;
    }

    public class Thingdef_CatSwarm : ThingDef
    {
        public string damageDef = "Bomb";
        public int damageAmountBase = 15;
        public float explosionRadius = 2.9f;
        public int speed = 50;
    }

    public class Thingdef_BadCatSwarm : ThingDef
    {
        public string damageDef = "Bomb";
        public int damageAmountBase = 15;
        public float explosionRadius = 2.9f;
        public int speed = 50;
    }

    public class Thingdef_SuperNukeBullet : ThingDef
    {
        public float explodeClusterSize = 12.0f;//the size of each clustered explosion blast
        public int totalRadius = 50;//the total spawn radius of the cluster blasts. 50 is enough to destroy half the map
        public int clusterFrequency = 982;//how dense to spawn cluster blasts. Lower numbers = more clusters 1000 will spawn no explosions. 0 will spawn an explosion on every block within the area of effect (lag)
        public int damageToDo = 100000;
    }

}
