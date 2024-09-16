using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace AlternateResearchProjects;

[HarmonyPatch(typeof(MainTabWindow_Research), "ListProjects")]
public static class MainTabWindow_Research_ListProjects
{
    public static void Prefix(MainTabWindow_Research __instance, ResearchProjectDef ___selectedProject)
    {
        var visibleResearchProjects = __instance.VisibleResearchProjects;
        var start = default(Vector2);
        var end = default(Vector2);
        for (var i = 0; i < 2; i++)
        {
            foreach (var item in visibleResearchProjects)
            {
                if (item.tab != __instance.CurTab)
                {
                    continue;
                }

                if (!AlternateResearchProjects.TryGetAlternateResearch(item, out var alternates))
                {
                    continue;
                }

                var num = 0f;
                if (ModsConfig.AnomalyActive && item.knowledgeCategory != null)
                {
                    num = 14f;
                }

                start.x = __instance.PosX(item);
                start.y = __instance.PosY(item) + 25f;
                for (var j = 0; j < item.prerequisites.CountAllowNull(); j++)
                {
                    var itemPrerequisite = item.prerequisites[j];

                    if (!alternates.TryGetValue(itemPrerequisite, out var prerequisiteAlternates))
                    {
                        continue;
                    }

                    foreach (var alternatePrerequisite in prerequisiteAlternates)
                    {
                        if (alternatePrerequisite == null || alternatePrerequisite.tab != __instance.CurTab)
                        {
                            continue;
                        }

                        var rect = new Rect(__instance.PosX(alternatePrerequisite),
                            __instance.PosY(alternatePrerequisite), 140f, 50f);
                        var anomalyAdd = 0f;
                        if (ModsConfig.AnomalyActive && alternatePrerequisite.knowledgeCategory != null)
                        {
                            anomalyAdd = 14f;
                        }

                        var label = __instance.GetLabel(alternatePrerequisite);
                        rect.xMax += anomalyAdd;
                        var rect2 = rect;
                        rect2.xMin += anomalyAdd;
                        Widgets.LabelCacheHeight(ref rect2, label);
                        var rect3 = rect2;
                        rect3.y = rect2.yMax - 4f;
                        Widgets.LabelCacheHeight(ref rect3, " ");
                        rect.yMax = rect3.yMax;

                        end.x = __instance.PosX(alternatePrerequisite) + 140f + num;
                        end.y = __instance.PosY(alternatePrerequisite) + 25f;
                        if (___selectedProject == item || ___selectedProject == alternatePrerequisite)
                        {
                            if (i == 1)
                            {
                                Widgets.DrawLine(start, end, TexUI.HighlightLineResearchColor, 4f);
                            }

                            if (!alternatePrerequisite.IsFinished)
                            {
                                Widgets.DrawRectFast(rect.ExpandedBy(2f),
                                    __instance.NoMatchTint(TexUI.DependencyOutlineResearchColor));
                            }

                            continue;
                        }

                        if (i == 0)
                        {
                            Widgets.DrawLine(start, end, TexUI.DefaultLineResearchColor, 2f);
                        }
                    }
                }
            }
        }
    }
}