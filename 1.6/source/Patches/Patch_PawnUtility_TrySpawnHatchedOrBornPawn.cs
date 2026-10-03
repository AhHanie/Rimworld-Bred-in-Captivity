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
            if (pawn == null || motherOrEgg == null || !pawn.RaceProps.Animal)
            {
                return;
            }
            if (motherOrEgg is Pawn birthingPawn)
            {
                if (birthingPawn.health?.hediffSet?.GetFirstHediffOfDef(HediffDefOf.Pregnant) is HediffWithParents pregnancy)
                {
                    DomesticationUtility.ApplyToNewborn(pawn, pregnancy.Mother ?? birthingPawn, pregnancy.Father);
                }
                return;
            }
            CompHatcher hatcher = motherOrEgg.TryGetComp<CompHatcher>();
            if (hatcher != null)
            {
                DomesticationUtility.ApplyToNewborn(pawn, hatcher.hatcheeParent, hatcher.otherParent);
            }
        }
    }
}
