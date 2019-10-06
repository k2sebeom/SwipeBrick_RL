using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Line;
    private GameObject Guide;

    private float BallSpeed = 15f;
    private SwipeAgent SAgent;
    public SwipeAgent OtherAgent;
    public SwipeAcademy Aca;

    // Start is called before the first frame update
    void Start()
    {
        SAgent = GetComponent<SwipeAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Aca.Ready)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                Guide = GameObject.Instantiate(Line, transform.position, Quaternion.LookRotation(Vector3.forward, Vector3.Cross(Vector3.forward, dir)));

            }
            if (Input.GetMouseButton(0))
            {
                Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                if (Mathf.Acos(dir.x / dir.magnitude) > Mathf.Deg2Rad * 10f && Mathf.Acos(dir.x / dir.magnitude) < Mathf.Deg2Rad * 170f && dir.y > 0)
                {
                    Guide.transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.Cross(Vector3.forward, dir));
                    SAgent.LaunchVel = dir.normalized * BallSpeed;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                SAgent.RequestAction();
                OtherAgent.RequestDecision();
                Destroy(Guide);
                Aca.Ready = false;
            }
        }
    }
}
