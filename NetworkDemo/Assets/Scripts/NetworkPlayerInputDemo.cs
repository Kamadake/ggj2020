using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayerInputDemo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * 50 * Time.deltaTime);
        }
    }
}
