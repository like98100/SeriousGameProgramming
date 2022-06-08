using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Button : MonoBehaviour
{
    Scene curScene;
    string root;
    private void Start()
    {
        curScene = SceneManager.GetActiveScene();
        if (curScene.name == "IngameScene") root = "GameRoot";
        else root = "GameSystem";
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
        GameObject.Find(root).GetComponent<Menual>().SetMenual();
    }

    public void GoPreMenual()
    {
        int idx = GameObject.Find(root).GetComponent<Menual>().GetFlag();

        if (idx > 0) GameObject.Find(root).GetComponent<Menual>().SetFlag(false);
        Debug.Log(GameObject.Find(root).GetComponent<Menual>().GetFlag());
    }

    public void GoNextMenual()
    {
        Debug.Log("��������");
        int idx = GameObject.Find(root).GetComponent<Menual>().GetFlag();

        if (idx < 3) GameObject.Find(root).GetComponent<Menual>().SetFlag(true);
        Debug.Log(GameObject.Find(root).GetComponent<Menual>().GetFlag());
    }

    private void Update()
    {
    }

}
