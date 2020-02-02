using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    private Camera _camera;

    void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 point = _camera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }

    }
}
