
using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class movement : MonoBehaviour
{

    private Rigidbody rb;
    private int col = 1;
    private int row = 1;
    private int i = 0;
    private bool collisionRight = false;
    private bool collisionLeft = false;
    private int horizontal;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        if (Input.GetKeyDown(key: KeyCode.LeftArrow ) && collisionLeft == false)
        {
            horizontal = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - col);
        }
        if (Input.GetKeyDown(key: KeyCode.RightArrow) && collisionRight == false)
        {
            horizontal = 1;
            i = i + 1;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + col);
        }
        if (Input.GetKeyDown(key: KeyCode.DownArrow) )
        {
            i = i + 1;
            transform.Translate(0,-row ,0);
        }

        

    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Boundary") && horizontal == 0)
        {
            collisionLeft = true;
            // transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 1);

        }
        else
        {
            collisionLeft = false;
        }
        if (other.gameObject.CompareTag("Boundary") && horizontal == 1)
        {
            collisionRight = true;
          

        }
        else
         {
               collisionRight = false;
        }

    }
   


}
