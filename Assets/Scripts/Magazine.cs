using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    [SerializeField] private float _time = 0.9f;

    private void Start()
    {
        Destroy(this.gameObject, 5);
    }

    private void FixedUpdate()
    {
        if (_time > 0)
        {
            transform.Translate(Vector3.forward * 3 * Time.fixedDeltaTime);
            _time -= Time.fixedDeltaTime;
        }
    }
}
