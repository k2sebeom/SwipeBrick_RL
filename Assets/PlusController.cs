using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusController : BlockController
{
    private bool touched = false;

    private void Start()
    {
        transform.localPosition = new Vector2(RelPos.x * 1.5f, RelPos.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector2(RelPos.x * 1.5f, RelPos.y);
        if(RelPos.y < 0)
        {
            EraseBlock();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (!touched)
            {
                touched = true;
                Player.NumBall++;
            }
            EraseBlock();
        }
    }
}
