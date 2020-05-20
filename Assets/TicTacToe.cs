using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToe : MonoBehaviour
{
    Sidequest_TicTacToe Sidequest_TicTacToe;

    [SerializeField]
    List<SpriteRenderer> sprites;
    int[][] grid;
    List<int> AIDrawQueue;
    bool playInProgress;
    bool allowDraw;

    float timeBetweenDraw = 2f;
    float currentTime = 0f;

    [SerializeField]
    Sprite O;
    [SerializeField]
    Sprite X;

    AudioSource AudioSource;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        Sidequest_TicTacToe = transform.parent.GetComponentInChildren<Sidequest_TicTacToe>();
        playInProgress = false;
        grid = new int[3][];
        for(int i = 0; i < 3; i++)
        {
            grid[i] = new int[3];
        }
        AIDrawQueue = new List<int>();
        for(int i = 0; i < 9; i++)
        {
            AIDrawQueue.Add(i);
        }
        allowDraw = false;
    }

    private void Update()
    {
        if(playInProgress == true && allowDraw == false)
        {
            print("Play in progress: " + playInProgress + " allowDraw: " + allowDraw);
            currentTime += Time.deltaTime;
            if(currentTime >= timeBetweenDraw)
            {
                currentTime = 0f;
                allowDraw = true;
                DrawX();
            }
        }
    }

    public void StartGame()
    {
        playInProgress = true;
        allowDraw = false;
        ResetBoard();
    }

    public void DrawX()
    {
        int index = Random.Range(0, AIDrawQueue.Count); //Get index
        int position = AIDrawQueue[index];
        if(sprites[position].sprite != null)
        {
            print("BAD CODE DETECTED");
        }
        AudioSource.Play();
        sprites[position].sprite = X;
        int row = position / 3;
        int col = position % 3;
        grid[row][col] = 2;
        AIDrawQueue.Remove(position);
        CheckBoard();
    }

    public void DrawO(int row, int col)
    {
        if(grid[row][col] == 0 && allowDraw && playInProgress)
        {
            int position = row * 3 + col;
            sprites[position].sprite = O;
            grid[row][col] = 1;
            AIDrawQueue.Remove(position);
            allowDraw = false;
            CheckBoard();
        }
    }

    public void CheckBoard()
    {
        if (winsX())
        {
            Sidequest_TicTacToe.PlayerLost();
            playInProgress = false;
            allowDraw = false;
        }
        else if (winsO())
        {
            Sidequest_TicTacToe.PlayerWon();
            playInProgress = false;
            allowDraw = false;
        }
        else if (gridFull())
        {
            Sidequest_TicTacToe.PlayerLost();
            playInProgress = false;
            allowDraw = false;
        }
    }

    public void ResetBoard()
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                grid[i][j] = 0;
            }
        }
        AIDrawQueue.Clear();
        for(int i = 0; i < 9; i++)
        {
            AIDrawQueue.Add(i);
            sprites[i].sprite = null;
        }
    }

    private bool winsX()
    {
        if((grid[0][0] == 2 && grid[0][1] == 2 && grid[0][2] == 2) ||
            (grid[1][0] == 2 && grid[1][1] == 2 && grid[1][2] == 2) ||
            (grid[2][0] == 2 && grid[2][1] == 2 && grid[2][2] == 2) ||
            (grid[0][0] == 2 && grid[1][0] == 2 && grid[2][0] == 2) ||
            (grid[0][1] == 2 && grid[1][1] == 2 && grid[2][1] == 2) ||
            (grid[0][2] == 2 && grid[1][2] == 2 && grid[2][2] == 2) ||
            (grid[0][0] == 2 && grid[1][1] == 2 && grid[2][2] == 2) ||
            (grid[0][2] == 2 && grid[1][1] == 2 && grid[2][0] == 2)
            )
        {
            return true;
        }
        return false;
    }
    private bool winsO()
    {
        if ((grid[0][0] == 1 && grid[0][1] == 1 && grid[0][2] == 1) ||
            (grid[1][0] == 1 && grid[1][1] == 1 && grid[1][2] == 1) ||
            (grid[2][0] == 1 && grid[2][1] == 1 && grid[2][2] == 1) ||
            (grid[0][0] == 1 && grid[1][0] == 1 && grid[2][0] == 1) ||
            (grid[0][1] == 1 && grid[1][1] == 1 && grid[2][1] == 1) ||
            (grid[0][2] == 1 && grid[1][2] == 1 && grid[2][2] == 1) ||
            (grid[0][0] == 1 && grid[1][1] == 1 && grid[2][2] == 1) ||
            (grid[0][2] == 1 && grid[1][1] == 1 && grid[2][0] == 1)
            )
        {
            return true;
        }
        return false;
    }

    private bool gridFull()
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if(grid[i][j] == 0)
                {
                    return false;
                }
            }
        }
        return true;
    }

}
