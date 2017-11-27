/*
 * Author: Jack Dalton
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class Movement : MonoBehaviour {

    private Rigidbody rb;
    public Boundary boundary;
    public int speed;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine("move");
	}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Boundary"))
        {
        
            StopCoroutine("move");

        }

    }

    IEnumerator move()
    {
        for (;;)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 4.0f, transform.localPosition.z);

            yield return new WaitForSeconds(1);
        }    
    }
}
