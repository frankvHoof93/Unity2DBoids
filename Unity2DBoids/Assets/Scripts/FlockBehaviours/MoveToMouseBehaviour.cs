using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Flock/Behaviours/Move To Mouse")]
public class MoveToMouseBehaviour : AFlockBehaviour
{
    [SerializeField]
    private float radius = 1.25f;

    public override Vector2 CalculateMove(Rocket agent, List<Transform> neighbours, FlockController flock)
    {
        // Check for Input
        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            // Check if not using Canvas-UI
            if (EventSystem.current.IsPointerOverGameObject())
                return Vector2.zero; // Ignore Touch
            Vector2? mousePos = null;
            // Prioritise Touch
            if (Input.touchCount > 0)
            {
                // Get WorldPos from Touch
                Touch touch = Input.GetTouch(0);
                mousePos = (Vector2)Camera.main.ScreenToWorldPoint(touch.position);
            }
            // FallBack to Mouse
            else if (Input.GetMouseButton(0))
            {
                // Get WorldPos from Mouse
                mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            // No Pos (Should not be possible here)
            if (!mousePos.HasValue)
                return Vector2.zero;
            // Get Offset
            Vector2 offset = mousePos.Value - (Vector2)agent.transform.position;
            float t = offset.magnitude / radius;
            // Leniency
            if (t < 0.9f)
                return Vector2.zero;
            // Return Vector to move to
            return offset * (t * t);
        }
        else return Vector2.zero;
    }
}
