using nl.FutureWhiz.Unity2DBoids.Boids.FlockBehaviours;
using nl.FutureWhiz.Unity2DBoids.Boids.FlockBehaviours.Alignment;
using nl.FutureWhiz.Unity2DBoids.Boids.FlockBehaviours.Coherence;
using nl.FutureWhiz.Unity2DBoids.Boids.FlockBehaviours.Separation;
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Utils are small pieces of code that can be re-used throughout the Project
/// </summary>
namespace nl.FutureWhiz.Unity2DBoids.Utils
{
    /// <summary>
    /// Type for Boid-Parameters
    /// </summary>
    public enum BoidParamType
    {
        Cohesion,
        Separation,
        Alignment
    }

    /// <summary>
    /// Extension Methods for BoidParamType-Enum
    /// </summary>
    public static class BoidParamTypeExtensions
    {
        /// <summary>
        /// Classes per Type
        /// </summary>
        private static Dictionary<BoidParamType, Type[]> behaviourTypes = new Dictionary<BoidParamType, Type[]>
        {
            { BoidParamType.Alignment, new Type[] { typeof(AlignmentBehaviour) } },
            { BoidParamType.Cohesion, new Type[] { typeof(CohesionBehaviour), typeof(SmoothCohesionBehaviour) } },
            { BoidParamType.Separation, new Type[] { typeof(AvoidanceBehaviour) } },
        };

        /// <summary>
        /// Checks whether an AFlockBehaviour is of a certain BoidParamType
        /// </summary>
        /// <param name="type">Type to Check for</param>
        /// <param name="behaviour">AFlockBehaviour to check</param>
        /// <returns>True if Behaviour is of BoidParamType</returns>
        public static bool IsClassOfType(this BoidParamType type, AFlockBehaviour behaviour)
        {
            Type[] types = behaviourTypes[type];
            return types.Contains(behaviour.GetType());
        }
    }
}
