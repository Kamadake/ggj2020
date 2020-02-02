using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IO_Ladder : AInteractiveObject
{
    public override void TriggerEnterAction(GameObject gameObject)
    {
        Debug.Log("Climb Ladder");
    }

    public override void TriggerExitAction(GameObject gameObject)
    {

    }
}
