using Verse;

namespace Bred_in_Captivity
{
    public class ModSettings : Verse.ModSettings
    {
        private float reductionPerGeneration = DomesticationUtility.DefaultReduction;

        public float ReductionPerGeneration
        {
            get => reductionPerGeneration;
            set => reductionPerGeneration = DomesticationUtility.NormalizeStep(value);
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref reductionPerGeneration, "reductionPerGeneration", DomesticationUtility.DefaultReduction);
            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                reductionPerGeneration = DomesticationUtility.NormalizeStep(reductionPerGeneration);
            }
        }
    }
}
