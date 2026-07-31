using RimWorld;
using Verse;

namespace RobotRamRod;

public class Verb_Shoot_ConsumeLibertyPrimeMiniNuke : Verb_Shoot
{
    private static ThingDef AmmoDef => DefDatabase<ThingDef>.GetNamedSilentFail("LibertyPrimeMiniNukeAmmo");

    public override bool Available()
    {
        return base.Available() && HasAmmo();
    }

    protected override bool TryCastShot()
    {
        if (!HasAmmo())
        {
            if (CasterIsPawn)
            {
                Messages.Message("Liberty Prime needs a mini nuke in its inventory to fire.", CasterPawn,
                    MessageTypeDefOf.RejectInput, false);
            }

            return false;
        }

        var fired = base.TryCastShot();
        if (fired)
        {
            CasterPawn.inventory.RemoveCount(AmmoDef, 1);
        }

        return fired;
    }

    private bool HasAmmo()
    {
        var ammoDef = AmmoDef;
        return ammoDef != null && CasterIsPawn && CasterPawn.inventory != null && CasterPawn.inventory.Count(ammoDef) > 0;
    }
}
