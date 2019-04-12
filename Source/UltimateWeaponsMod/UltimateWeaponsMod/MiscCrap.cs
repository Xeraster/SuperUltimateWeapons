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
    }
}
