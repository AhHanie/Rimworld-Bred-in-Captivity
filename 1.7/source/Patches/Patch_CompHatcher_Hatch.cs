using HarmonyLib;
using RimWorld;
using Verse;

namespace Bred_in_Captivity
{
    [HarmonyPatch(typeof(CompHatcher), nameof(CompHatcher.Hatch))]
    public static class Patch_CompHatcher_Hatch
    {
        [HarmonyInfix(typeof(PawnUtility), nameof(PawnUtility.TrySpawnHatchedOrBornPawn))]
        public static void Prefix(Pawn pawn, [HarmonyOuter] CompHatcher __instance)
        {
            DomesticationUtility.ApplyToNewborn(pawn, __instance.hatcheeParent, __instance.otherParent);
        }
    }
}
