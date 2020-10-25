using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0)){

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if(Physics.Raycast(ray, out hit, 100.0f)){
                if(hit.transform != null){
                    CheckName(hit.transform.gameObject);
                }
            }
        }
	}
     
    private void CheckName(GameObject go){
        print(go.name);
        if(go.name.Contains("Loot Box")){
            Animator objAnimator = go.GetComponent<Animator>();
            objAnimator.SetBool("box_open", true);
        }

    }
}
