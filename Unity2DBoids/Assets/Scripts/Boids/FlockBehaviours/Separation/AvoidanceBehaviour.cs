using nl.FutureWhiz.Unity2DBoids.Boids.Agents;
using System.Collections.Generic;
using UnityEngine;

namespace nl.FutureWhiz.Unity2DBoids.Boids.FlockBehaviours.Separation
{
    /// <summary>
    /// Makes Agents avoid collision with Neighbours
    /// </summary>
    [CreateAssetMenu(menuName = "Flock/Behaviours/Avoidance")]
    public class AvoidanceBehaviour : AFlockBehaviour
    {
        #region Methods
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
            Vector2 avoidance = Vector2.zero;
            int numToAvoid = 0;
            // Check Neighbours
            for (int i = 0; i < neighbours.Count; i++)
            {
                Transform neighbour = neighbours[i];
                // Check distance to Neighbour
                if (Vector2.SqrMagnitude(neighbour.position - agent.transform.position) < flock.SquareAvoidanceRadius)
                {
                    // Avoid Neigbour
                    avoidance += (Vector2)(agent.transform.position - neighbour.position);
                    numToAvoid++;
                }
            }
            // Average out Avoidance
            if (numToAvoid > 0)
                avoidance /= numToAvoid;
            // Return Result
            return avoidance;
        }
        #endregion
    }
}