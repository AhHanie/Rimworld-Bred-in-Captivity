using HarmonyLib;
using RimWorld;
using Verse;

namespace Bred_in_Captivity
{
    [HarmonyPatch(typeof(PawnUtility), nameof(PawnUtility.TrySpawnHatchedOrBornPawn))]
    public static class Patch_PawnUtility_TrySpawnHatchedOrBornPawn
    {
        public static void Prefix(Pawn pawn, Thing motherOrEgg)
        {
            if (pawn == null || !pawn.RaceProps.Animal || !(motherOrEgg is Pawn birthingPawn))
            {
                return;
            }
            if (birthingPawn.health?.hediffSet?.GetFirstHediffOfDef(HediffDefOf.Pregnant) is HediffWithParents pregnancy)
            {
                DomesticationUtility.ApplyToNewborn(pawn, pregnancy.Mother ?? birthingPawn, pregnancy.Father);
            }
        }
    }
}
