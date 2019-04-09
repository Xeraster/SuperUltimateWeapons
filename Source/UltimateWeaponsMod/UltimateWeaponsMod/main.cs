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

    public class Thingdef_HouseBullet : ThingDef
    {
        public float buildingWealthMultiplier = 1.00f; //base multiplier to be applied when calculating a random.
    }

    public class Thingdef_MentalHealthBullet : ThingDef
    {
        public float AddHediffChance = 0.95f; //The default chance of adding a hediff.
        public HediffDef HediffToAdd = HediffDefOf.Plague;
    }

    public class Thingdef_CatBullet : ThingDef
    {
        public int catSpawning = 1;//1 = "explode" cats everywhere upon impact. 0 = do not "explode" cats everywhere upon impact
        public int catRadius = 3;//INTEGERS ONLY (this means whole numbers kiddies). Roughly the radius of which to spawn a shit ton of cats
    }

    public class Thingdef_BeerRocket : ThingDef
    {
        public string damageDef = "Bomb";
        public int damageAmountBase = 15;
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
    }

    public class Thingdef_SuperNukeBullet : ThingDef
    {

    }

}
