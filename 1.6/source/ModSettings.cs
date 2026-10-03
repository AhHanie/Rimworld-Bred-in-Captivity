using Verse;

namespace Bred_in_Captivity
{
    public class ModSettings : Verse.ModSettings
    {
        private float reductionPerGeneration = DomesticationUtility.DefaultReduction;
        private bool penFreeDomesticated;

        // Read once while patching, so changing it requires a restart.
        public bool PenFreeDomesticated => penFreeDomesticated;

        public void SetPenFreeDomesticated(bool value)
        {
            penFreeDomesticated = value;
        }

        public float ReductionPerGeneration
        {
            get => reductionPerGeneration;
            set => reductionPerGeneration = DomesticationUtility.NormalizeStep(value);
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref reductionPerGeneration, "reductionPerGeneration", DomesticationUtility.DefaultReduction);
            Scribe_Values.Look(ref penFreeDomesticated, "penFreeDomesticated", false);
            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                reductionPerGeneration = DomesticationUtility.NormalizeStep(reductionPerGeneration);
            }
        }
    }
}
