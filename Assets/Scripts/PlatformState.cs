using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformState : MonoBehaviour {

    public string state;

    void Start()
    {
        state = "down";
    }

    public void setState(string newState)
    {
        this.state = newState;
    }
    public string getState()
    {
        return state;
    }
}
