using UnityEngine;

namespace nl.FutureWhiz.Unity2DBoids.Boids.Agents
{
    /// <summary>
    /// Agent in Boids-Flock
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Rocket : MonoBehaviour
    {
        #region Variables
        /// <summary>
        /// Collider for Rocket
        /// </summary>
        public Collider2D RocketCollider { get; private set; }
        #endregion

        #region Methods
        #region Public
        /// <summary>
        /// Moves Rocket in World
        /// </summary>
        /// <param name="velocity">Velocity of Rocket (Vector)</param>
        public void MoveRocket(Vector2 velocity)
        {
            // Rotate
            transform.up = velocity;
            // Move
            transform.position += (Vector3)(velocity * Time.deltaTime);
        }
        #endregion

        #region Unity
        /// <summary>
        /// Retrieves Collider for Rocket
        /// </summary>
        private void Awake()
        {
            RocketCollider = GetComponent<Collider2D>();
        }
        #endregion
        #endregion
    }
}