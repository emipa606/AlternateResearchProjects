using System.Linq;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace AlternateResearchProjects;

[HarmonyPatch(typeof(MainTabWindow_Research), nameof(MainTabWindow_Research.DrawResearchPrerequisites))]
public static class MainTabWindow_Research_DrawResearchPrerequisites
{
    private static readonly Color fulfilledPrerequisiteColor = Color.green;
    private static readonly Color missingPrerequisiteColor = ColorLibrary.RedReadable;

    public static bool Prefix(Rect rect, ref float y, ResearchProjectDef project)
    {
        if (!AlternateResearchProjects.TryGetAlternateResearch(project, out var alternates))
        {
            return true;
        }

        Widgets.Label(rect, ref y, $"{"Prerequisites".Translate()}:");
        rect.xMin += 6f;
        foreach (var prerequisite in project.prerequisites)
        {
            if (!alternates.TryGetValue(prerequisite, out var prerequisiteAlternates))
            {
                setPrerequisiteStatusColor(prerequisite.IsFinished);
                Widgets.Label(rect, ref y, $"- {prerequisite.LabelCap}");
                continue;
            }

            setPrerequisiteStatusColor(prerequisite.IsFinished || prerequisiteAlternates.Any(def => def.IsFinished));
            Widgets.Label(rect, ref y,
                $"- {prerequisite.LabelCap}/{string.Join("/", prerequisiteAlternates.Select(def => def.LabelCap))}");
        }

        if (project.hiddenPrerequisites != null)
        {
            foreach (var hiddenPrerequisite in project.hiddenPrerequisites)
            {
                setPrerequisiteStatusColor(hiddenPrerequisite.IsFinished);
                Widgets.Label(rect, ref y, $"- {hiddenPrerequisite.LabelCap}");
            }
        }

        GUI.color = Color.white;
        return false;
    }

    private static void setPrerequisiteStatusColor(bool present)
    {
        GUI.color = present ? fulfilledPrerequisiteColor : missingPrerequisiteColor;
    }
}