using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{


    private void Start()
    {
    }
    public void StartGame()
    {
        Debug.Log("���� ����");
        GameStatus.stage = 0;
        LoadingSceneManager.LoadScene("IngameScene");
    }

    public void ExitGame()
    {
        Debug.Log("���� ����");
        Application.Quit();
    }

    public void MenualActivate()
    {
        Debug.Log("�޴���");
        GameObject.Find("GameSystem").GetComponent<Menual>().SetMenual();
    }

    private void Update()
    {
    }

}
