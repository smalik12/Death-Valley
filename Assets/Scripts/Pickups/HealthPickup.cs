using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

    private PlayerHealth healthScript;

    // Use this for initialization
    void Start () {
        healthScript = GameObject.Find("FirstPersonCharacter").GetComponent<PlayerHealth>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PickUp() {
    
            healthScript.AddHealth(10);
            Destroy(this.gameObject);
    }
}
