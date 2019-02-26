using System;
using RimWorld;
using Verse;

namespace UltimateWeaponsMod
{
    public static class ActuallyUsefulFunctions
    {
        public static void SetGround(IntVec3 location, TerrainDef groundType, Map map)
        {
            if (groundType == null)
            {
                Log.Error("Tried to set terrain at " + location + " to null.");
                return;
            }
            if (Current.ProgramState == ProgramState.Playing)
            {
                map.designationManager.DesignationAt(location, DesignationDefOf.SmoothFloor)?.Delete();
            }
            int num = map.cellIndices.CellToIndex();
            if (groundType.layerable)
            {
                if (underGrid[num] == null)
                {
                    if (topGrid[num].passability != Traversability.Impassable)
                    {
                        underGrid[num] = topGrid[num];
                    }
                    else
                    {
                        underGrid[num] = TerrainDefOf.Sand;
                    }
                }
            }
            else
            {
                underGrid[num] = null;
            }
            topGrid[num] = groundType;
            //DoTerrainChangedEffects(c);
        }
    }
}
