using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Lever : MonoBehaviour {

    private Animator anim, platAnim, doorAnim;
    public Transform player, platform, door2;
    public RawImage imagePull;


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        platAnim = platform.GetComponent<Animator>();
        doorAnim = door2.GetComponent<Animator>();

        anim.speed = 0.2f;
    }
	
    public void OnMouseOver()
    {
        float dist = Vector3.Distance(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z), this.transform.position);
        if (dist < 6)
        {
            imagePull.enabled = true;
        }
    }

    public void OnMouseExit()
    {
        imagePull.enabled = false;
    }


    public void OnMouseUp()
    {
        float dist = Vector3.Distance(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z), this.transform.position);
        if (dist < 6)
        {
            if (platform.GetComponent<PlatformState>().getState().Equals("down"))
            {
                anim.Play("LeverDown");
                platAnim.speed = 0.015f;
                platAnim.Play("Platform1Up");
                platform.GetComponent<PlatformState>().setState("up");

                doorAnim.speed = 0.02f;
                doorAnim.Play("Door2Down");
                door2.GetComponent<Door>().setState("closed");
            }
            else if (platform.GetComponent<PlatformState>().getState().Equals("up"))
            {
                anim.Play("LeverUp");
                Animator platAnim = platform.GetComponent<Animator>();
                platAnim.speed = 0.015f;
                platAnim.Play("Platform1Down");
                platform.GetComponent<PlatformState>().setState("down");

                doorAnim.speed = 0.02f;
                doorAnim.Play("Door2Up");
                door2.GetComponent<Door>().setState("opened");

            }
        }
    }
}
