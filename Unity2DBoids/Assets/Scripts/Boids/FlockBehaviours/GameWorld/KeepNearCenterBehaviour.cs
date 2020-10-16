using nl.FutureWhiz.Unity2DBoids.Boids.Agents;
using System.Collections.Generic;
using UnityEngine;

namespace nl.FutureWhiz.Unity2DBoids.Boids.FlockBehaviours.GameWorld
{
    /// <summary>
    /// Keeps Agents in Flock near a certain position (to prevent them from leaving the Screen)
    /// </summary>
    [CreateAssetMenu(menuName = "Flock/Behaviours/Keep Near Center")]
    public class KeepNearCenterBehaviour : AFlockBehaviour
    {
        #region Variables
        #region Editor
        /// <summary>
        /// Position to keep Agents near
        /// </summary>
        [SerializeField]
        [Tooltip("Position to keep Agents near")]
        private Vector2 center;
        /// <summary>
        /// Radius around Center to keep Agents in
        /// </summary>
        [SerializeField]
        [Tooltip("Radius around Center to keep Agents in")]
        private float radius = 15f;
        #endregion
        #endregion

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
            // Calculate Offset
            Vector2 offset = center - (Vector2)agent.transform.position;
            float t = offset.magnitude / radius;
            // Check if Current Movement needs adjustment
            if (t < 0.9f)
                return Vector2.zero;
            // Return Result
            return offset * (t * t);
        }
        #endregion
    }
}