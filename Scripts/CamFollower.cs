using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollower : MonoBehaviour
{

    public GameObject player;
    Vector3 velocity = Vector3.zero;

    void Update()
    {
        //camera follow with small delay
        Vector3 follow = new Vector3(player.transform.position.x, player.transform.position.y,-10);
        //function for smooth following
        transform.position = Vector3.SmoothDamp(transform.position, follow, ref velocity, 0.1f);

    }
}
