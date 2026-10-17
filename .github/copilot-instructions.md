# Copilot Instructions for Alternate Research Projects Mod

## Overview and Purpose

**Mod Name:** Alternate Research Projects  
**Author:** Mlie  
**PackageId:** Mlie.AlternateResearchProjects  

The "Alternate Research Projects" mod introduces a novel approach to handling research progressions in RimWorld by enabling alternative prerequisites for research projects. This allows players to achieve research milestones through different pathways, fostering diverse gameplay strategies. Mod authors can extend research projects by defining alternate prerequisites, where only one of the multiple alternatives is required to unlock the subsequent project.

## Key Features and Systems

- **Alternative Prerequisites:** Define multiple paths to unlocking a research project by setting alternatives, offering flexibility in gameplay.
- **Mod Extensions:** Enables mod authors to incorporate alternate prerequisites for custom research projects.
- **Included Patches:** 
  - Psychoid Brewing can be an alternative path for Drug Production to unlock Psychite Refining.
- **Integration with Other Mods:** Includes patches for mods like "GiTS Cyberbrains".

## Coding Patterns and Conventions

- **Class Structure:** Each C# file is organized to focus on specific aspects of the mod such as research extensions, prerequisites handling, and UI enhancements.
- **File Naming:** Files are named descriptively to reflect their purpose (e.g., `MainTabWindow_Research_ListProjects.cs` is utilized for listing and managing research projects in the main tab).
- **Code Style:** Follow C# coding conventions including PascalCase for classes and methods, and camelCase for fields and parameters.

## XML Integration

- The mod utilizes XML for defining patch data and extensions. The file `Vanilla.xml` contains definitions for the integrations and extensions applied to the base game.
- XML files should be structured properly with clear comments to indicate different sections and changes being made.

## Harmony Patching

- The mod employs Harmony patches to inject functionalities into the existing game methods without altering the original files.
- Key Harmony patches are applied within:
  - `MainTabWindow_Research_DrawResearchPrerequisites.cs`: Modifies how research prerequisites are displayed in the UI.
  - `MainTabWindow_Research_ListProjects.cs`: Adjusts the listing behavior of research projects to accommodate alternates.
- Use Prefix and Postfix where applicable to ensure Harmony patches do not disrupt game stability.

## Suggestions for Copilot

1. **Context Awareness:** When writing methods related to research prerequisites, ensure the context of alternative paths is maintained.
2. **Error Handling:** Assist in implementing robust error handling, especially when dealing with undefined alternates or missing XML configurations.
3. **Optimization:** Suggest performance optimizations where complex logic is processed in loops, especially within UI elements.
4. **XML Interaction:** Propose improvements while integrating or updating XML definitions to maintain compatibility with other mods and game updates.
5. **Code Simplification:** Offer refactoring options to simplify complex logical blocks for improved readability and maintainability.

This guide is designed to assist developers in understanding the core structure and objectives of the "Alternate Research Projects" mod, providing them with a reference to implement consistent and efficient coding practices in their contributions.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

