using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviours/Smoothed Cohesion")]
public class SmoothCohesionBehaviour : AFlockBehaviour
{
    [SerializeField]
    private float smoothTime = 0.5f;

    private Vector2 currentVelocity;

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
        // Smooth move
        move = Vector2.SmoothDamp(agent.transform.up, move, ref currentVelocity, smoothTime);
        return move;
    }
}
