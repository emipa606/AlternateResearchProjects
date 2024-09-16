using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Verse;

namespace AlternateResearchProjects;

[StaticConstructorOnStartup]
public class AlternateResearchProjects
{
    private static readonly
        Dictionary<ResearchProjectDef, Dictionary<ResearchProjectDef, List<ResearchProjectDef>>>
        alternateDictionary;

    static AlternateResearchProjects()
    {
        new Harmony("Mlie.AlternateResearchProjects").PatchAll(Assembly.GetExecutingAssembly());
        alternateDictionary = [];

        foreach (var researchProjectDef in DefDatabase<ResearchProjectDef>.AllDefs.Where(def =>
                     def.HasModExtension<AlternateResearchModExtension>()))
        {
            var alternateResearchProjectsDefs = researchProjectDef.GetModExtension<AlternateResearchModExtension>();
            var alternates = new Dictionary<ResearchProjectDef, List<ResearchProjectDef>>();
            foreach (var alternateResearchProject in alternateResearchProjectsDefs.alternateResearchProjects)
            {
                alternates.Add(alternateResearchProject.prerequisite, alternateResearchProject.alternates);
            }

            alternateDictionary.Add(researchProjectDef, alternates);
        }

        Log.Message(
            $"[AlternateResearchProjects]: Added alternate prerequisites for {alternateDictionary.Count} research projects");
    }

    public static bool TryGetAlternateResearch(ResearchProjectDef researchProjectDef,
        out Dictionary<ResearchProjectDef, List<ResearchProjectDef>> alternates)
    {
        return alternateDictionary.TryGetValue(researchProjectDef, out alternates);
    }

    public static bool IsAnyAlternateComplete(ResearchProjectDef researchProjectDef)
    {
        if (!alternateDictionary.TryGetValue(researchProjectDef, out var alternates))
        {
            return false;
        }

        if (researchProjectDef.prerequisites != null)
        {
            foreach (var prerequisite in researchProjectDef.prerequisites)
            {
                if (prerequisite.IsFinished)
                {
                    continue;
                }

                if (!alternates.TryGetValue(prerequisite, out var alternateList))
                {
                    continue;
                }

                var alternateDone = false;

                foreach (var projectDef in alternateList)
                {
                    if (!projectDef.IsFinished)
                    {
                        continue;
                    }

                    alternateDone = true;
                    break;
                }

                if (!alternateDone)
                {
                    return false;
                }
            }
        }

        if (researchProjectDef.hiddenPrerequisites == null)
        {
            return true;
        }

        foreach (var prerequisite in researchProjectDef.hiddenPrerequisites)
        {
            if (prerequisite.IsFinished)
            {
                continue;
            }

            if (!alternates.TryGetValue(prerequisite, out var alternateList))
            {
                continue;
            }

            var alternateDone = false;

            foreach (var projectDef in alternateList)
            {
                if (!projectDef.IsFinished)
                {
                    continue;
                }

                alternateDone = true;
                break;
            }

            if (!alternateDone)
            {
                return false;
            }
        }

        return true;
    }
}