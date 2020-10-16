using nl.FutureWhiz.Unity2DBoids.Boids.Agents;
using System.Collections.Generic;
using UnityEngine;

namespace nl.FutureWhiz.Unity2DBoids.Boids.FlockBehaviours.Coherence
{
    /// <summary>
    /// Calculates Movement for Agent based on Cohesion with Neighbours
    /// </summary>
    [CreateAssetMenu(menuName = "Flock/Behaviours/Cohesion")]
    public class CohesionBehaviour : AFlockBehaviour
    {
        /// <summary>
        /// Calculates Movement for Agent
        /// </summary>
        /// <param name="agent">Agent to calculate Movement for</param>
        /// <param name="neighbours">Neighbours for Agent</param>
        /// <param name="flock">Controller for Flock</param>
        /// <returns>Movement for Agent</returns>
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
            // Return Result
            return move;
        }
    }
}