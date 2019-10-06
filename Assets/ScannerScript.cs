using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerScript : MonoBehaviour
{
    public List<BlockController> Blocks = new List<BlockController>();
    public GameObject Block;
    public GameObject Plus;
    public int level = 0;
    public float[,] Table = new float[7, 6];
    public SwipeAgent Player;
    public bool GameOver = false;
    private float snooze = 0f;
    private SwipeAcademy Aca;

    public bool Ready = false;

    // Start is called before the first frame update
    void Start()
    {
        level = 0;
        ResetTable();
        Aca = GameObject.Find("SwipeAcademy").GetComponent<SwipeAcademy>();
    }

    public void GetStart()
    {
        Invoke("PlayerReady", 1f);
    }

    void PlayerReady()
    {
        Player.Ready = true;
    }

    void ResetTable()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Table[i, j] = 0;
            }
        }
    }

    void PrintObs()
    {
        string r = "";
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                r += Table[6 -i, j].ToString("F1");
                r += "   ";
            }
            r += "\n";
        }
        r += "Number of Balls: ";
        r += ((float)Player.NumBall / level).ToString("F1");
        //Obs.text = r;
    }

    public void ResetScanner()
    {
        level = 0;
        GameOver = false;
        int n = Blocks.Count;
        for(int i = 0; i < n; i++)
        {
            Blocks[0].EraseBlock();
        }
    } 

    public int NumBlocks()
    {
        int n = 0;
        foreach(BlockController b in Blocks)
        {
            if(b.Number > 0)
            {
                n += b.Number;
            }
        }
        return n;
    }

    public void NextLine()
    {
        level++;
        int[] line = Aca.BlockLine;

        foreach(BlockController block in Blocks)
        {
            block.RelPos.y--;
        }

        for (int i = 0; i < 6; i++)
        {
            if (line[i] > 0) AddBlock(i, 6, level);
            else if (line[i] < 0)
            {
                AddPlus(i, 6);
            }
        }
    }

    void AddBlock(int x, int y, int n)
    {
        Block.GetComponent<BlockController>().RelPos = new Vector2(x, y);
        Block.GetComponent<BlockController>().Number = n;
        BlockController newBlock = GameObject.Instantiate(Block).GetComponent<BlockController>();
        newBlock.gameObject.transform.parent = gameObject.transform;
        newBlock.Player = Player;
        newBlock.Scanner = this;
        Blocks.Add(newBlock);
    }

    void AddPlus(int x, int y)
    {
        Plus.GetComponent<BlockController>().RelPos = new Vector2(x, y);
        Plus.GetComponent<BlockController>().Number = -5;
        PlusController newPlus = GameObject.Instantiate(Plus).GetComponent<PlusController>();
        newPlus.gameObject.transform.parent = gameObject.transform;
        newPlus.Player = Player;
        newPlus.Scanner = this;
        Blocks.Add(newPlus);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver)
        {
            Player.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            Player.AddReward(-1f);
            Player.Done();
        }
        else if (Player.Ready)
        {
            Player.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            Player.Ready = false;
            //NextLine();
            Ready = true;
        }
    }

    void NextAction()
    {
        Player.RequestDecision();
    }
}
