using nl.FutureWhiz.Unity2DBoids.Boids.Agents;
using System.Collections.Generic;
using UnityEngine;

namespace nl.FutureWhiz.Unity2DBoids.Boids.FlockBehaviours
{
    /// <summary>
    /// Abstract FlockBehaviour to serve as Base Class for other Behaviours
    /// </summary>
    public abstract class AFlockBehaviour : ScriptableObject
    {
        /// <summary>
        /// Calculates Movement for a Boids-Agent
        /// </summary>
        /// <param name="agent">Agent to Calculate Movement for</param>
        /// <param name="neighbours">Neighbours for Agent</param>
        /// <param name="flock">Flock-Controller</param>
        /// <returns>Movement for Agent</returns>
        public abstract Vector2 CalculateMove(Rocket agent, List<Transform> neighbours, FlockController flock);
    }
}