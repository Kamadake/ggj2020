using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AListener : MonoBehaviour
{
    private List<GameObject> interactiveObjects = new List<GameObject>();

    public void AddInteractiveObject(GameObject gameObj)
    {
        interactiveObjects.Add(gameObj);
    }

    public void RemoveInteractiveObject(GameObject gameObject)
    {
        interactiveObjects.Remove(gameObject);
    }

    public List<GameObject> GetInteractiveObjects ()
    {
        return interactiveObjects;
    }
}
