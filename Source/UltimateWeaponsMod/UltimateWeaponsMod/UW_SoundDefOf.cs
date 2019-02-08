using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;

[DefOf]
    public static class UW_SoundDefOf
    {
    public static SoundDef fartSound;

    public static SoundDef healthSound;

    static UW_SoundDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(SoundDefOf));
    }
}
