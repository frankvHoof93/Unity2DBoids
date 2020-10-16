using nl.FutureWhiz.Unity2DBoids.Boids.Agents;
using nl.FutureWhiz.Unity2DBoids.Boids.FlockBehaviours;
using nl.FutureWhiz.Unity2DBoids.Utils;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All Code related to Boids (Flocking-Behaviour)
/// </summary>
namespace nl.FutureWhiz.Unity2DBoids.Boids
{
    /// <summary>
    /// Controller for Flock of Rockets
    /// 
    /// <para>
    /// FlockController is used to iterate through all rockets, 
    /// as the Unity LifeCycle's usage of reflection is far slower than manual iteration
    /// </para>
    /// <para>
    /// Singleton is used for easy access from MenuController
    /// </para>
    /// </summary>
    public class FlockController : SingletonBehaviour<FlockController>
    {
        #region Variables
        #region Public
        /// <summary>
        /// Square of AvoidanceRadius
        /// </summary>
        public float SquareAvoidanceRadius { get => squareAvoidanceRadius; }
        #endregion

        #region Editor
        #region Spawning
        [Header("Spawn-Settings")]
        /// <summary>
        /// Prefab for a Rocket
        /// </summary>
        [SerializeField]
        [Tooltip("Prefab for a Rocket")]
        private GameObject rocketPrefab;
        /// <summary>
        /// Initial Size of Flock at Spawn
        /// </summary>
        [SerializeField]
        [Range(25, 500)]
        [Tooltip("Initial Size of Flock at Spawn")]
        private int initialFlockSize = 50;
        /// <summary>
        /// Density of Flock at Spawn
        /// </summary>
        [SerializeField]
        [Tooltip("Density of Flock at Spawn")]
        private float density = 0.075f;
        #endregion

        #region RocketSettings
        [Header("RocketSettings")]
        /// <summary>
        /// Acceleration for a Rocket
        /// </summary>
        [Range(1f, 100f)]
        [SerializeField]
        [Tooltip("Acceleration for a Rocket")]
        private float driveFactor = 10f;
        /// <summary>
        /// Max Speed for a Rocket
        /// </summary>
        [Range(1f, 100f)]
        [SerializeField]
        [Tooltip("Max Speed for a Rocket")]
        private float maxSpeed = 5f;
        #endregion

        #region Flocking
        [Header("FlockBehaviour")]
        /// <summary>
        /// Main FlockBehaviour to Execute (Use Complex-Behaviour to layer multiple behaviours)
        /// </summary>
        [SerializeField]
        [Tooltip("Main FlockBehaviour to Execute (Use Complex-Behaviour to layer multiple behaviours)")]
        private AFlockBehaviour flockBehaviour;
        /// <summary>
        /// Radius in which to check for Neighbours
        /// </summary>
        [SerializeField]
        [Range(0.05f, 20f)]
        [Tooltip("Radius in which to check for Neighbours")]
        private float neighbourRadius = 1.5f;
        /// <summary>
        /// Radius in which to check for Neighbours when Avoiding
        /// </summary>
        [SerializeField]
        [Range(0.05f, 10f)]
        [Tooltip("Radius in which to check for Neighbours when Avoiding")]
        private float avoidanceRadiusFactor = 0.5f;
        #endregion
        #endregion

        #region Private
        /// <summary>
        /// Max Speed Squared
        /// </summary>
        private float squareMaxSpeed;
        /// <summary>
        /// Neighbour-Radius Squared
        /// </summary>
        private float squareNeighbourRadius;
        /// <summary>
        /// Avoidance-Radius Squared
        /// </summary>
        private float squareAvoidanceRadius;
        /// <summary>
        /// List of Rockets in Flock
        /// </summary>
        private List<Rocket> flock = new List<Rocket>();
        #endregion
        #endregion

        #region Methods
        #region Public
        /// <summary>
        /// Gets Current Weight for a specific BoidParamType
        /// <para>
        /// Returns Weight from first available Behaviour of Type
        /// </para>
        /// </summary>
        /// <param name="type">BoidParamType to get Weight for</param>
        /// <returns></returns>
        public float GetWeight(BoidParamType type)
        {
            if (flockBehaviour as ComplexFlockBehaviour != null)
                return ((ComplexFlockBehaviour)flockBehaviour).GetWeight(type);
            return 0;
        }

        /// <summary>
        /// Sets Weight for a specific BoidParamType
        /// <para>
        /// Sets Weight to first available Behaviour of Type
        /// </para>
        /// </summary>
        /// <param name="type">BoidParamType to set Weight for</param>
        /// <param name="weight">Weight to Set</param>
        public void SetWeight(BoidParamType type, float weight)
        {
            if (flockBehaviour as ComplexFlockBehaviour != null)
            {
                ((ComplexFlockBehaviour)flockBehaviour).SetWeight(type, weight);
            }
        }
        #endregion

        #region Unity
        /// <summary>
        /// Initializes FlockController
        /// <para>
        /// Calculates cached Squares and Spawns Flock
        /// </para>
        /// </summary>
        private void Start()
        {
            // Pre-calc squares for performance reasons
            squareMaxSpeed = maxSpeed * maxSpeed;
            squareNeighbourRadius = neighbourRadius * neighbourRadius;
            squareAvoidanceRadius = squareNeighbourRadius * (avoidanceRadiusFactor * avoidanceRadiusFactor);
            // Allow for overhead in list-size in case extras are spawned later (Future-feature?)
            flock = new List<Rocket>((int)(initialFlockSize * 1.5f));
            // Spawn initial Flock
            SpawnRockets(initialFlockSize);
        }
        /// <summary>
        /// Runs FlockBehaviour to Update Rockets in Flock
        /// </summary>
        private void Update()
        {
            for (int i = 0; i < flock.Count; i++)
            {
                Rocket rocket = flock[i];
                // Get Neighbours
                List<Transform> neighbours = GetNeighbours(rocket);
                // Calculate Move from Behaviour
                Vector2 move = flockBehaviour.CalculateMove(rocket, neighbours, this);
                // Factor in Acceleration
                move *= driveFactor;
                // Factor in MaxSpeed
                if (move.sqrMagnitude >= squareMaxSpeed)
                    move = move.normalized * squareMaxSpeed;
                // Move Rocket
                rocket.MoveRocket(move);
            }
        }
        #endregion

        #region Private
        /// <summary>
        /// Spawns Rockets for Flock (Adds to current Flock)
        /// <para>
        ///     Rockets are Spawned in a radius around Controller-Position, using density
        /// </para>
        /// </summary>
        /// <param name="amount">Amount to Spawn</param>
        private void SpawnRockets(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                GameObject newRocket = Instantiate(
                    rocketPrefab,
                    UnityEngine.Random.insideUnitCircle * amount * density,
                    Quaternion.Euler(Vector3.forward * UnityEngine.Random.Range(0, 360f)),
                    transform);
                newRocket.name = $"Rocket {i}";
                flock.Add(newRocket.GetComponent<Rocket>());
            }
        }
        /// <summary>
        /// Gets Neighbours for Rocket through Collision-Check
        /// </summary>
        /// <param name="rocket">Rocket to get Neighbours for</param>
        /// <returns>List of Neighbours</returns>
        private List<Transform> GetNeighbours(Rocket rocket)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(rocket.transform.position, neighbourRadius, ~rocket.gameObject.layer);
            List<Transform> result = new List<Transform>();
            for (int i = 0; i < colliders.Length; i++)
                if (colliders[i] != rocket.RocketCollider)
                    result.Add(colliders[i].transform);
            return result;
        }
        #endregion
        #endregion
    }
}