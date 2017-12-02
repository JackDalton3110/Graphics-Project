using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetramino : MonoBehaviour {

    float fall = 0;
    public float fallSpeed = 1;
    public bool allowRotation = true;
    public bool limitRotation = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CheckUserInput();
	}
    void CheckUserInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);

            if (CheckIsValidPosition())
            {
               // FindObjectOfType<Game>().updateGrid(this);
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
            
        }
        else  if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);

            if (CheckIsValidPosition())
            {
               // FindObjectOfType<Game>().updateGrid(this);
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (allowRotation)
            {
                if (limitRotation)
                {
                    if (transform.rotation.eulerAngles.z >= 90)
                    {
                        transform.Rotate(0, 0, -90);

                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }
                else
                {
                    transform.Rotate(0, 0, 90);
                }
                if (CheckIsValidPosition())
                {
                    //FindObjectOfType<Game>().updateGrid(this);
                }
               //Stops rotating out of grid
                else
                {
                    if (limitRotation)
                    {
                        if (transform.rotation.eulerAngles.z >= 90)
                        {
                            transform.Rotate(0, 0, -90);
                        }
                        else
                        {
                            transform.Rotate(0, 0, 90);
                        }
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                   
                }

            }

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - fall >= fallSpeed)
        {
            transform.position += new Vector3(0, -1, 0);
            if (CheckIsValidPosition())
            {
              //  FindObjectOfType<Game>().updateGrid(this);
            }
            else
            {
                
                transform.position += new Vector3(0, 1, 0);
                
                enabled = false;
                FindObjectOfType<Game>().updateGrid(this);
                FindObjectOfType<Game>().DeleteRow();

                FindObjectOfType< Game > ().SpawnNextTetromino();
            }
            fall = Time.time;
        }
    }

    //Checl position of each mino (child)
    bool CheckIsValidPosition()
    {
        foreach (Transform mino in transform)
        {
            Vector3 pos = FindObjectOfType<Game>().Round(mino.position);
            Debug.Log(pos);
            if (FindObjectOfType<Game>().CheckIsInsideGrid(pos) == false)
            {
               return false;
            }
            if (FindObjectOfType<Game>().GetTransformAtGridPosition(pos) != null && FindObjectOfType<Game>().GetTransformAtGridPosition(pos).parent != transform)
            {
                return false;
            }
        }
        return true;
    }
}
