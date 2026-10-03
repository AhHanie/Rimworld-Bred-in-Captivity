using UnityEngine;
using Verse;

namespace Bred_in_Captivity
{
    public static class ModSettingsWindow
    {
        public static void Draw(Rect parent)
        {
            ModSettings settings = Mod.Settings;
            Listing_Standard listing = new Listing_Standard();
            listing.Begin(parent);

            int points = Mathf.RoundToInt(settings.ReductionPerGeneration * 100f);
            listing.Label("BredinCaptivity.ReductionLabel".Translate(points));
            settings.ReductionPerGeneration = Widgets.HorizontalSlider(
                listing.GetRect(22f),
                settings.ReductionPerGeneration,
                DomesticationUtility.MinReduction,
                DomesticationUtility.MaxReduction,
                roundTo: DomesticationUtility.ReductionStep);
            listing.Gap(6f);
            Text.Font = GameFont.Tiny;
            listing.Label("BredinCaptivity.ReductionDesc".Translate());
            Text.Font = GameFont.Small;
            listing.Gap(12f);
            if (listing.ButtonText("BredinCaptivity.ResetDefault".Translate()))
            {
                settings.ReductionPerGeneration = DomesticationUtility.DefaultReduction;
            }

            listing.GapLine(12f);
            bool penFree = settings.PenFreeDomesticated;
            listing.CheckboxLabeled(
                "BredinCaptivity.PenFreeLabel".Translate(),
                ref penFree,
                "BredinCaptivity.PenFreeTooltip".Translate());
            settings.SetPenFreeDomesticated(penFree);

            listing.End();
        }
    }
}
