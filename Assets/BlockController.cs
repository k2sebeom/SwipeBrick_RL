using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public Vector2 RelPos = new Vector2(1,1);
    public int Number = 1;
    public SwipeAgent Player;
    public ScannerScript Scanner;

    private void Start()
    {
        transform.localPosition = new Vector2(RelPos.x * 1.5f, RelPos.y);
        transform.GetChild(0).GetComponent<TextMesh>().text = Number.ToString();
    }

    public void EraseBlock()
    {
        transform.parent.gameObject.GetComponent<ScannerScript>().Blocks.Remove(this);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector2(RelPos.x * 1.5f, RelPos.y);
        transform.GetChild(0).GetComponent<TextMesh>().text = Number.ToString();
        float r = (float)Number / (float)Scanner.level;
        GetComponent<SpriteRenderer>().color = new Color(1f, 0.7f * 1f - r * r * r, 0f);
        if(Number <= 0)
        {
            //Player.broken++;
            EraseBlock();
        }
        if(RelPos.y < 0)
        {
            Scanner.GameOver = true;
            EraseBlock();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            Player.broken++;
            Number--;
        }
    }
}
