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

        public static void Postfix(Pawn __instance, ref float? __result)
        {
            if (__result.HasValue && DomesticationUtility.IsPenFree(__instance))
            {
                __result = null;
            }
        }
    }
}
