using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviours/Alignment")]
public class AlignmentBehaviour : AFlockBehaviour
{
    public override Vector2 CalculateMove(Rocket agent, List<Transform> neighbours, FlockController flock)
    {
        if (neighbours.Count == 0)
            return agent.transform.up; // No neighbours, keep current alignment

        // Average Alignment
        Vector2 alignment = Vector2.zero;
        for (int i = 0; i < neighbours.Count; i++)
            alignment += (Vector2)neighbours[i].up;
        alignment /= neighbours.Count;

        return alignment;
    }
}
