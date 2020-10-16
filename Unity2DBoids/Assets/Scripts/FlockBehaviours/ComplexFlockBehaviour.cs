using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Complex")]
public class ComplexFlockBehaviour : AFlockBehaviour
{
    [SerializeField]
    private AFlockBehaviour[] behaviours;
    [SerializeField]
    private float[] weights;

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
}
