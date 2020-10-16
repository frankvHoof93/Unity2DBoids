using nl.FvH.Library.Behaviours;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// FlockController is used to iterate through all rockets, 
/// as the Unity LifeCycle's usage of reflection is far slower than manual iteration
/// 
/// Singleton for easy access
/// </summary>
public class FlockController : SingletonBehaviour<FlockController>
{
    #region Variables
    [SerializeField]
    private AFlockBehaviour flockBehaviour;

    [SerializeField]
    [Range(25, 500)]
    private int initialFlockSize = 50;
    [SerializeField]
    private GameObject rocketPrefab;

    [SerializeField]
    private float density = 0.075f;

    [Range(1f, 100f)]
    [SerializeField]
    private float driveFactor = 10f;
    [Range(1f, 100f)]
    [SerializeField]
    private float maxSpeed = 5f;

    [SerializeField]
    private float neighbourRadius = 1.5f;
    [SerializeField]
    private float avoidanceRadiusFactor = 0.5f;

    float squareMaxSpeed;
    float squareNeighbourRadius;
    float squareAvoidanceRadius;

    public float SquareAvoidanceRadius { get => squareAvoidanceRadius; }

    private List<Rocket> flock = new List<Rocket>();

    #endregion


    #region Methods
    #region Unity
    // Start is called before the first frame update
    void Start()
    {
        // Pre-calc squares for performance reasons
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighbourRadius = neighbourRadius * neighbourRadius;
        squareAvoidanceRadius = squareNeighbourRadius * (avoidanceRadiusFactor * avoidanceRadiusFactor);

        // Allow for overhead in list-size in case extras are spawned later
        flock = new List<Rocket>((int)(initialFlockSize * 1.5f));
        // Spawn initial Flock
        SpawnRockets(initialFlockSize);
    }

    private void Update()
    {
        for (int i = 0; i < flock.Count; i++)
        {
            Rocket rocket = flock[i];
            List<Transform> neighbours = GetNeighbours(rocket);

            //Debug.Log($"Found {neighbours.Count} Neighbours");

            Vector2 move = flockBehaviour.CalculateMove(rocket, neighbours, this);
            move *= driveFactor;
            if (move.sqrMagnitude >= squareMaxSpeed)
            {
                move = move.normalized * squareMaxSpeed;
            }
            rocket.MoveRocket(move);
        }
    }
    #endregion

    #region Private
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