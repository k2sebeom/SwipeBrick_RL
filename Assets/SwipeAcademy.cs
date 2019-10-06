using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwipeAcademy : Academy
{
    public float MaxBlock = 2f;
    public int[] BlockLine;
    public ScannerScript Scanner1;
    public ScannerScript Scanner2;
    public bool Ready = false;
    public Text Result;
    public GameObject ReButton;

    public override void AcademyReset()
    {
        //Monitor.SetActive(true);
        ReButton.SetActive(false);
    }

    private void Update()
    {
        if(Scanner1.Ready && Scanner2.Ready)
        {
            newLine();
            Scanner1.NextLine();
            Scanner2.NextLine();
            Scanner1.Ready = false;
            Scanner2.Ready = false;
            Ready = true;
        }
        if (Scanner1.GameOver)
        {
            Result.text = "You Win";
            ReButton.SetActive(true);
        }
        else if (Scanner2.GameOver)
        {
            Result.text = "Machine Wins";
            ReButton.SetActive(true);
        }
    }

    public int[] newLine()
    {
        int[] line = new int[6] { 0, 0, 0, 0, 0, 0 };
        int b = Random.Range(1, 4);
        float bp = Random.Range(0f, 1f);
        if (bp < 0.1f)
        {
            b = 4;
        }
        for (int i = 0; i < b; i++)
        {
            int j = (int)Random.Range(0, 6f);

            while (line[j] != 0)
            {
                j = (int)Random.Range(0, 6f);
            }
            line[j] = 1;
        }
        int p = Random.Range(0, 6);
        while (line[p] != 0)
        {
            p = Random.Range(0, 6);
        }
        line[p] = -1;

        BlockLine = line;
        return line;
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }
}
