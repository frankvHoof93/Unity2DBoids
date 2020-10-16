using nl.FutureWhiz.Unity2DBoids.Boids.Agents;
using System.Collections.Generic;
using UnityEngine;

namespace nl.FutureWhiz.Unity2DBoids.Boids.FlockBehaviours.Alignment
{
    /// <summary>
    /// Calculates Movement for Agent based on Alignment with Neighbours
    /// </summary>
    [CreateAssetMenu(menuName = "Flock/Behaviours/Alignment")]
    public class AlignmentBehaviour : AFlockBehaviour
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
                return agent.transform.up; // No neighbours, keep current alignment
            // Average Alignment
            Vector2 alignment = Vector2.zero;
            for (int i = 0; i < neighbours.Count; i++)
                alignment += (Vector2)neighbours[i].up;
            alignment /= neighbours.Count;
            // Return Result
            return alignment;
        }
    }
}