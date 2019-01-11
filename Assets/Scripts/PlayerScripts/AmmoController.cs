using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoController : MonoBehaviour {

    int acutalAmmo { get; set; }
    int totalAmmo { get; set; }
    public Text ammoText;
    private AudioSource audioSource;
    public bool isReloading;

	// Use this for initialization
	void Start () {
        acutalAmmo = 30;
        totalAmmo = 60;
        UpdateAmmoText();
        audioSource = GetComponent<AudioSource>();
        isReloading = false;
	}

    IEnumerator Example(){
        audioSource.Play();
        isReloading = true;
        yield return new WaitForSeconds(audioSource.clip.length);
        Reload();
        isReloading = false;
    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.R) && acutalAmmo < 30 && totalAmmo > 0){
            StartCoroutine(Example());
        }

        UpdateAmmoText();


	}

    void UpdateAmmoText(){
        ammoText.text = acutalAmmo.ToString() + "/" + totalAmmo.ToString();
    }

    public int GetCurrentAmmo() {
        return acutalAmmo;
    }

    public void DecreaseAmmo() {
        if(acutalAmmo > 0){
            acutalAmmo--;
        }
    }

    private void Reload() {
        if(acutalAmmo < 30 && totalAmmo > 0){
            int ammoAdded = 30 - acutalAmmo;
            if(ammoAdded > totalAmmo){
                acutalAmmo += totalAmmo;
                totalAmmo = 0;
            }else{
                totalAmmo -= ammoAdded;
                acutalAmmo = 30;
            }
        }
    }

    public void PickupAmmo(int count) {
        if(totalAmmo + count < 300) {
            totalAmmo += count;
        } else {
            totalAmmo = 300;
        }
    }
}
