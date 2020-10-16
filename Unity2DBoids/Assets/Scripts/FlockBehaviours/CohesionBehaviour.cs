using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviours/Cohesion")]
public class CohesionBehaviour : AFlockBehaviour
{
    public override Vector2 CalculateMove(Rocket agent, List<Transform> neighbours, FlockController flock)
    {
        if (neighbours.Count == 0)
            return Vector2.zero; // No neighbours, no behaviour
        // Average neighbour position
        Vector2 move = Vector2.zero;
        for (int i = 0; i < neighbours.Count; i++)
            move += (Vector2)neighbours[i].position;
        move /= neighbours.Count;
        // Translate to rocket
        move -= (Vector2)agent.transform.position;
        return move;
    }
}
