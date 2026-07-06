# RimWorld 1.6 port

Built and checked against RimWorld 1.6.4871.

## Changes

- Added a dedicated 1.6 load folder and metadata.
- Recompiled `Robots.dll` and `FaceLaserTesting.dll` against the 1.6 game assemblies.
- Updated `PlanetTile`, allowed-area, and protected tick APIs changed by RimWorld 1.6.
- Updated the renamed VEF extinguish damage worker.
- Removed the obsolete JecsTools dependency and replaced its start-with-hediff behavior locally.
- Removed the obsolete Achievements Expanded integration. It was not gameplay-critical and its 1.4 API was compiled into the old assembly.

## Required mods

- Harmony
- Humanoid Alien Races
- Vanilla Expanded Framework

The XML files are well-formed and both assemblies compile cleanly. The referenced HAR classes (`AlienBackstoryDef`, `RaceSettings`, and `ThingDef_AlienRace`) were verified against the installed RimWorld 1.6 release of Humanoid Alien Races.
