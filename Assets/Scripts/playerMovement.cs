using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public static playerMovement Instance;

    public bool MoveByTouch;
    private Vector3 _mouseStartPos, PlayerStartPos;
    [Range(0f, 100f)] public float maxAcceleration;
    private Vector3 move, direction;
    public Transform target; // the player will look at this object, which is in front of it

    public GameObject candle;
    private List<GameObject> candles = new List<GameObject>();
    private int candleCount = 0;

    public bool gameOver = false;

    void Awake()
    {
        gameOver = false;
        Instance = this; 
    }

    void Start()
    {
        candles.Add(gameObject);
        maxAcceleration = 0.05f;
    }

    void FixedUpdate()
    {

        transform.position += new Vector3(0, 0, 1f) * Time.deltaTime * 15;

        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, 0f);

            float Distance;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out Distance))
            {
                _mouseStartPos = ray.GetPoint(Distance);
                PlayerStartPos = transform.position;
            }

            MoveByTouch = true;

        }
        else if (Input.GetMouseButtonUp(0))
        {
            MoveByTouch = false;
        }

        if (MoveByTouch)
        {
            Plane plane = new Plane(Vector3.up, 0f);
            float Distance;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (plane.Raycast(ray, out Distance))
            {
                var newray = ray.GetPoint(Distance);
                move = newray - _mouseStartPos;
                var controller = PlayerStartPos + move;

                controller.x = Mathf.Clamp(controller.x, -4.72f, 4.8f);


                var TargetNewPos = target.position;

                TargetNewPos.x = Mathf.MoveTowards(TargetNewPos.x, controller.x, 80f * Time.deltaTime);
                TargetNewPos.z = Mathf.MoveTowards(TargetNewPos.z, 1000f, 10f * Time.deltaTime);

                target.position = TargetNewPos;

                var PlayerNewPos = transform.position;

                PlayerNewPos.x = Mathf.MoveTowards(PlayerNewPos.x, controller.x, 5 * Time.deltaTime);
                //PlayerNewPos.z = Mathf.MoveTowards(PlayerNewPos.z, 1000f, 10f * Time.deltaTime);
                transform.position = PlayerNewPos;
            }
        }

    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Candle")
        {
            Destroy(coll.gameObject);
            GameObject candleGO = Instantiate(candle, new Vector3(candles[candleCount].transform.position.x, candles[candleCount].transform.position.y+0.25f, candles[candleCount].transform.position.z), Quaternion.identity);
            candleGO.transform.parent = gameObject.transform;
            candles.Add(candleGO);
            candleCount++;
        }

        if (coll.gameObject.tag == "Obstacle")
        {
            Destroy(candles[candleCount]);
            candleCount--;
            if (candleCount <= 0)
            {
                gameOver = true;
                Debug.LogError("GAME OVER");
            }
        }
    }
}