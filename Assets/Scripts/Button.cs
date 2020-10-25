using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Button : MonoBehaviour {

    private Animator animator, door1Anim, door2Anim, door3Anim, platformAnim;
    public Transform player, door1, door2, door3, platform;
    public RawImage imagePress;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        door1Anim = door1.GetComponent<Animator>();
        door2Anim = door2.GetComponent<Animator>();
        door3Anim = door3.GetComponent<Animator>();
        platformAnim = platform.GetComponent<Animator>();
    }

    public void OnMouseOver()
    {
        float dist = Vector3.Distance(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z), this.transform.position);
        if (dist < 6)
        {
            imagePress.enabled = true;
        }

    }

    public void OnMouseExit()
    {
        imagePress.enabled = false;
    }

    public void OnMouseUp()
    {
       float dist = Vector3.Distance(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z), this.transform.position);
        if (dist < 6)
        {
            if (this.gameObject.name.Equals("Button1"))
            {
                animator.speed = 0.3f;
                door1Anim.speed = 0.02f;
                if (door1.GetComponent<Door>().getState().Equals("closed"))
                {
                    door1.GetComponent<Door>().setState("opened");
                    animator.Play("ButtonAnim");
                    door1Anim.Play("Door1Up");
                }
                else if (door1.GetComponent<Door>().getState().Equals("opened"))
                {
                    door1.GetComponent<Door>().setState("closed");
                    animator.Play("ButtonAnim2");
                    door1Anim.Play("Door1Down");
                }
            }
            else if (this.gameObject.name.Equals("Button2"))
            {
                animator.speed = 0.3f;
                door2Anim.speed = 0.02f;
                if (door2.GetComponent<Door>().getState().Equals("closed"))
                {
                    door2.GetComponent<Door>().setState("opened");
                    door1.GetComponent<Door>().setState("closed");
                    animator.Play("Button2Anim");
                    door2Anim.Play("Door2Up");
                    door1Anim.Play("Door1Down");
                }
                else if (door2.GetComponent<Door>().getState().Equals("opened"))
                {
                    door2.GetComponent<Door>().setState("closed");
                    door1.GetComponent<Door>().setState("opened");
                    animator.Play("Button2Anim2");
                    door2Anim.Play("Door2Down");
                    door1Anim.Play("Door1Up");

                }

            }
            else if (this.gameObject.name.Equals("Button3"))
            {
                animator.speed = 0.3f;
                door3Anim.speed = 0.02f;
                if (door3.GetComponent<Door>().getState().Equals("closed"))
                {
                    door3.GetComponent<Door>().setState("opened");
                    animator.Play("Button3Anim");
                    door3Anim.Play("Door3Down");
                    platformAnim.Play("Platform1Down");
                    platform.GetComponent<PlatformState>().setState("down");

                }
                else if (door3.GetComponent<Door>().getState().Equals("opened"))
                {
                    door3.GetComponent<Door>().setState("closed");
                    animator.Play("Button3Anim2");
                    door3Anim.Play("Door3Up");
                    platformAnim.Play("Platform1Up");
                    platform.GetComponent<PlatformState>().setState("up");


                }

            }

        }
    }


}
