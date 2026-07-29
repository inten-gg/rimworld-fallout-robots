using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace FaceLaserTesting;

[HarmonyPatch(typeof(Building_Turret), "ThreatDisabled")]
public static class AvP_Building_Turret_Shoulder_ThreatDisabled_Patch
{
    [HarmonyPostfix]
    public static void IgnoreShoulderTurret(Building_Turret __instance, ref bool __result,
        IAttackTargetSearcher disabledFor)
    {
        __result = __result || __instance is Building_Turret_Shoulder;
    }
}
