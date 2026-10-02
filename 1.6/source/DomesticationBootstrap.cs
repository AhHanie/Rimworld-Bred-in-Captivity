using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace Bred_in_Captivity
{
    [StaticConstructorOnStartup]
    public static class DomesticationBootstrap
    {
        static DomesticationBootstrap()
        {
            List<ThingDef> defs = DefDatabase<ThingDef>.AllDefsListForReading;
            for (int i = 0; i < defs.Count; i++)
            {
                ThingDef def = defs[i];
                if (def.race?.Animal != true || def.thingClass == null || !typeof(Pawn).IsAssignableFrom(def.thingClass))
                {
                    continue;
                }
                if (def.comps == null)
                {
                    def.comps = new List<CompProperties>();
                }
                if (!def.comps.Any(c => c is CompProperties_Domestication))
                {
                    def.comps.Add(new CompProperties_Domestication());
                }
            }

            StatDef wildness = StatDefOf.Wildness;
            if (wildness.parts == null)
            {
                wildness.parts = new List<StatPart>();
            }
            if (!wildness.parts.Any(p => p is StatPart_Domestication))
            {
                wildness.parts.Add(new StatPart_Domestication { parentStat = wildness });
            }
        }
    }
}
