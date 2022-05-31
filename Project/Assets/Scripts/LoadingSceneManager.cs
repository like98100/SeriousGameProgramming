using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LoadingSceneManager : MonoBehaviour
{
    public static string nextScene; // ������ �ҷ��� ���� �̸�

    [SerializeField]
    Image progressBar;      //�ε� ��

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());    // �ε� �� ȣ�� �Լ��� �ڷ�ƾ���� ����
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;      // ���� �� �̸� ����
        SceneManager.LoadScene("LoadingScene"); // �ε� �� ȣ��
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene); // �ε� �� ���� ������ ��
        op.allowSceneActivation = false;    // ����� �غ�� ��� Ȱ��ȭ(���⼭ X�ϴ� ������ �ε��� ������ ������ ���� �����ϱ� ����)

        float timer = 0f;
        while(!op.isDone)   // �� �ε��� ������ �ʾ��� ��
        {
            yield return null;  // ������� �Ѱ��� ����ٰ� �������� ����� ������

            timer += Time.deltaTime;
            if (op.progress < 0.9f)  // �ε��� ������ 90% ������ ��
            {
                //progressBar.fillAmount = op.progress; // ���൵ ��ŭ ä��

                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);    // timer ���� ���缭 fillamount���� op.progress���� ä��
                if (progressBar.fillAmount >= op.progress)   // �ε� ���غ��� �ٰ� �� ä������ ��
                {
                    timer = 0f; // Ÿ�̸� �ʱ�ȭ
                }
            }
            else // �ε��� ������ 90% �̻��� ��
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer); // timer ���� ���缭 fillAmount���� 100%���� ä��
                if (progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true; // �� �̵� ���
                    yield break;    // �ݺ��� Ż��
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
