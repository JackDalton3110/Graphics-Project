/*
 * Author: Aaron Curry, Jack Dalton
 * Date: 2/12/2017-04/12/2017
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    public static int gridHeight = 20;
    public static int gridWidth = 10;

    //Score values for line destruction
    public int oneLine = 40;
    public int twoLines = 100;
    public int threeLines = 300;
    public int fourLines = 1200;

    public Text Score;

    private int numOfRows = 0;

    public static int currentScore = 0;

    private GameObject preview;
    private GameObject nextShape;

    private bool gameStarted = false;

    private Vector3 previewPos = new Vector3(-6.5f, 15f, 0.0f);

    private AudioSource audioSource;
    public AudioClip removeLine;



    public static Transform[,] grid = new Transform[gridWidth, gridHeight];

	// Use this for initialization
	void Start () {
        SpawnNextTetromino();
        audioSource = GetComponent<AudioSource>();
	}

    //Checks if blocks are above grid for game over
    public bool CheckIsAboveGrid(Tetramino tetromino)
    {
        for (int x = 0; x<gridWidth; x++)
        {
            foreach (Transform mino in tetromino.transform)
            {
                Vector2 pos = Round(mino.position);
                if(pos.y > gridHeight -1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    //Checks is row full
    public bool IsFullRowAt(int y)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }
        numOfRows++;
        return true;
    }

    //Deletes blocks
    public void DeleteMinoAt(int y)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }
    //Moves rows down after row beneath has been destroyed
    public void MoveRowDown(int y)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }

    }
    public void MoveAllRowsDown(int y)
    {
        for (int i = y; i < gridHeight; ++i)
        {
            MoveRowDown(i);
            
        }


    }
    public void DeleteRow()
    {
        for (int y = 0; y < gridHeight; ++y)
        {
            if (IsFullRowAt(y))
            {
                DeleteMinoAt(y);
                MoveAllRowsDown(y + 1);
               
                --y;
            }
        }
    }
    public void updateGrid(Tetramino tetromino)
    {
        for (int y = 0; y < gridHeight; ++y)
        {
            for (int x = 0; x < gridWidth; ++x)
            {
                if (grid[x, y] != null)
                {
                    if (grid[x, y].parent = tetromino.transform)
                    {
                        grid[x, y] = null;
                    }
                }

            }
        }

        foreach(Transform mino in tetromino.transform)
        {
            Vector3 pos = Round(mino.position);

            if (pos.y < gridHeight)
            {
                grid[(int)pos.x, (int)pos.y] = mino;
            }


        }
    }
    public Transform GetTransformAtGridPosition(Vector3 pos)
    {
        if (pos.y > gridHeight -1)
        {
            return null;
        }
        else
        {
            return grid[(int)pos.x, (int)pos.y];
        }

    }
    public bool CheckIsInsideGrid(Vector3 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridWidth && (int)pos.y >= 0);
    }

    public Vector2 Round (Vector3 pos)
    {

        return new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
    }
    string GetRandomTetrimino()
    {
        int randomTetromino = Random.Range(1, 8);
        string randomTetrominoName = "Tetromino_T";

        switch (randomTetromino)
        {
            case 1:
                randomTetrominoName = "Prefabs/Tetromino_T";
                break;
            case 2:
                randomTetrominoName = "Prefabs/Tetromino_Long";
                break;
            case 3:
                randomTetrominoName = "Prefabs/Tetromino_Square";
                break;
            case 4:
                randomTetrominoName = "Prefabs/Tetromino_J";
                break;
            case 5:
                randomTetrominoName = "Prefabs/Tetromino_L";
                break;
            case 6:
                randomTetrominoName = "Prefabs/Tetromino_S";
                break;
            case 7:
                randomTetrominoName = "Prefabs/Tetromino_Z";
                break;
        }
        return randomTetrominoName;
    }

    //Ends game
    public void Gameover()
    {
      SceneManager.LoadScene("GameOver");
    }

    //Sapwns new shape
    public void SpawnNextTetromino()
    {
        if(!gameStarted)
        {
            //Sets new block and preview block before game starts
            gameStarted = true;
            nextShape = (GameObject)Instantiate(Resources.Load(GetRandomTetrimino(), typeof(GameObject)), new Vector3(5.0f, 20.0f, 0.0f), Quaternion.identity);
            preview = (GameObject)Instantiate(Resources.Load(GetRandomTetrimino(), typeof(GameObject)), previewPos, Quaternion.identity);
            preview.GetComponent<Tetramino>().enabled=false;
        }
        else
        {
            //sets preview block as new shape and gets new preview shape
            preview.transform.localPosition = new Vector3(5.0f, 20.0f, 0.0f);
            nextShape = preview;
            nextShape.GetComponent<Tetramino>().enabled = true;

            preview = (GameObject)Instantiate(Resources.Load(GetRandomTetrimino(), typeof(GameObject)), previewPos, Quaternion.identity);
            preview.GetComponent<Tetramino>().enabled = false;
        }
    }

    //Updates score after rows have been deleted
    public void UpdateScore()
    {
        if (numOfRows>0)
        {
            if(numOfRows==1)
            {
                ClearedOne();
            }
            else if(numOfRows==2)
            {
                CleareTwo();
            }
            else if (numOfRows == 3)
            {
                ClearedThree();
            }
            else if (numOfRows == 4)
            {
                ClearedFour();
            }

            numOfRows = 0;
            ClearLineAudio();
        }
       
    }

    //Plays audio when line is cleared
    public void ClearLineAudio()
    {
        audioSource.PlayOneShot(removeLine);
    }

    //changes current score when line is cleared
    void ClearedOne()
    {
        currentScore += oneLine;
    }

    void CleareTwo()
    {
        currentScore += twoLines;
    }
    void ClearedThree()
    {
        currentScore += threeLines;
    }
    void ClearedFour()
    {
        currentScore += fourLines;
    }

    //Update text
    private void Update()
    {
        UpdateScore();
        UpdateUI();
    }

    //Sends score to string
    public void UpdateUI()
    {
        Score.text = currentScore.ToString();
    }

}
