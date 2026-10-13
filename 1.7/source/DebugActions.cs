using System.Collections.Generic;
using System.Linq;
using LudeonTK;
using RimWorld;
using Verse;

namespace Bred_in_Captivity
{
    public static class DebugActions
    {
        private const float NearTermSeverity = 0.99f;

        [DebugAction("Bred in Captivity", "Make pregnant (click mother, pick father)", actionType = DebugActionType.ToolMapForPawns, allowedGameStates = AllowedGameStates.PlayingOnMap)]
        private static void MakePregnant(Pawn mother)
        {
            if (!mother.RaceProps.Animal || mother.gender != Gender.Female)
            {
                Reject("Click a female animal.");
                return;
            }
            if (mother.health.hediffSet.HasHediff(HediffDefOf.Pregnant))
            {
                Reject(mother.LabelShort + " is already pregnant.");
                return;
            }

            List<DebugMenuOption> options = new List<DebugMenuOption>
            {
                new DebugMenuOption("(no father)", DebugMenuOptionMode.Action, () => StartPregnancy(mother, null))
            };
            IEnumerable<Pawn> fathers = mother.Map.mapPawns.AllPawnsSpawned
                .Where(p => p != mother && p.RaceProps.Animal && p.gender == Gender.Male)
                .OrderByDescending(p => p.def == mother.def)
                .ThenByDescending(p => DomesticationUtility.IsColonyTamed(p));
            foreach (Pawn father in fathers)
            {
                Pawn localFather = father;
                options.Add(new DebugMenuOption(Describe(localFather), DebugMenuOptionMode.Action, () => StartPregnancy(mother, localFather)));
            }
            Find.WindowStack.Add(new Dialog_DebugOptionListLister(options));
        }

        private static void StartPregnancy(Pawn mother, Pawn father)
        {
            Hediff_Pregnant pregnancy = (Hediff_Pregnant)HediffMaker.MakeHediff(HediffDefOf.Pregnant, mother);
            pregnancy.SetParents(null, father, null);
            mother.health.AddHediff(pregnancy);
            pregnancy.Severity = NearTermSeverity;
            DebugActionsUtility.DustPuffFrom(mother);
        }

        private static string Describe(Pawn pawn)
        {
            CompDomestication comp = pawn.TryGetComp<CompDomestication>();
            string tamed = DomesticationUtility.IsColonyTamed(pawn) ? "tamed" : "not tamed";
            string generation = comp != null ? "gen " + comp.Generation : "no comp";
            return pawn.LabelShort + " (" + pawn.KindLabel + ", " + tamed + ", " + generation + ")";
        }

        private static void Reject(string text)
        {
            Messages.Message(text, MessageTypeDefOf.RejectInput, historical: false);
        }
    }
}
