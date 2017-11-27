
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class rotate : MonoBehaviour {

    private Rigidbody rb;
    public Transform target;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        if (Input.GetKeyDown(key: KeyCode.UpArrow))
        {
           
            transform.RotateAround(target.position, Vector3.left, 90);
        }
    }
}
