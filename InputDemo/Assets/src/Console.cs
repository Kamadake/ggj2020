using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : AInteractiveObject
    
{
    public List<string> requiredItems = new List<string>();
    private List<string> submittedItems = new List<string>();

    public void AddItems(List<string> playerInventory)
    {
        List<string> tmp = new List<string>();
        foreach(string item in playerInventory)
        {
            if(!submittedItems.Contains(item) && requiredItems.Contains(item))
            {
                submittedItems.Add(item);
                tmp.Add(item);
                Debug.Log("Submitted " + item);
            }
        }

        foreach(string item in tmp)
        {
            playerInventory.Remove(item);
        }
    }

    public override void TriggerEnterAction(GameObject gameObject)
    {
        foreach(string item in requiredItems)
        {
            // check what items player is missing
            if (submittedItems.Contains(item))
            {
                continue;
            }
            Debug.Log("I still need a " + item);
        }

        // check whether player has items that can be submitted
        foreach(string item in gameObject.GetComponentInParent<PlayerStats>().inventory)
        {
            if (submittedItems.Contains(item))
            {
                continue;
            }
            Debug.Log("I've got some missing parts!");
        }
    }

    public override void TriggerExitAction(GameObject gameObject)
    {
        // hide message
    }
}
