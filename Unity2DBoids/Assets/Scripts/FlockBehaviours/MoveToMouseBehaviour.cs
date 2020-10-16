using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviours/Move To Mouse")]
public class MoveToMouseBehaviour : AFlockBehaviour
{
    [SerializeField]
    private float radius = 1.25f;

    public override Vector2 CalculateMove(Rocket agent, List<Transform> neighbours, FlockController flock)
    {
        // TODO: Touch Input
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 offset = mousePos - (Vector2)agent.transform.position;
            float t = offset.magnitude / radius;

            if (t < 0.9f)
                return Vector2.zero;

            return offset * (t * t);
        }
        else return Vector2.zero;
    }
}
