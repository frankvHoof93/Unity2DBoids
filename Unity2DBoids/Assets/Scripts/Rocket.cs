using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Rocket : MonoBehaviour
{
    #region Variables
    public Collider2D RocketCollider { get; private set; }
    #endregion

    #region Methods
    #region Public
    public void MoveRocket(Vector2 velocity)
    {
        // Rotate
        transform.up = velocity;
        // Move
        transform.position += (Vector3)(velocity * Time.deltaTime);
    }
    #endregion

    #region Unity
    private void Awake()
    {
        RocketCollider = GetComponent<Collider2D>();
    }
    #endregion
    #endregion
}
