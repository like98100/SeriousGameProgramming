using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    Transform playerPos;
    Vector3[] cameraPos;
    int sum;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Player").transform;
        cameraPos = new Vector3[2];

        switch (this.gameObject.name)
        {
            case "Main Camera":
                sum = 0;
                break;
            case "Minimap Camera":
                sum = 1;
                break;
        }


    }

    // Update is called once per frame
    void Update()
    {
        cameraPos[0] = new Vector3(playerPos.localPosition.x, playerPos.localPosition.y + 7.5f, playerPos.localPosition.z - 8.0f);  // 메인카메라
        cameraPos[1] = new Vector3(playerPos.localPosition.x, playerPos.localPosition.y + 20.0f, playerPos.localPosition.z);        // 미니맵카메라

        this.gameObject.transform.localPosition = cameraPos[sum];
    }
}
