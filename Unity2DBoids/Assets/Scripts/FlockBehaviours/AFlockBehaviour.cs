using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

public abstract class AFlockBehaviour : ScriptableObject
{
    public abstract Vector2 CalculateMove(Rocket agent, List<Transform> neighbours, FlockController flock);
}