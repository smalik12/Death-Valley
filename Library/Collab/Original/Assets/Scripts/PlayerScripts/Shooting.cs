using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

    public float fireRate = 0.25f;

    public Camera fpsCam;
    public WaitForSeconds shotDuration = new WaitForSeconds(.07f);

    public float nextFire;
    public GameObject muzzelSpawn;
    public GameObject muzzelFlash;
    public GameObject holdFlash;

	// Use this for initialization
	void Start () {
        fpsCam = GetComponentInParent<Camera>();
        muzzelSpawn = this.transform.Find("MuzzlePosition").gameObject;

	}
	
	// Update is called once per frame
	void Update () {

        //Check to make sure there is a weapon. If they have the WeaponStats script then they have a weapon attached
        if(transform.GetComponentInChildren<WeaponStats>() != null){
            //Use the script to get the range and damange of the current weapon
            WeaponStats weaponStats = transform.GetComponentInChildren<WeaponStats>();
            float gunRange = weaponStats.getRange();
            float damage = weaponStats.getDamage();

            //Check if user is pressing fire button and time between fires has been met
            if (Input.GetButtonDown("Fire1") && Time.time > nextFire) {

                //used to make sure user isnt firing too fast.
                nextFire = Time.time + fireRate;
                holdFlash = Instantiate(muzzelFlash, muzzelSpawn.transform.position, muzzelSpawn.transform.rotation * Quaternion.EulerAngles(0, 0, 90)) as GameObject;
                holdFlash.transform.parent = muzzelSpawn.transform;
                weaponStats.playGunFire();

                Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));// gives you a point at exact center of camera viewpoint
                RaycastHit hit;

                if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, gunRange)) {
                    //if we hit an object with the tag "Enemy" 
                    if (hit.transform.tag == "Enemy") {
                        EnemyTemp enemy = hit.transform.gameObject.GetComponent<EnemyTemp>();
                        enemy.takeDamage();

                        Debug.Log("Hit Enemy");
                        Debug.Log(enemy.printHealth());
                    }
                }
            }
        }
	}

}
