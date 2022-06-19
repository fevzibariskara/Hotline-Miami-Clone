using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    static CameraController me;
    Camera mainCam;
    bool followPlayer;
    Transform player;
    public static CameraController Me()
    {
        if (me == null)
        {
            me = FindObjectOfType<CameraController>();
        }

        return me;
    }

    public Camera GetMainCamera()
    {
        if (mainCam == null)
        {
            mainCam = Camera.main;
        }

        return mainCam;
    }

    public void SetPlayerToFollow(Transform t)
    {
        followPlayer = true;
        player = t;
    }

    void FollowPlayer()
    {
        if (followPlayer)
        {
            this.transform.position = new Vector3(player.position.x, player.position.y, this.transform.position.z);
        }
    }

    private void Update()
    {
        FollowPlayer();
    }
}
