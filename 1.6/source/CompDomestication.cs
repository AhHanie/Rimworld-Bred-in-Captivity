using UnityEngine;
using Verse;

namespace Bred_in_Captivity
{
    public class CompDomestication : ThingComp
    {
        private int domesticationGeneration;
        private float wildnessReduction;

        public int Generation => domesticationGeneration;

        public float WildnessReduction => wildnessReduction;

        public int ReductionPoints => Mathf.RoundToInt(wildnessReduction * 100f);

        internal void Set(int generation, float reduction)
        {
            domesticationGeneration = Mathf.Max(0, generation);
            wildnessReduction = Mathf.Clamp01(reduction);
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref domesticationGeneration, "bredInCaptivityGeneration", 0);
            Scribe_Values.Look(ref wildnessReduction, "bredInCaptivityReduction", 0f);
            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                domesticationGeneration = Mathf.Max(0, domesticationGeneration);
                wildnessReduction = Mathf.Clamp01(wildnessReduction);
            }
        }

        public override string CompInspectStringExtra()
        {
            if (wildnessReduction <= 0f)
            {
                return null;
            }
            return "BredinCaptivity.InspectString".Translate(domesticationGeneration, ReductionPoints).Resolve();
        }
    }
}
