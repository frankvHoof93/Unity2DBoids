using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Flock/Behaviours/Move To Mouse")]
public class MoveToMouseBehaviour : AFlockBehaviour
{
    [SerializeField]
    private float radius = 1.25f;

    private bool isMouseDown;

    void OnEnable()
    {
        isMouseDown = false;
    }

    void OnDisable()
    {
        isMouseDown = false;
    }

    public override Vector2 CalculateMove(Rocket agent, List<Transform> neighbours, FlockController flock)
    {
        // Negate Previous Input
        if (isMouseDown && (Input.GetMouseButtonUp(0) && Input.touchCount == 0))
            isMouseDown = false;
        // Check for NEW Input
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && !isMouseDown))
            isMouseDown = !EventSystem.current.IsPointerOverGameObject(); // Negate Input if Press was on Canvas instead of Play-Area

        // Handle Input
        if (isMouseDown)
        {
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
