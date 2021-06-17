using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    public float time = 0.9f;
    void Start()
    {
        Destroy(this.gameObject, 5);
    }

    private void FixedUpdate()
    {
        if (time > 0)
        {
            transform.Translate(Vector3.forward * 3 * Time.fixedDeltaTime);
            time -= Time.fixedDeltaTime;
        }
    }
}
