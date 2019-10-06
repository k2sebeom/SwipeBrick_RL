using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class SwipeAgent : Agent
{
    public int NumBall = 1;
    private int NumCheck = 1;
    private int CurrBall = 0;
    public int ReturnedBall = 0;
    public GameObject Ball;
    public ScannerScript Scanner;
    public Vector2 LaunchVel;
    public Vector2 LaunchPos;
    private float BallSpeed = 15f;
    public bool Ready = false;
    public bool isPlayer = false;

    public int original = 0;
    public int broken = 0;

    public GameObject GameRoom;

    public override void CollectObservations()
    {
        AddVectorObs((float)NumBall / Scanner.level);
        AddVectorObs(Mathf.Clamp(transform.localPosition.x / 8f, -1f, 1f));
    }

    public override void AgentReset()
    {
        transform.localPosition = new Vector2(3.74f, -1.15f);
        NumBall = 1;
        Scanner.ResetScanner();
        //Learning
        //Ready = true;
    }

    private Vector2 Ind2Angle(int a)
    {
        float angle = (-80f + (160f / 30f * a)) * Mathf.Deg2Rad;
        return new Vector2(BallSpeed * Mathf.Sin(angle), BallSpeed * Mathf.Cos(angle));
    }

    public bool isFirst()
    {
        return ReturnedBall == 1;
    }

    public bool isEnd()
    {
        return ReturnedBall == NumCheck;
    }

    public void AddResult()
    {
        if (broken == 0 && NumBall == NumCheck)
        {
            AddReward(-1f);
        }
        else
        {
            AddReward((float)broken / original);
        }
        //Debug.LogFormat("In this turn, it broke {0} of {1} blocks, adding {2} reward.", broken, original, (float)broken / original);
        broken = 0;
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        if (!isPlayer)
        {
            int action = Mathf.FloorToInt(vectorAction[0]);
            LaunchVel = Ind2Angle(action);
        }
        CurrBall = 0;
        LaunchPos = transform.localPosition;
        original = Scanner.NumBlocks();
        ReturnedBall = 0;
        NumCheck = NumBall;
        InvokeRepeating("Launch", 0f, 0.05f);
    }

    void GetReady()
    {
        Ready = true;
    }

    private void Update()
    {
        //Monitor.Log("Record", GetCumulativeReward(), transform);
        transform.GetChild(0).GetComponent<TextMesh>().text = "x" + NumBall.ToString();
    }

    private void Launch()
    {
        BallController newBall = GameObject.Instantiate(Ball, GameRoom.transform).GetComponent<BallController>();
        newBall.GetComponent<Rigidbody2D>().velocity = LaunchVel;
        newBall.transform.localPosition = LaunchPos;
        newBall.player = this;
        CurrBall++;
        if(CurrBall >= NumCheck)
        {
            CancelInvoke("Launch");
        }
    }
}
