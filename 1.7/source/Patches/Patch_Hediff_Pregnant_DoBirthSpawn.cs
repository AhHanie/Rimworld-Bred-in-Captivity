using HarmonyLib;
using RimWorld;
using Verse;

namespace Bred_in_Captivity
{
    // DoBirthSpawn receives both parents directly, so wrapping its one TrySpawnHatchedOrBornPawn call
    // needs no lookup of the pregnancy hediff to find them.
    [HarmonyPatch(typeof(Hediff_Pregnant), nameof(Hediff_Pregnant.DoBirthSpawn))]
    public static class Patch_Hediff_Pregnant_DoBirthSpawn
    {
        [HarmonyInfix(typeof(PawnUtility), nameof(PawnUtility.TrySpawnHatchedOrBornPawn))]
        public static void Prefix(Pawn pawn, [HarmonyOuter] Pawn mother, [HarmonyOuter] Pawn father)
        {
            DomesticationUtility.ApplyToNewborn(pawn, mother, father);
        }
    }
}
