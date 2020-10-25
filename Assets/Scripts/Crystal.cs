using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Crystal : MonoBehaviour {

    public Transform player;
    public RawImage breakCrystal, breakableCrystal;

    public void OnMouseOver()
    {
        float dist = Vector3.Distance(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z), this.transform.position);
        if (dist < 6)
        {
            if(player.GetComponent<PlayerState>().getCanBreak()==true){
                breakCrystal.enabled = true;
                player.GetComponent<PlayerController>().setCrystal(this.gameObject);
            }
            else if(player.GetComponent<PlayerState>().getCanBreak() == false)
            {
                breakableCrystal.enabled = true;
            }
        }

    }

    public void OnMouseExit()
    {
        breakCrystal.enabled = false;
        breakableCrystal.enabled = false;
        player.GetComponent<PlayerController>().setCrystal(GameObject.Find("C_Default"));
    }


}
