using nl.FutureWhiz.Unity2DBoids.Boids.Agents;
using nl.FutureWhiz.Unity2DBoids.UserInput;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace nl.FutureWhiz.Unity2DBoids.Boids.FlockBehaviours.Input
{
    /// <summary>
    /// Moves Agents towards Mouse-Position if Mouse is Down (Works with Touch as well)
    /// </summary>
    [CreateAssetMenu(menuName = "Flock/Behaviours/Move To Mouse")]
    public class MoveToMouseBehaviour : AFlockBehaviour
    {
        #region Variables
        #region Editor
        /// <summary>
        /// Radius around Mouse-Position to Stick to
        /// </summary>
        [SerializeField]
        [Tooltip("Radius around Mouse-Position to Stick to")]
        private float radius = 1.25f;
        #endregion

        #region Private
        /// <summary>
        /// Whether Mouse is currently active (Whether to run this behaviour
        /// </summary>
        private bool isMouseDown;
        #endregion
        #endregion

        #region Methods
        #region Public
        /// <summary>
        /// Calculates Movement for Agent
        /// </summary>
        /// <param name="agent">Agent to calculate Movement for</param>
        /// <param name="neighbours">Neighbours for Agent</param>
        /// <param name="flock">Controller for Flock</param>
        /// <returns>Movement for Agent</returns>
        public override Vector2 CalculateMove(Rocket agent, List<Transform> neighbours, FlockController flock)
        {
            Vector2? mousePos = InputController.Instance.InputPosition();
            // No Pos (Should not be possible here)
            if (!mousePos.HasValue)
                return Vector2.zero;
            // Get Offset
            Vector2 offset = mousePos.Value - (Vector2)agent.transform.position;
            float t = offset.magnitude / radius;
            // Leniency
            if (t < 0.9f)
                return Vector2.zero;
            // Return Vector to move to
            return offset * (t * t);
        }
        #endregion
        #endregion
    }
}