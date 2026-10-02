using RimWorld;
using UnityEngine;
using Verse;

namespace Bred_in_Captivity
{
    public static class DomesticationUtility
    {
        public const float DefaultReduction = 0.08f;
        public const float MinReduction = 0.01f;
        public const float MaxReduction = 0.20f;
        public const float ReductionStep = 0.01f;

        public static float NormalizeStep(float value)
        {
            return Mathf.Clamp(RoundToStep(value), MinReduction, MaxReduction);
        }

        public static float RoundToStep(float value)
        {
            return Mathf.Round(value / ReductionStep) * ReductionStep;
        }

        public static bool IsColonyTamed(Pawn pawn)
        {
            return pawn != null
                && pawn.Faction == Faction.OfPlayer
                && pawn.training != null
                && pawn.training.HasLearned(TrainableDefOf.Tameness);
        }

        public static void ApplyToNewborn(Pawn newborn, Pawn mother, Pawn father)
        {
            if (mother == null || father == null || mother == father)
            {
                return;
            }
            if (mother.def != newborn.def || father.def != newborn.def)
            {
                return;
            }
            if (!IsColonyTamed(mother) || !IsColonyTamed(father))
            {
                return;
            }
            CompDomestication newbornComp = newborn.TryGetComp<CompDomestication>();
            if (newbornComp == null)
            {
                return;
            }
            CompDomestication motherComp = mother.TryGetComp<CompDomestication>();
            CompDomestication fatherComp = father.TryGetComp<CompDomestication>();
            int parentGeneration = Mathf.Max(motherComp?.Generation ?? 0, fatherComp?.Generation ?? 0);
            float parentReduction = Mathf.Max(motherComp?.WildnessReduction ?? 0f, fatherComp?.WildnessReduction ?? 0f);
            int generation = parentGeneration == int.MaxValue ? int.MaxValue : parentGeneration + 1;
            float baseWildness = Mathf.Max(0f, newborn.def.GetStatValueAbstract(StatDefOf.Wildness));
            float reduction = Mathf.Min(baseWildness, RoundToStep(parentReduction + Mod.Settings.ReductionPerGeneration));
            newbornComp.Set(generation, reduction);
        }
    }
}
