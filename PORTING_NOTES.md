# RimWorld 1.6 port

Built and checked against RimWorld 1.6.4871.

## Changes

- Added a prototype Liberty Prime robot race, including race, pawnkind, backstory, research, crafting kit, and machining-bench recipe definitions.
- Liberty Prime is configured as a unique, extremely expensive combat-only unit. A custom recipe worker prevents building another while Liberty Prime or a Liberty Prime kit already exists, and the hatcher blocks duplicate pawn creation as a safety fallback.
- Replaced Liberty Prime's temporary Sentrybot-style mounted weapons with unique integrated weapons: an eye laser and a reusable mini nuke launcher.
- Added craftable Liberty Prime mini nuke ammunition. The mini nuke launcher now requires one `LibertyPrimeMiniNukeAmmo` item in Liberty Prime's inventory and consumes one per shot.
- Added separate Liberty Prime PNG slices for south, north, east, a west reference, and several concept item sprites.
- Added a dedicated 1.6 load folder and metadata.
- Recompiled `Robots.dll` and `FaceLaserTesting.dll` against the 1.6 game assemblies.
- Updated `PlanetTile`, allowed-area, and protected tick APIs changed by RimWorld 1.6.
- Updated the renamed VEF extinguish damage worker.
- Converted legacy backstory skill gains to RimWorld 1.6's keyed format.
- Migrated legacy HAR body-addon fields to HAR 1.6 render conditions.
- Assigned RimWorld 1.6's `Humanlike` pawn render tree to all eight robot races.
- Added the silhouette graphic now required by RimWorld 1.6 for humanlike life stages.
- Removed obsolete Tabula Rasa update definitions and removed XML fields no longer available in RimWorld 1.6.
- Removed the obsolete JecsTools dependency and replaced its start-with-hediff behavior locally.
- Removed the obsolete Achievements Expanded integration. It was not gameplay-critical and its 1.4 API was compiled into the old assembly.

## Required mods

- Harmony
- Humanoid Alien Races
- Vanilla Expanded Framework

The XML files are well-formed and both assemblies compile cleanly. The referenced HAR classes (`AlienBackstoryDef`, `RaceSettings`, and `ThingDef_AlienRace`) were verified against the installed RimWorld 1.6 release of Humanoid Alien Races.
