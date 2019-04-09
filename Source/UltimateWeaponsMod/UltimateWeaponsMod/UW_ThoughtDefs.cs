using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

[DefOf]
    public static class UW_ThoughtDefs
    {
        public static ThoughtDef ifarted;

        public static ThoughtDef mischappypoopyeahpickle;

        static UW_ThoughtDefs()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(ThoughtDefOf));
        }
    }
