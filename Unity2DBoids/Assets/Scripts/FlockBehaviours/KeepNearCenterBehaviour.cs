using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviours/Keep Near Center")]
public class KeepNearCenterBehaviour : AFlockBehaviour
{
    [SerializeField]
    private Vector2 center;
    [SerializeField]
    private float radius = 15f;

    public override Vector2 CalculateMove(Rocket agent, List<Transform> neighbours, FlockController flock)
    {
        Vector2 offset = center - (Vector2)agent.transform.position;
        float t = offset.magnitude / radius;

        if (t < 0.9f)
            return Vector2.zero;

        return offset * (t * t);
    }
}
