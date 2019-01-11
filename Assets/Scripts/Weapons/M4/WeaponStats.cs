using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponStats : MonoBehaviour {
    public int range = 300;
    public int damage = 10;
    public AudioSource gunFire;

    public GameObject muzzelSpawn;
    public GameObject[] muzzelFlash;
    public GameObject holdFlash;
    public GameObject bullet;
    public Animation recoil;

    public int ammo;
    private AmmoController ammoController;

    private bool isADS;

    // Use this for initialization
    void Start () {
        recoil = GetComponent<Animation>();
        ammoController = GetComponentInParent<AmmoController>();
        isADS = false;

	}


    // Update is called once per frame
    void Update () {
        if(Input.GetButtonDown("Fire2")) {
            if(!isADS){
                isADS = true;
                this.GetComponent<Animation>().Play("Move to ADS");
            }else{
                isADS = false;
                this.GetComponent<Animation>().Play("Move Away ADS");
            }
        }
	}

    public int getRange() {
        return range;
    }

    public int getDamage() {
        return damage;
    }

    public void shoot() {
        if (ammoController.GetCurrentAmmo() > 0 && !ammoController.isReloading){
            int randomNumberForMuzzelFlash = Random.Range(0, 5);
            holdFlash = Instantiate(muzzelFlash[randomNumberForMuzzelFlash], muzzelSpawn.transform.position, muzzelSpawn.transform.rotation * Quaternion.Euler(0, 0, 90)) as GameObject;
            holdFlash.transform.parent = muzzelSpawn.transform;
            if (bullet) {
                Instantiate(bullet, muzzelSpawn.transform.position, muzzelSpawn.transform.rotation);
                if(isADS){
                    this.GetComponent<Animation>().Play("Recoil in ADS");
                }else{
                    this.GetComponent<Animation>().Play("Recoil");
                }
                ammoController.DecreaseAmmo();
            }
            gunFire.Play();
        }
    }
}
