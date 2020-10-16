using nl.FutureWhiz.Unity2DBoids.Boids.Agents;
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
            // Negate Previous Input
            if (isMouseDown && (UnityEngine.Input.GetMouseButtonUp(0) && UnityEngine.Input.touchCount == 0))
                isMouseDown = false;
            // Check for NEW Input
            if (UnityEngine.Input.GetMouseButtonDown(0) || (UnityEngine.Input.touchCount > 0 && !isMouseDown))
                isMouseDown = !EventSystem.current.IsPointerOverGameObject(); // Negate Input if Press was on Canvas instead of Play-Area

            // Handle Input
            if (isMouseDown)
            {
                Vector2? mousePos = null;
                // Prioritise Touch
                if (UnityEngine.Input.touchCount > 0)
                {
                    // Get WorldPos from Touch
                    Touch touch = UnityEngine.Input.GetTouch(0);
                    mousePos = (Vector2)Camera.main.ScreenToWorldPoint(touch.position);
                }
                // FallBack to Mouse
                else if (UnityEngine.Input.GetMouseButton(0))
                {
                    // Get WorldPos from Mouse
                    mousePos = (Vector2)Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
                }
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
            else return Vector2.zero;
        }
        #endregion

        #region Unity
        /// <summary>
        /// Reset for IsMouseDown
        /// </summary>
        private void OnEnable()
        {
            isMouseDown = false;
        }
        /// <summary>
        /// Reset for IsMouseDown
        /// </summary>
        private void OnDisable()
        {
            isMouseDown = false;
        }
        #endregion
        #endregion
    }
}