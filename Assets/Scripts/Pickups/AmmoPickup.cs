using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour {

    private AmmoController ammoScript;


    // Use this for initialization
    void Start () {
        ammoScript = GameObject.Find("FirstPersonCharacter").GetComponent<AmmoController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PickUp() {
    
            ammoScript.PickupAmmo(30);
            Destroy(this.gameObject);
    
    }
}
