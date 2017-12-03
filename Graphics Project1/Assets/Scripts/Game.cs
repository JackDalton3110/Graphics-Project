using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public static int gridHeight = 20;
    public static int gridWidth = 10;

    public int oneLine = 40;
    public int twoLines = 100;
    public int threeLines = 300;
    public int fourLines = 1200;

    public Text Score;

    private int numOfRows = 0;

    public static int currentScore = 0;

    public static Transform[,] grid = new Transform[gridWidth, gridHeight];
	// Use this for initialization
	void Start () {
        SpawnNextTetromino();
	}

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
    public void DeleteMinoAt(int y)
    {
        for (int x = 0; x < gridWidth; ++x)
        {
            Destroy(grid[x, y].gameObject);

            grid[x, y] = null;
        }
    }
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

    public void Gameover()
    {
        Application.LoadLevel("GameOver");
    }


    public void SpawnNextTetromino()
    {
        GameObject nextTetremino = (GameObject)Instantiate(Resources.Load(GetRandomTetrimino(), typeof(GameObject)), new Vector3(5.0f, 20.0f, 0.0f), Quaternion.identity);
    }

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
        }
    }

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

    private void Update()
    {
        UpdateScore();
        UpdateUI();
    }

    public void UpdateUI()
    {
        Score.text = currentScore.ToString();
    }

}
