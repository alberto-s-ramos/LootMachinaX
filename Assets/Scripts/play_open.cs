using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play_open : MonoBehaviour {

    public Animator anim;

    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
            anim.Play("caixa_open");
    }
}
