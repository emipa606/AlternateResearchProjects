using HarmonyLib;
using Verse;

namespace AlternateResearchProjects;

[HarmonyPatch(typeof(ResearchProjectDef), nameof(ResearchProjectDef.PrerequisitesCompleted), MethodType.Getter)]
public static class ResearchProjectDef_PrerequisitesCompleted
{
    [HarmonyPostfix]
    public static void Postfix(ref bool __result, ResearchProjectDef __instance)
    {
        if (__result)
        {
            return;
        }

        __result = AlternateResearchProjects.IsAnyAlternateComplete(__instance);
    }
}