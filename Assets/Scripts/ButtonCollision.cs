using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCollision : MonoBehaviour {

    private Animator animator;
    public Transform door;

    public void OnCollisionEnter(Collision collision)
    {
        print("Collided");
        if( collision.collider.gameObject.name.Equals("Character")){
            animator = GetComponent<Animator>();

            Animator doorAnim = door.GetComponent<Animator>();
            if (door.GetComponent<Door>().getState().Equals("closed"))
            {
                door.GetComponent<Door>().setState("opened");
                animator.Play("Button2A1");
                doorAnim.Play("Door2Up");
            }
            else if (door.GetComponent<Door>().getState().Equals("opened"))
            {
                door.GetComponent<Door>().setState("closed");
                animator.Play("Button2A2");
                doorAnim.Play("Door2Down");
            }
        }
    }




}
