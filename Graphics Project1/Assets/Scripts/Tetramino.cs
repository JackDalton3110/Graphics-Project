using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetramino : MonoBehaviour {

    float fall = 0;
    private float fallSpeed;
    public bool allowRotation = true;
    public bool limitRotation = false;

    public int blockScore = 100;

    public float blockScoreTime;

    private float continuousVerticalSpeed = 0.05f; //Speed the tetromino moves when the down button is held 
    private float coninuousHorizontalSpeed = 0.1f; // Speed at which the teromino will move when the left or right 
    private float buttonDownWait = 0.2f; //How long to wait befor the shape starts moving when it recognizes a button is held
    private float buttonDownWaitTimer = 0; 

    private float verticalTimer = 0.0f;
    private float horizontalTimer = 0.0f;

    private bool movedImmediateHorizontal = false;
    private bool movedImmediateVertical = false;

    // Use this for initialization
    void Start () {

        fallSpeed = GameObject.Find("Grid").GetComponent<Game>().fallSpeed;

    }

    void UpdateBlockScore()
    {
        if(blockScoreTime<1)
        {
            blockScoreTime += Time.deltaTime;
        }
        else
        {
            blockScoreTime = 0;
            blockScore = Mathf.Max(blockScore - 10, 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
        CheckUserInput();
        UpdateBlockScore();
	}
    void CheckUserInput()
    {  //To increment when player just hits the key once. to override timer code. e.g Sometimes the timer doesnt increment enough so shape wont move.
    //Resets timers and immediate move bools  when player releases key
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            movedImmediateHorizontal = false;
            movedImmediateVertical = false;
            horizontalTimer = 0;
            verticalTimer = 0;
            buttonDownWaitTimer = 0;


        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (movedImmediateHorizontal == true)
            {


                if (buttonDownWaitTimer < buttonDownWait)
                {
                    buttonDownWaitTimer += Time.deltaTime;
                    return;
                }
                //Stepping movement
                if (horizontalTimer < coninuousHorizontalSpeed)
                {
                    horizontalTimer += Time.deltaTime;
                    //Get out of the method
                    return;
                }

            }
            if (movedImmediateHorizontal == false)
            {
                movedImmediateHorizontal = true;
            }
            horizontalTimer = 0;
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
        else  if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (movedImmediateHorizontal == true)
            {
                if (buttonDownWaitTimer < buttonDownWait)
                {
                    buttonDownWaitTimer += Time.deltaTime;
                    return;
                }
                //Stepping movement
                if (horizontalTimer < coninuousHorizontalSpeed)
                {
                    horizontalTimer += Time.deltaTime;
                    //Get out of the method
                    return;
                }
            }
            if (movedImmediateHorizontal == false)
            {
                movedImmediateHorizontal = true;
            }
            horizontalTimer = 0;
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
                        transform.Rotate(0, 0, -90);
                    }
                   
                }

            }

        }
        else if (Input.GetKey(KeyCode.DownArrow) || Time.time - fall >= fallSpeed)
        {
            if (movedImmediateVertical == true)
            {


                if (buttonDownWaitTimer < buttonDownWait)
                {
                    buttonDownWaitTimer += Time.deltaTime;
                    return;
                }
                if (verticalTimer < continuousVerticalSpeed)
                {
                    verticalTimer += Time.deltaTime;
                    return;
                }
            }
            if (movedImmediateVertical == false)
            {
                movedImmediateVertical = true;
            }
            verticalTimer = 0;
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

                if(FindObjectOfType<Game>().CheckIsAboveGrid(this))
                {
                    FindObjectOfType<Game>().Gameover();
                }

                FindObjectOfType< Game > ().SpawnNextTetromino();

                Game.currentScore += blockScore;
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
