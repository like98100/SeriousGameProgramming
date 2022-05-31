using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    AudioSource audioSource;
    string value;
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.5f;

        if(this.gameObject.name == "Main Camera")
        {
            Debug.Log("메인카메라 사운드");
            value = "stage" + (GameStatus.stage+1).ToString();
            audioSource.clip = Resources.Load<AudioClip>("Sounds/" + value);
            audioSource.playOnAwake = true;
            audioSource.Play();
        }
    }

    public static void SetSound(AudioSource path, string val)
    {
        string value = val;
        path.clip = Resources.Load<AudioClip>("Sounds/" + value);
        path.Play();
    }
}
