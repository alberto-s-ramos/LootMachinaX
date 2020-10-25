using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StoryBox : MonoBehaviour {


    private Animator anim;
    public ParticleSystem PS;
    public Transform player;
    public RawImage imageClickToOpen, imageOpened, message;
    public GameObject closeButton;
    private AudioSource audioSource;
    public AudioClip audioClip;

    private int nTimes = 0;
    public bool closed = true;

    public enum State{closed, opened, inbetween}
    public State state;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        state = StoryBox.State.closed;
        closed = true;
        PS.Stop();
        audioSource = GetComponent<AudioSource>();

    }


    public void OnMouseOver()
    {
        float dist = Vector3.Distance(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z), this.transform.position);
        if (dist < 6)
        {
            if (closed == true)
            {
                imageClickToOpen.enabled = true;
                imageOpened.enabled = false;
            }
            if (closed == false)
            {
                imageOpened.enabled = true;
                imageClickToOpen.enabled = false;
            }
        }

    }

    public void OnMouseExit()
    {

        if (closed == true)
        {
            imageClickToOpen.enabled = false;

        }
        if (closed == false)
        {
            imageOpened.enabled = false;
        }
    }

    public void OnMouseUp()
    {
        if (state == StoryBox.State.closed)
        {
            open();
        }

    }

    private void open()
    {
        float dist = Vector3.Distance(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z), this.transform.position);
        if (dist < 6)
        {
            anim.Play("Open");
            PS.Play(); 
            if(nTimes==0){
                audioSource.PlayOneShot(audioClip);

            }
            message.enabled = true;
            closeButton.SetActive(true);
            nTimes++;
        }
        closed = false;
        }


    }





