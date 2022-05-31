using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketDamage : MonoBehaviour
{
    private GameStatus game_status = null;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        this.game_status = GameObject.Find("GameRoot").GetComponent<GameStatus>();
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        float damageTime = 1.0f;

        if(GameObject.Find("Island").transform.localScale.x < 0.0f)
        {
            time += Time.deltaTime;
            if(time >= damageTime)
            {
                this.game_status.addRepairment(-0.01f);
                time = 0;
            }
        }
    }
}
