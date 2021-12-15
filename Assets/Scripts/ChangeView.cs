using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This script is used to change the perspective of the portals
*/

public class ChangeView : MonoBehaviour
{

    public Player player;
    public Transform camera;

    public float cameraRotation;
    public Vector3 portalPos;
    public Vector3 playerPos;
    public float angle;
    float xAngle;
    float zAngle;

    // Start is called before the first frame update
    void Start()
    {
        portalPos = transform.position;
        cameraRotation = camera.eulerAngles.y;
        xAngle = camera.eulerAngles.x;
        zAngle = camera.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        angle = Mathf.Rad2Deg*Mathf.Atan(Mathf.Abs(playerPos.x - portalPos.x) / Mathf.Abs(portalPos.z - playerPos.z));
        //Finds angle between player and portal for perspective shift
        camera.transform.localRotation = Quaternion.Euler(xAngle,angle - 50, zAngle);
    }
}
