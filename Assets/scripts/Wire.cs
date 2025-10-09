using UnityEngine;

public class Wire : MonoBehaviour
{
    [SerializeField] SpriteRenderer wireEnd;
    private Camera cam;
    private Vector3 pointerOffset;
    private Plane dragPlane;
    Vector3 startPoint;
    Vector3 startPosition;

    // store initial rotation so we can restore it on release
    private Quaternion initialRotation;

    // public field you can reference anywhere, equivalent to "newPosition"
    public Vector3 MouseWorldPosition { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        // record the transform's starting world position
        startPosition = transform.position;
        startPoint = transform.position;

        // store initial rotation
        initialRotation = transform.rotation;
    }

    private void OnMouseDown()
    {
        if (cam == null) cam = Camera.main;
        // Plane that matches the object's plane (use transform.forward so it works for world-space canvas)
        dragPlane = new Plane(transform.forward, transform.position);

        // compute and store the world point under the cursor
        Vector3 hitPoint = ComputeMouseWorldPosition();
        MouseWorldPosition = hitPoint;

        pointerOffset = transform.position - hitPoint;
    }

    private void OnMouseDrag()
    {
        if (cam == null) cam = Camera.main;

        // compute and store the world point under the cursor
        Vector3 hitPoint = ComputeMouseWorldPosition();
        MouseWorldPosition = hitPoint;

        transform.position = hitPoint + pointerOffset;
        // Vector3 direction = new Vector3(0, 0, hitPoint.z - startPoint.z);
        // transform.right = direction * transform.localScale.z;

        // float dist = Vector3.Distance(startPosition, transform.position);
        // wireEnd.size = new Vector3(dist * 435, wireEnd.size.y);
    }
    private void OnMouseUp()
    {
        transform.position = startPosition;
        // restore original rotation instead of setting transform.right
        transform.rotation = initialRotation;
        wireEnd.size = new Vector3(0, wireEnd.size.y);
    }
    // void UpdateWire(Vector3 )
    // {

    // }

    // helper: convert screen point to a world point on a given Plane using a raycast
    private Vector3 ScreenToWorldOnPlane(Vector2 screenPos, Plane plane, Camera c = null)
    {
        if (c == null) c = cam != null ? cam : Camera.main;
        Ray ray = c.ScreenPointToRay(screenPos);
        float enter;
        if (plane.Raycast(ray, out enter))
            return ray.GetPoint(enter);
        return Vector3.zero;
    }

    // helper: equivalent for RectTransform (useful for world-space canvas)
    private bool ScreenPointToWorldPointInRect(RectTransform rect, Vector2 screenPos, out Vector3 worldPoint, Camera c = null)
    {
        if (c == null) c = cam != null ? cam : Camera.main;
        return RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, screenPos, c, out worldPoint);
    }

    // returns the world position under the cursor:
    // - if dragPlane is defined and raycast hits it, returns that hit point
    // - otherwise falls back to ScreenToWorldPoint using this object's Z
    private Vector3 ComputeMouseWorldPosition()
    {
        if (cam == null) cam = Camera.main;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float enter;
        if (dragPlane.normal != Vector3.zero && dragPlane.Raycast(ray, out enter))
        {
            return ray.GetPoint(enter);
        }
        else
        {
            // fallback similar to old code: preserve object's Z
            Vector3 np = cam.ScreenToWorldPoint(Input.mousePosition);
            np.z = transform.position.z;
            return np;
        }
    }
}

