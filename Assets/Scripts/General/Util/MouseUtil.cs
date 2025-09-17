using UnityEngine;

public class MouseUtil : MonoBehaviour
{
    public static Camera Camera => UnityEngine.Camera.main;

    public static Vector3 GetMousePositionInWorldSpace(float zValue = 0f)
    {
        Plane dragPlane = new Plane(Vector3.back, new Vector3(0, 0, zValue));
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        if (dragPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        return Vector3.zero;
    }

}
