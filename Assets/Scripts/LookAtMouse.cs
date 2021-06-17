using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    [SerializeField]
    Camera _camera;
    private static Vector3 targetPoint;
    void Update()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (playerPlane.Raycast(ray, out float hitdist) && PlayerController.isLife)
        {
            targetPoint = ray.GetPoint(hitdist);
            Debug.DrawLine(ray.origin, targetPoint, Color.green);
            transform.LookAt(new Vector3(targetPoint.x, targetPoint.y, targetPoint.z));
        }
    }

    //public static void StopLooking() => targetPoint = new Vector3(0,0,0);
}
