using HarmonyLib;
using UnityEngine;
using Verse;

namespace Bred_in_Captivity
{
    public class Mod : Verse.Mod
    {
        public static ModSettings Settings { get; private set; }

        public Mod(ModContentPack content) : base(content)
        {
            Settings = GetSettings<ModSettings>();
            new Harmony("sk.bredincaptivity").PatchAll();
        }

        public override string SettingsCategory()
        {
            return "BredinCaptivity.SettingsTitle".Translate();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            ModSettingsWindow.Draw(inRect);
            base.DoSettingsWindowContents(inRect);
        }
    }
}
