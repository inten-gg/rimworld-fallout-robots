using System.Linq;
using Verse;

namespace RobotRamRod;

public static class LibertyPrimeUtility
{
    public static bool Exists(bool includeKits)
    {
        var libertyPrimeRace = DefDatabase<ThingDef>.GetNamedSilentFail("LibertyPrime");
        var libertyPrimeKit = DefDatabase<ThingDef>.GetNamedSilentFail("LibertyPrimeKit");

        if (libertyPrimeRace != null)
        {
            foreach (var map in Find.Maps)
            {
                if (map.mapPawns.AllPawns.Any(pawn => pawn.def == libertyPrimeRace && !pawn.Dead))
                {
                    return true;
                }
            }

            if (Find.WorldPawns.AllPawnsAlive.Any(pawn => pawn.def == libertyPrimeRace))
            {
                return true;
            }
        }

        if (!includeKits || libertyPrimeKit == null)
        {
            return false;
        }

        foreach (var map in Find.Maps)
        {
            if (map.listerThings.ThingsOfDef(libertyPrimeKit).Any())
            {
                return true;
            }
        }

        return false;
    }
}
