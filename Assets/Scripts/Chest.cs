using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class Chest : MonoBehaviour {

    private Animator anim, playerAnim;
    public ParticleSystem PS;
    public Transform player;
    public string nameOfAnimation;
    public RawImage imageClickToOpen, imageOpened, imageUpgrade, imageExplain, skillBar, Skill1, Skill2, Skill3;
    private AudioSource audioSource;
    public AudioClip audioClip;
    public bool closed = true;

    public enum State{closed, opened, inbetween}
    public State state;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        playerAnim = player.GetComponent<Animator>();
        state = Chest.State.closed;
        closed = true;
        PS.Stop();
        audioSource = GetComponent<AudioSource>();
	}

    public void OnMouseOver()
    {
        float dist = Vector3.Distance(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z), this.transform.position);
        if(dist<6){
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

    public void OnMouseExit(){
    
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
        if (closed==true)
        {
            open();
        }
        else if (closed==false)
        {
            imageExplain.enabled = true;
            StartCoroutine(TimeToDisplay());
        }
    }

    private void open()
    {
        float dist = Vector3.Distance(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z), this.transform.position);
        if (dist < 6)
        {
            anim.Play("Open");
            PS.Play();
            if (nameOfAnimation == "Jump W Root")
            {
                if (closed == true)
                {
                    player.GetComponent<PlayerState>().setCanJump(true);
                    playerAnim.Play(nameOfAnimation);
                    imageExplain.enabled = true;
                    audioSource.PlayOneShot(audioClip);
                    StartCoroutine(TimeToDisplay());
                }
         
            }
            else if (nameOfAnimation == "Run Forward W Root")
            {
                if (closed == true)
                {
                    player.GetComponent<PlayerState>().setCanWalkFast(true);
                    player.GetComponent<Animator>().SetBool("Walk Forward", false);
                    StartCoroutine(TimeToDisplay());
                    audioSource.PlayOneShot(audioClip);

                }

            }
            else if (nameOfAnimation == "Super Jump")
            {
                if (closed == true)
                {
                    skillBar.enabled = true;
                    Skill1.enabled = true;
                    player.GetComponent<PlayerState>().setCanSuperJump(true);
                    imageExplain.enabled = true;
                    audioSource.PlayOneShot(audioClip);
                    StartCoroutine(TimeToDisplay());

                }
             
            }
            else if (nameOfAnimation == "Dash")
            {
                if (closed == true)
                {
                    Skill2.enabled = true;
                    player.GetComponent<PlayerState>().setCanDash(true);
                    imageExplain.enabled = true;
                    audioSource.PlayOneShot(audioClip);
                    StartCoroutine(TimeToDisplay());

                }

            }
            else if (nameOfAnimation == "Break")
            {
                if (closed == true)
                {
                    Skill3.enabled = true;
                    player.GetComponent<PlayerState>().setCanBreak(true);
                    audioSource.PlayOneShot(audioClip);
                    StartCoroutine(TimeToDisplay());

                }
           
            }
            imageExplain.enabled = true;
            imageUpgrade.enabled = true;

            closed = false;

            }
        }


    IEnumerator TimeToDisplay(){
        yield return new WaitForSeconds(3);
   
            imageUpgrade.enabled = false;
            imageExplain.enabled = false;
    }


}
