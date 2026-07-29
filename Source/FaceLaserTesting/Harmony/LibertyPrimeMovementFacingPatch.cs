using System;
using HarmonyLib;
using Verse;
using Verse.AI;

namespace FaceLaserTesting;

/// <summary>
/// Keeps Liberty Prime facing the cell he is actually walking toward.
/// Large cached HAR pawns can otherwise retain the previous horizontal
/// rotation while their tweened position is already moving west.
/// </summary>
[HarmonyPatch(typeof(Pawn_PathFollower), nameof(Pawn_PathFollower.PatherTick))]
internal static class LibertyPrimeMovementFacingPatch
{
    [HarmonyPostfix]
    private static void Postfix(Pawn ___pawn, Pawn_PathFollower __instance)
    {
        if (___pawn?.def?.defName != "LibertyPrime" ||
            !___pawn.Spawned ||
            !__instance.Moving ||
            !__instance.nextCell.IsValid ||
            __instance.nextCell == ___pawn.Position ||
            ___pawn.stances?.FullBodyBusy == true)
        {
            return;
        }

        var movement = __instance.nextCell - ___pawn.Position;
        if (Math.Abs(movement.x) >= Math.Abs(movement.z))
        {
            ___pawn.Rotation = movement.x < 0 ? Rot4.West : Rot4.East;
        }
        else
        {
            ___pawn.Rotation = movement.z < 0 ? Rot4.South : Rot4.North;
        }
    }
}

/// <summary>
/// Applies the movement direction directly to both render phases. This is
/// necessary because another pawn update can replace Pawn.Rotation after the
/// path follower has run, while the cached pawn atlas keeps that later value.
/// </summary>
[HarmonyPatch(typeof(PawnRenderer), nameof(PawnRenderer.DynamicDrawPhaseAt))]
internal static class LibertyPrimeRenderFacingPatch
{
    [HarmonyPrefix]
    private static void Prefix(Pawn ___pawn, ref Rot4? rotOverride)
    {
        if (___pawn?.def?.defName != "LibertyPrime" ||
            ___pawn.pather == null ||
            !___pawn.pather.Moving ||
            !___pawn.pather.nextCell.IsValid ||
            ___pawn.pather.nextCell == ___pawn.Position ||
            ___pawn.stances?.FullBodyBusy == true)
        {
            return;
        }

        var movement = ___pawn.pather.nextCell - ___pawn.Position;
        if (Math.Abs(movement.x) >= Math.Abs(movement.z))
        {
            rotOverride = movement.x < 0 ? Rot4.West : Rot4.East;
        }
        else
        {
            rotOverride = movement.z < 0 ? Rot4.South : Rot4.North;
        }
    }
}
