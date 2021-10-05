using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _objToFollow;
    [SerializeField] private float _smoothFactor = 0.02f;

    private Vector3 _deltaPos;    
    
    private void Start()
    {
        _deltaPos = transform.position - _objToFollow.position;
    }
        
    private void Update()
    {
        Vector3 newPos = _objToFollow.position + _deltaPos;
        transform.position = Vector3.Slerp(transform.position, newPos, _smoothFactor);
    }
}
