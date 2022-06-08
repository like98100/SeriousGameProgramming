using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseImage : MonoBehaviour
{
    GameObject pauseImage;
    // Start is called before the first frame update
    void Start()
    {
        pauseImage = GameObject.Find("Canvas/PauseImage");
        pauseImage.transform.localScale = new Vector3(0, 0, 0);
    }

    public void SetPauseImage(float idx)
    {
        if (this.gameObject.GetComponent<Menual>().GetMenual()) return;

        if (idx == 0f) pauseImage.transform.localScale = new Vector3(1f, 1f, 1f);
        else pauseImage.transform.localScale = new Vector3(0, 0, 0);

        Time.timeScale = idx;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
