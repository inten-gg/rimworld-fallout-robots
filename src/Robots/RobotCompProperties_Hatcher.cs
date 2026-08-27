using Verse;

namespace Inten.Rimworld.Fallout.Robots.RobotRamRod;

public class RobotCompProperties_Hatcher : CompProperties
{
    public float hatcherDaystoHatch = 1f;

    public PawnKindDef hatcherPawn;

    public RobotCompProperties_Hatcher()
    {
        compClass = typeof(RobotCompHatcher);
    }
}