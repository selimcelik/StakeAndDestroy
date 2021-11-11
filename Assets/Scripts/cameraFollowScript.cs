using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;

public class cameraFollowScript : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField]
    private GameObject player;

    public Vector3 offset;

    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!playerMovement.Instance.gameOver)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, player.transform.position.z + offset.z), 3000 * Time.deltaTime);
        }
    }
}
