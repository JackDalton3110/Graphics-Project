using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetramino : MonoBehaviour {

    private AudioSource audioSource;
    float fall = 0;
    public float fallSpeed = 1;
    public bool allowRotation = true;
    public bool limitRotation = false;

    public int blockScore = 100;

    public float blockScoreTime;

    public AudioClip move;
    public AudioClip rotate;
    public AudioClip removeLine; 

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
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
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);

            if (CheckIsValidPosition())
            {
                // FindObjectOfType<Game>().updateGrid(this);
                moveAudio();
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
                moveAudio();
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
                    rotateAudio();
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
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - fall >= fallSpeed)
        {
            transform.position += new Vector3(0, -1, 0);
            if (CheckIsValidPosition())
            {
                //  FindObjectOfType<Game>().updateGrid(this);
                moveAudio();
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

    void moveAudio()
    {
        audioSource.PlayOneShot(move);

    }

    void rotateAudio()
    {
        audioSource.PlayOneShot(rotate);

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
