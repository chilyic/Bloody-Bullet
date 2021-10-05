using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    [SerializeField] Camera _camera;
    private Vector3 _targetPoint;

    private void Update()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (playerPlane.Raycast(ray, out float hitdist) && PlayerController.isLife && Time.timeScale == 1)
        {
            _targetPoint = ray.GetPoint(hitdist);
            Debug.DrawLine(ray.origin, _targetPoint, Color.green);
            
            transform.LookAt(new Vector3(_targetPoint.x, _targetPoint.y, _targetPoint.z));
        }
    }
}
