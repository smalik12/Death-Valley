using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = .5f;

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	

    private void Update() {
        float mouseX = (Input.mousePosition.x / Screen.width) - 0.5f;
        float mouseY = (Input.mousePosition.y / Screen.height) - 0.5f;
        transform.localRotation = Quaternion.Euler(new Vector4(-1f * (mouseY * 180f), mouseX * 360f, transform.localRotation.z));

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(vertical > 0){
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }else if(vertical < 0){
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }

        if(horizontal > 0){
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }else if(horizontal < 0){
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }


}

