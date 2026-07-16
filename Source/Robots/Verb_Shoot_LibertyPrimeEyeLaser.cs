using RimWorld;
using UnityEngine;
using Verse;

namespace RobotRamRod;

public class Verb_Shoot_LibertyPrimeEyeLaser : Verb_Shoot
{
    private const float DrawSize = 4f;
    private const float ProjectileAltitude = 0.36f;

    public override void WarmupComplete()
    {
        FaceCurrentTarget();
        base.WarmupComplete();
    }

    public override void BurstingTick()
    {
        FaceCurrentTarget();
        base.BurstingTick();
    }

    protected override bool TryCastShot()
    {
        if (currentTarget.HasThing && currentTarget.Thing.Map != caster.Map)
        {
            return false;
        }

        var projectileDef = Projectile;
        if (projectileDef == null)
        {
            return false;
        }

        if (!TryFindShootLineFromTo(caster.Position, currentTarget, out var shootLine))
        {
            if (verbProps.stopBurstWithoutLos)
            {
                return false;
            }
        }

        FaceCurrentTarget();

        var launcher = caster;
        var compMannable = caster.TryGetComp<CompMannable>();
        if (compMannable?.ManningPawn != null)
        {
            launcher = compMannable.ManningPawn;
        }

        var launchOrigin = CasterIsPawn ? GetLibertyPrimeEyePosition(CasterPawn) : caster.DrawPos;
        var spawnCell = launchOrigin.ToIntVec3();
        if (!spawnCell.InBounds(caster.Map))
        {
            spawnCell = shootLine.Source;
        }

        var projectile = (Projectile)GenSpawn.Spawn(projectileDef, spawnCell, caster.Map);
        projectile.Launch(launcher, launchOrigin, currentTarget, currentTarget, ProjectileHitFlags.IntendedTarget,
            false, EquipmentSource);

        return true;
    }

    private void FaceCurrentTarget()
    {
        if (!CasterIsPawn || !currentTarget.IsValid)
        {
            return;
        }

        var offset = currentTarget.Cell - CasterPawn.Position;
        if (offset == IntVec3.Zero)
        {
            return;
        }

        CasterPawn.Rotation = Rot4.FromAngleFlat(offset.AngleFlat);
    }

    private static Vector3 TextureOffset(float textureX, float textureY)
    {
        return new Vector3((textureX - 0.5f) * DrawSize, ProjectileAltitude, (0.5f - textureY) * DrawSize);
    }

    private static Vector3 ForwardOffset(Rot4 rotation)
    {
        if (rotation == Rot4.North)
        {
            return new Vector3(0f, 0f, 0.22f);
        }

        if (rotation == Rot4.South)
        {
            return new Vector3(0f, 0f, -0.22f);
        }

        if (rotation == Rot4.East)
        {
            return new Vector3(0.22f, 0f, 0f);
        }

        if (rotation == Rot4.West)
        {
            return new Vector3(-0.22f, 0f, 0f);
        }

        return Vector3.zero;
    }

    private static Vector3 GetLibertyPrimeEyePosition(Pawn pawn)
    {
        var origin = pawn.DrawPos;
        var rotation = pawn.Rotation;
        Vector3 offset;

        if (rotation == Rot4.South)
        {
            offset = TextureOffset(0.50f, 0.205f);
        }
        else if (rotation == Rot4.North)
        {
            // The visor is on the hidden far side in north view. Use the head's
            // front center, not the pawn center, so north shots still emerge from
            // the eye line instead of the torso.
            offset = TextureOffset(0.50f, 0.205f);
        }
        else if (rotation == Rot4.East)
        {
            offset = TextureOffset(0.615f, 0.180f);
        }
        else if (rotation == Rot4.West)
        {
            offset = TextureOffset(0.385f, 0.180f);
        }
        else
        {
            offset = Vector3.zero;
        }

        return origin + offset + ForwardOffset(rotation);
    }
}
