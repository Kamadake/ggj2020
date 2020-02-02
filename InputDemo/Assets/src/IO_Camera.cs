using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IO_Camera : AInteractiveObject
{
    public Camera camera = new Camera();

    public override void TriggerEnterAction(GameObject gameObject)
    {
        if (camera == null) return;

        if (camera.enabled)
        {
            camera.enabled = false;
        }
        else
        {
            camera.enabled = true;
        }
    }

    public override void TriggerExitAction(GameObject gameObject)
    {
        camera.enabled = false;
    }
}
