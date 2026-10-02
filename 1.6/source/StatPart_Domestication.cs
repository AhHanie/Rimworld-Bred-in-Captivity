using RimWorld;
using UnityEngine;
using Verse;

namespace Bred_in_Captivity
{
    public class StatPart_Domestication : StatPart
    {
        public override void TransformValue(StatRequest req, ref float val)
        {
            CompDomestication comp = GetComp(req);
            if (comp != null)
            {
                val = Mathf.Max(0f, val - comp.WildnessReduction);
            }
        }

        public override string ExplanationPart(StatRequest req)
        {
            CompDomestication comp = GetComp(req);
            if (comp == null)
            {
                return null;
            }
            return "BredinCaptivity.StatExplanation".Translate(comp.Generation, comp.ReductionPoints).Resolve();
        }

        private static CompDomestication GetComp(StatRequest req)
        {
            if (!(req.Thing is Pawn pawn) || !pawn.RaceProps.Animal)
            {
                return null;
            }
            CompDomestication comp = pawn.TryGetComp<CompDomestication>();
            return comp != null && comp.WildnessReduction > 0f ? comp : null;
        }
    }
}
