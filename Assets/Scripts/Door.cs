using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public string state;
    public AudioClip audioClip;
    private AudioSource audioSource;


    void Start()
    {
        state = "closed";
        audioSource = GetComponent<AudioSource>();
    }


    public void setState(string newState){
        this.state = newState;
        audioSource.PlayOneShot(audioClip);
    }
    public string getState(){
        return state;
    }


}
