using nl.FutureWhiz.Unity2DBoids.Boids.Agents;
using nl.FutureWhiz.Unity2DBoids.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behaviours used to create Flocking
/// </summary>
namespace nl.FutureWhiz.Unity2DBoids.Boids.FlockBehaviours
{
    /// <summary>
    /// Complex Behaviour containing multiple Sub-Behaviours
    /// </summary>
    [CreateAssetMenu(menuName = "Flock/Behaviours/Complex")]
    public class ComplexFlockBehaviour : AFlockBehaviour
    {
        #region Variables
        #region Editor
        /// <summary>
        /// Behaviours to Run
        /// </summary>
        [SerializeField]
        [Tooltip("Behaviours to Run")]
        private AFlockBehaviour[] behaviours;
        /// <summary>
        /// Weights for each Behaviour
        /// </summary>
        [SerializeField]
        [Tooltip("Weights for each Behaviour")]
        private float[] weights;
        #endregion
        #endregion

        #region Methods
        #region Public
        /// <summary>
        /// Calculates Movement for Agent by adding up Movements from Sub-Behaviours
        /// </summary>
        /// <param name="agent">Agent to calculate Movement for</param>
        /// <param name="neighbours">Neighbours for Agent</param>
        /// <param name="flock">Controller for Flock</param>
        /// <returns>Movement for Agent</returns>
        public override Vector2 CalculateMove(Rocket agent, List<Transform> neighbours, FlockController flock)
        {
            if (weights.Length != behaviours.Length)
                throw new InvalidOperationException("Invalid Settings for ComplexFlockBehaviour (Weights do not match Behaviours)");
            Vector2 move = Vector2.zero;
            for (int i = 0; i < behaviours.Length; i++)
            {
                float weight = weights[i];
                Vector2 behaviourMove = behaviours[i].CalculateMove(agent, neighbours, flock) * weight;
                if (behaviourMove != Vector2.zero && behaviourMove.sqrMagnitude > (weight * weight))
                {
                    behaviourMove.Normalize();
                    behaviourMove *= weight;
                }
                move += behaviourMove;
            }
            return move;
        }
        #endregion

        #region Internal
        /// <summary>
        /// Sets Weight to Sub-Behaviour based on ParamType
        /// <para>
        /// Sets weight to First Behaviour of Type
        /// </para>
        /// </summary>
        /// <param name="type">ParamType for Weight</param>
        /// <param name="weight">Weight to Set</param>
        internal void SetWeight(BoidParamType type, float weight)
        {
            for (int i = 0; i < behaviours.Length; i++)
                if (type.IsClassOfType(behaviours[i]))
                {
                    weights[i] = weight;
                    return;
                }
        }
        /// <summary>
        /// Gets Weight from Sub-Behaviour based on ParamType
        /// <para>
        /// Gets weight from First Behaviour of Type
        /// </para>
        /// </summary>
        /// <param name="type">ParamType for Weight</param>
        /// <returns>Weight for ParamType, or 0 if none could be found</returns>
        internal float GetWeight(BoidParamType type)
        {
            for (int i = 0; i < behaviours.Length; i++)
                if (type.IsClassOfType(behaviours[i]))
                    return weights[i];
            return 0;
        }
        #endregion
        #endregion
    }
}