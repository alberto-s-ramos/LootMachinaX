using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DemoButtons : MonoBehaviour {

    public Transform player, whereTo;
    public GameObject demoTrails, demo2Portal;
    public RawImage skillBar, Skill1;


    public void clicked(){
        if (this.gameObject.name.Equals("DemoButton1"))
        {
            demo2Portal.SetActive(false);
            demoTrails.SetActive(false);
            whereTo.gameObject.SetActive(false);

        }
        else if (this.gameObject.name.Equals("DemoButton2"))
        {
            demo2Portal.SetActive(false);
            demoTrails.SetActive(true);
            whereTo.gameObject.SetActive(false);

        }
        else if (this.gameObject.name.Equals("DemoButton3"))
        {
            demoTrails.SetActive(false);
            demo2Portal.SetActive(true);
            player.GetComponent<PlayerState>().setCanJump(true);
            player.GetComponent<PlayerState>().setCanWalkFast(true);
            player.GetComponent<PlayerState>().setCanSuperJump(true);
            player.GetComponent<Animator>().SetBool("Walk Forward", false);
            player.transform.position = whereTo.transform.position;
            skillBar.enabled = true;
            Skill1.enabled = true;

        }
    }
    


}
