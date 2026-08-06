using RimWorld;
using UnityEngine;
using Verse;

namespace RobotRamRod;

public class Projectile_LibertyPrimeMiniNuke : Projectile_Explosive
{
    protected override void Impact(Thing hitThing, bool blockedByShield = false)
    {
        var impactCell = Position;
        var impactMap = Map;
        base.Impact(hitThing, blockedByShield);

        if (impactMap == null || !impactCell.IsValid)
        {
            return;
        }

        SpawnMushroomCloud(impactCell.ToVector3Shifted(), impactMap);
    }

    private static void SpawnMushroomCloud(Vector3 center, Map map)
    {
        // Stack several vanilla smoke/fire motes in a vertical-looking cluster.
        // RimWorld is top-down, so this is a stylized mushroom-cloud puff rather
        // than a real 3D column.
        for (var i = 0; i < 18; i++)
        {
            var angle = Rand.Range(0f, 360f);
            var radius = Rand.Range(0.1f, 2.2f);
            var loc = center + new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius);
            FleckMaker.ThrowSmoke(loc, map, Rand.Range(2.4f, 4.6f));
        }

        for (var i = 0; i < 12; i++)
        {
            var angle = Rand.Range(0f, 360f);
            var radius = Rand.Range(0.2f, 1.15f);
            var loc = center + new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius);
            FleckMaker.ThrowSmoke(loc + new Vector3(0f, 0f, Rand.Range(0.4f, 1.6f)), map, Rand.Range(3.8f, 6.4f));
        }

        for (var i = 0; i < 8; i++)
        {
            var angle = Rand.Range(0f, 360f);
            var radius = Rand.Range(1.0f, 3.6f);
            var loc = center + new Vector3(Mathf.Cos(angle) * radius, 0f, Mathf.Sin(angle) * radius);
            FleckMaker.ThrowFireGlow(loc, map, Rand.Range(1.8f, 3.2f));
        }
    }
}
