using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menual : MonoBehaviour
{
    float time;
    bool isMenual = false;
    GameObject menual;

    // Start is called before the first frame update
    void Start()
    {
        menual = GameObject.Find("Canvas/MenualImage");
        menual.transform.localScale = new Vector3(0, 0, 0);
    }

    public void SetMenual()
    {
        this.isMenual = !this.isMenual;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMenual && menual.transform.localScale.x < 1.0f)
        {
            
            menual.transform.localScale = new Vector3(time, time, time);
            time += 5.0f * Time.deltaTime;
        }
        else if (!isMenual && menual.transform.localScale.x > 0f)
        {
            menual.transform.localScale = new Vector3(time, time, time);
            time -= 5.0f * Time.deltaTime;
        }

        if(isMenual && menual.transform.localScale.x <= 1.0f) menual.transform.localScale = new Vector3(1f, 1f, 1f);
        if (!isMenual && menual.transform.localScale.x >= 0f) menual.transform.localScale = new Vector3(0, 0, 0);
    }
}
