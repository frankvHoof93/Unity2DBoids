using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviours/Avoidance")]
public class AvoidanceBehaviour : AFlockBehaviour
{
    public override Vector2 CalculateMove(Rocket agent, List<Transform> neighbours, FlockController flock)
    {
        if (neighbours.Count == 0)
            return Vector2.zero; // No neighbours, no behaviour

        Vector2 avoidance = Vector2.zero;
        int numToAvoid = 0;
        for (int i = 0; i < neighbours.Count; i++)
        {
            Transform neighbour = neighbours[i];
            if (Vector2.SqrMagnitude(neighbour.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                avoidance += (Vector2)(agent.transform.position - neighbour.position);
                numToAvoid++;
            }
        }
        if (numToAvoid > 0)
            avoidance /= numToAvoid;
        return avoidance;
    }
}
