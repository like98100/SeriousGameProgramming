using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeburfControl : MonoBehaviour
{
    public GameObject DeburfParticle;
    GameObject island, deburfParticleInst;

    Vector3 centerPos;
    float distance, landRadius;
    bool[] deburfActivate = new bool[3] { false, false, false };
    bool isActivated;
    // Start is called before the first frame update
    void Start()
    {
        //curPosition = this.gameObject.transform;
        island = GameObject.Find("Island");
        centerPos = new Vector3(island.transform.position.x, 0f, island.transform.position.z);
        deburfActivate = new bool[3] { false, false, false };

        deburfParticleInst = Instantiate(DeburfParticle, this.gameObject.transform.position, this.gameObject.transform.rotation);
        deburfParticleInst.transform.parent = this.gameObject.transform;

        isActivated = deburfActivate[GameStatus.stage];
    }

    // Update is called once per frame
    void Update()
    {
        distance = (this.gameObject.transform.position - centerPos).magnitude;
        landRadius = island.transform.localScale.x / 2.0f;
        if (distance > landRadius)
        {
            deburfActivate[GameStatus.stage] = true;
            if (!isActivated)
            {
                this.gameObject.GetComponent<AudioSource>().volume = 0.1f;
                SoundControl.SetSound(this.gameObject.GetComponent<AudioSource>(), "MP_롤 소환사 주문 탈진");
                isActivated = true;
            }
        }
        else
        {
            deburfActivate[GameStatus.stage] = false;
            if (isActivated)
            {
                this.gameObject.GetComponent<AudioSource>().volume = 0.1f;
                SoundControl.SetSound(this.gameObject.GetComponent<AudioSource>(), "MP_롤 소환사 주문 유체화");
                isActivated = false;
            }
        }

        deburfParticleInst.SetActive(deburfActivate[GameStatus.stage]);
        //deburfParticleInst.transform.position = this.gameObject.transform.position;
    }

    public bool GetDeburfActivate()
    {
        return this.deburfActivate[GameStatus.stage];
    }
}
