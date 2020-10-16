using nl.FutureWhiz.Unity2DBoids.Boids.Agents;
using System.Collections.Generic;
using UnityEngine;

namespace nl.FutureWhiz.Unity2DBoids.Boids.FlockBehaviours.Coherence
{
    /// <summary>
    /// Calculates Movement for Agent based on Cohesion with Neighbours
    /// <para>
    /// Smoothes out Movement to prevent Jitters
    /// </para>
    /// </summary>
    [CreateAssetMenu(menuName = "Flock/Behaviours/Smoothed Cohesion")]
    public class SmoothCohesionBehaviour : CohesionBehaviour
    {
        #region Variables
        #region Editor
        /// <summary>
        /// Factor for Vector2.SmoothDamp
        /// </summary>
        [SerializeField]
        [Tooltip("Factor for Vector2.SmoothDamp")]
        private float smoothTime = 0.5f;
        #endregion

        #region Private
        /// <summary>
        /// Current Velocity (for Vector2.SmoothDamp)
        /// </summary>
        private Vector2 currentVelocity;
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
            // Calculate from Base-Class
            Vector2 move = base.CalculateMove(agent, neighbours, flock);
            if (move != Vector2.zero)
            {
                // Smooth move
                move = Vector2.SmoothDamp(agent.transform.up, move, ref currentVelocity, smoothTime);
            }
            // Return Result
            return move;
        }
        #endregion
    }
}