using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D BallBody;
    public SwipeAgent player;

    private void Start()
    {
        BallBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Mathf.Abs(Mathf.Acos(BallBody.velocity.x / BallBody.velocity.magnitude)) < Mathf.Deg2Rad * 2f)
        {
            BallBody.velocity += new Vector2(0, -0.1f);
           
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Finish")
        {
            player.ReturnedBall++;
            if (player.isFirst())
            { 
                player.transform.localPosition = new Vector2(transform.localPosition.x, -1.15f);
            }
            if (player.isEnd())
            {
                player.AddResult();
                player.Ready = true;
            }
            Destroy(gameObject);
        }
    }
}
