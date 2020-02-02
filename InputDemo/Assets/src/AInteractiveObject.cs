using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AInteractiveObject : MonoBehaviour
{
    public string registerToTag = "Player";

    // Register gameObject on target
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == registerToTag)
        {
            other.gameObject.GetComponent<AListener>().AddInteractiveObject(this.gameObject);
            TriggerEnterAction(other.gameObject);
        }
    }

    // De-register gameObject on target
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == registerToTag)
        {
            other.gameObject.GetComponent<AListener>().RemoveInteractiveObject(this.gameObject);
            TriggerExitAction(other.gameObject);
        }
    }

    // Custom action to perform when trigger is activated
    public abstract void TriggerEnterAction(GameObject gameObject);

    public abstract void TriggerExitAction(GameObject gameObject);
}
