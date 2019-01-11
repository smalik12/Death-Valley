using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class GameController : MonoBehaviour {

    private Text killsText;
    private Text wavesText;
    private int kills;
    private int waveNumber;
    private GameObject mCanvas;
    private bool displayWaves;
    private Text diedText;
    private FirstPersonController fpsController;
    private Shooting shootingScript;


    public IEnumerator BlinkText() {
        int i = 0;
        while (i < 5) {
            wavesText.text = "Wave " + waveNumber.ToString();
            yield return new WaitForSeconds(.5f);
            wavesText.text = "";
            yield return new WaitForSeconds(.5f);
            i++;
        }
    }

    // Use this for initialization
    void Start() {
        Time.timeScale = 1;
        kills = 0;
        fpsController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
        shootingScript = GameObject.Find("FPSController").GetComponentInChildren<Shooting>();
        mCanvas = GameObject.FindGameObjectWithTag("MainCanvas");
        for (int i = 0; i < mCanvas.transform.childCount; i++) {
            GameObject child = mCanvas.transform.GetChild(i).gameObject;
            if (child.name == "Kills") {
                killsText = child.GetComponent<Text>();
            }
            if (child.name == "Wave Number") {
                wavesText = child.GetComponent<Text>();
            }
            if(child.name == "Died Text") {
                diedText = child.GetComponent<Text>();
            }
        }
        waveNumber = 1;
        StartCoroutine(BlinkText());

    }

    // Update is called once per frame
    void Update() {
        killsText.text = "Kills: " + kills;
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SceneManager.LoadScene("Main Level");
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    public void updateKill() {
        kills++;
    }

    public void updateLevel() {
        waveNumber++;
        StartCoroutine(BlinkText());
    }

    public void Died() {
        Time.timeScale = 0;
        diedText.text = "You have died. Press 1 to restart";
        fpsController.enabled = false;
        shootingScript.enabled = false;
    }

}
