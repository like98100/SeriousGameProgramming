using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menual : MonoBehaviour
{
    float time;
    bool isMenual = false;
    GameObject menual, menualArray;
    int flag;

    // Start is called before the first frame update
    void Start()
    {
        menual = GameObject.Find("Canvas/MenualImage");
        menualArray = GameObject.Find("Canvas/MenualImage/MenualArray");
        menual.transform.localScale = new Vector3(0, 0, 0);

        flag = 0;
        SetMenualArray();
    }

    public int GetFlag()
    {
        return this.flag;
    }

    public void SetFlag(bool direction)
    {
        if (direction) flag++;
        else flag--;

        SetMenualArray();
    }

    public void SetMenual()
    {
        this.isMenual = !this.isMenual;
    }

    void SetMenualArray()
    {
        for(int idx = 0; idx < 3; idx++)
        {
            if(idx == flag) menualArray.transform.GetChild(idx).gameObject.SetActive(true);
            else menualArray.transform.GetChild(idx).gameObject.SetActive(false);
        }
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
