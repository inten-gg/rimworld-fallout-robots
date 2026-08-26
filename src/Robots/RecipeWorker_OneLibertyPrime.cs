using RimWorld;
using Verse;

namespace Inten.Rimworld.Fallout.Robots.RobotRamRod;

public class RecipeWorker_OneLibertyPrime : RecipeWorker
{
    public override bool AvailableOnNow(Thing thing, BodyPartRecord part = null)
    {
        return base.AvailableOnNow(thing, part) && !LibertyPrimeUtility.Exists(includeKits: true);
    }
}
