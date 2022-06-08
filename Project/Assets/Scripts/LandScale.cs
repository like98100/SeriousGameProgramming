using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandScale : MonoBehaviour
{
    float startScale = 65f;
    float time;
    float[] speed;
    GameObject land;
    // Start is called before the first frame update
    void Start()
    {
        land = this.gameObject;
        land.transform.localScale = new Vector3(startScale, 1f, startScale);
        time = 0;
        speed = new float[3] { 0.225f, 0.25f, 0.255f};
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.transform.localScale.x >= 0)
        {
            this.gameObject.transform.localScale = new Vector3(startScale - time, 1f, startScale - time);
            time += Time.deltaTime * speed[GameStatus.stage];
        }
    }
}
