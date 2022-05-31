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

    public void GoPreMenual()
    {
        int idx = GameObject.Find("GameSystem").GetComponent<Menual>().GetFlag();

        if (idx > 0) GameObject.Find("GameSystem").GetComponent<Menual>().SetFlag(false);
        Debug.Log(GameObject.Find("GameSystem").GetComponent<Menual>().GetFlag());
    }

    public void GoNextMenual()
    {
        Debug.Log("��������");
        int idx = GameObject.Find("GameSystem").GetComponent<Menual>().GetFlag();

        if (idx < 2) GameObject.Find("GameSystem").GetComponent<Menual>().SetFlag(true);
        Debug.Log(GameObject.Find("GameSystem").GetComponent<Menual>().GetFlag());
    }

    private void Update()
    {
    }

}
