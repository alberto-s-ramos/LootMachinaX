using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Teleport : MonoBehaviour
{

    public GameObject player, closeButton;
    public Transform whereTo;
    public RawImage demoFinalMessage;

    private void OnTriggerEnter(Collider other)
    {
        if(!this.gameObject.name.Equals("TeleportCubeC2")){
            player.transform.position = whereTo.transform.position;
        }
        else if(this.gameObject.name.Equals("TeleportCubeC2")){
            print("final demo");
            player.transform.position = whereTo.transform.position;
            demoFinalMessage.enabled = true;
            closeButton.SetActive(true);
        }
    }

}