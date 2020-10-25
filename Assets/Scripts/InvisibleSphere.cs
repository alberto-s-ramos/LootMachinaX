using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleSphere : MonoBehaviour {

    private int state = 0;

    public void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.name.Equals("Character"))
            state = 0;
    }
    public void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.name.Equals("Character"))
            state = 1;
    }

    public int getState(){
        return state;
    }




}
