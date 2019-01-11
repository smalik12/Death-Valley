using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour {
    public Texture2D crosshairImage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI() {
        float xMin = (Screen.width - Input.mousePosition.x) - (crosshairImage.width / 2);
        float yMin = (Screen.height - Input.mousePosition.y) - (crosshairImage.height / 2);
        GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
    }
}
