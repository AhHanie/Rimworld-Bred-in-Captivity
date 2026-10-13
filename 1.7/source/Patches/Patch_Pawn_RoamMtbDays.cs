using HarmonyLib;
using Verse;

namespace Bred_in_Captivity
{
    // Pawn.Roamer, FenceBlocked, rope management and allowed-area support all derive from this getter,
    // so clearing it makes a pen-free animal behave as a non-roamer everywhere.
    [HarmonyPatch(typeof(Pawn), nameof(Pawn.RoamMtbDays), MethodType.Getter)]
    public static class Patch_Pawn_RoamMtbDays
    {
        // Skip patching entirely when the setting is off; the setting is only read at startup.
        public static bool Prepare()
        {
            return Mod.Settings.PenFreeDomesticated;
        }

        // Wraps only the read of the race's roam interval, so the getter's earlier "hediff removes roaming"
        // return and pawns without race properties never reach this code.
        [HarmonyInfix(typeof(RaceProperties), nameof(RaceProperties.roamMtbDays), InnerTargetKind.FieldRead)]
        public static float? Postfix(float? __result, [HarmonyOuter] Pawn __instance)
        {
            return __result.HasValue && DomesticationUtility.IsPenFree(__instance) ? null : __result;
        }
    }
}
