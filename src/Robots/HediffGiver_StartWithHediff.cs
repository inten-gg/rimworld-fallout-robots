using Verse;

namespace RobotStuff;

/// <summary>
/// Gives a race its permanent robot-traits hediff without depending on JecsTools.
/// </summary>
public class HediffGiver_StartWithHediff : HediffGiver
{
    public override void OnIntervalPassed(Pawn pawn, Hediff cause)
    {
        if (pawn?.health?.hediffSet == null || pawn.health.hediffSet.HasHediff(hediff))
        {
            return;
        }

        pawn.health.AddHediff(hediff);
    }
}
