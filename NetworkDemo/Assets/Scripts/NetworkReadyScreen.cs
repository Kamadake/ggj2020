using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkReadyScreen : MonoBehaviour
{
    public Button UIButton;

    public void ReadyUpButton()
    {
        Text buttonText = UIButton.GetComponentInChildren<Text>();
        if(PlayerNetworkManager.getInstance().PlayerNetworkID == -1)
        {
            PlayerNetworkManager.getInstance().SetAsReady();
            buttonText.text = "Ready";
        } else
        {
            PlayerNetworkManager.getInstance().SetAsNotReady();
            buttonText.text = "Not Ready";
        }
    }
}
