using RimWorld;
using UnityEngine;
using Verse;

namespace RobotRamRod;

public class Verb_Shoot_LibertyPrimeEyeLaser : Verb_Shoot
{
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

        var launcher = caster;
        var compMannable = caster.TryGetComp<CompMannable>();
        if (compMannable?.ManningPawn != null)
        {
            launcher = compMannable.ManningPawn;
        }

        var launchOrigin = EyeLaunchOrigin();
        var projectile = (Projectile)GenSpawn.Spawn(projectileDef, shootLine.Source, caster.Map);
        projectile.Launch(launcher, launchOrigin, currentTarget, currentTarget, ProjectileHitFlags.IntendedTarget,
            false, EquipmentSource);

        return true;
    }

    private Vector3 EyeLaunchOrigin()
    {
        var origin = caster.DrawPos;
        var target = currentTarget.Cell.ToVector3Shifted();
        var direction = target - origin;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            direction.Normalize();
            origin += direction * 0.35f;
        }
        else if (CasterIsPawn)
        {
            origin += CasterPawn.Rotation.FacingCell.ToVector3() * 0.35f;
        }

        // DrawPos is the pawn's ground/body center. Liberty Prime's eyes are baked
        // high in the oversized body sprite, so a normal projectile still appears
        // to launch from the torso. Push the visual origin upward on-screen and a
        // little sideways for side-facing sprites.
        origin.z += 1.05f;
        if (CasterIsPawn)
        {
            if (CasterPawn.Rotation == Rot4.East)
            {
                origin.x += 0.38f;
            }
            else if (CasterPawn.Rotation == Rot4.West)
            {
                origin.x -= 0.38f;
            }
        }

        // Keep the projectile above the ground layer but below overhead motes.
        origin.y += 0.36f;
        return origin;
    }
}
