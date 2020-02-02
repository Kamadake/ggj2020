using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float oxygen = 100;
    public float oxygenDurationSeconds = 60;
    private float _currentOxygen;

    public List<string> inventory;

    // Start is called before the first frame update
    void Start()
    {
        _currentOxygen = oxygen;
    }

    public void AddInventory(GameObject gameObject)
    {
        inventory.Add(gameObject.tag);
        Debug.Log("Added " + gameObject.name + " to inventory.");
    }

    public void AddOxygen(float amount)
    {
        _currentOxygen = Mathf.Clamp(_currentOxygen += amount, 0, oxygen);
        Debug.Log("Replenished " + amount + " oxygen: " + _currentOxygen);
    }

    // Update is called once per frame
    void Update()
    {
        _currentOxygen -= (oxygen / oxygenDurationSeconds) * Time.deltaTime;
        if (_currentOxygen < 30)
        {
            Debug.Log("Oxygen Low: " + _currentOxygen);
        }
    }
}
