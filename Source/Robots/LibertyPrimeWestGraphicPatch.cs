using HarmonyLib;
using UnityEngine;
using Verse;

namespace RobotRamRod;

[StaticConstructorOnStartup]
public static class LibertyPrimeWestGraphicPatch
{
    static LibertyPrimeWestGraphicPatch()
    {
        new Harmony("inten-gg.rimworld-fallout-robots.libertyprime-west-graphics").PatchAll(typeof(LibertyPrimeWestGraphicPatch).Assembly);
    }
}

[HarmonyPatch(typeof(Graphic), nameof(Graphic.MatAt), typeof(Rot4), typeof(Thing))]
public static class LibertyPrimeGraphicMultiMatAtPatch
{
    public static void Postfix(Graphic __instance, Rot4 rot, ref Material __result)
    {
        if (rot != Rot4.West || __result == null || !LibertyPrimeRenderUtility.IsLibertyPrimeGraphic(__instance.path))
        {
            return;
        }

        var westTexture = ContentFinder<Texture2D>.Get(__instance.path + "_west", false);
        if (westTexture == null)
        {
            return;
        }

        __result = MaterialPool.MatFrom(westTexture, __result.shader, __result.color);
    }

}

[HarmonyPatch(typeof(Graphic), nameof(Graphic.MeshAt))]
public static class LibertyPrimeGraphicMeshAtPatch
{
    public static bool Prefix(Graphic __instance, Rot4 rot, ref Mesh __result)
    {
        if (rot != Rot4.West || !LibertyPrimeRenderUtility.IsLibertyPrimeGraphic(__instance.path))
        {
            return true;
        }

        __result = MeshPool.GridPlane(__instance.drawSize);
        return false;
    }

}

[HarmonyPatch(typeof(PawnRenderer), "ParallelGetPreRenderResults")]
public static class LibertyPrimePawnRendererCachePatch
{
    private static readonly AccessTools.FieldRef<PawnRenderer, Pawn> PawnField =
        AccessTools.FieldRefAccess<PawnRenderer, Pawn>("pawn");

    public static void Prefix(PawnRenderer __instance, ref bool disableCache)
    {
        if (LibertyPrimeRenderUtility.IsLibertyPrime(PawnField(__instance)))
        {
            disableCache = true;
        }
    }
}

public static class LibertyPrimeRenderUtility
{
    public static bool IsLibertyPrime(Pawn pawn)
    {
        return pawn?.def?.defName == "LibertyPrime";
    }

    public static bool IsLibertyPrimeGraphic(string path)
    {
        return path != null &&
               (path.StartsWith("LibertyPrime/Body/") || path.StartsWith("LibertyPrime/Head/"));
    }
}
