using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    // �������� �ܰ�
    public static int stage;

    // ö����, �Ĺ��� ������� �� ������ ���� ����.
    public static float[] GAIN_REPAIRMENT_IRON = new float[3] { 0.5f, 0.4f, 0.4f };
    public static float[] GAIN_REPAIRMENT_PLANT = new float[3] { 0.15f, 0.15f, 0.2f };

    // ���� ���ּ� �ջ� �����
    public static float[] DAMAGEENEMY = new float[3] { 0.01f, 0.02f, 0.02f };

    // ö����, ���, �Ĺ��� ������� �� ������ ü�� �Ҹ� ����.
    public static float[] CONSUME_SATIETY_IRON = new float[3] { 0.05f, 0.05f, 0.05f };
    public static float[] CONSUME_SATIETY_APPLE = new float[3] { 0.02f, 0.02f, 0.02f };
    public static float[] CONSUME_SATIETY_PLANT = new float[3] { 0.01f, 0.01f, 0.01f };

    // �Ʒ� ���� �߰�
    public static float[] CONSUME_SATIETY_ALWAYS = new float[3] { 0.01f, 0.01f, 0.01f };

    // ���, �Ĺ��� �Ծ��� �� ������ ü�� ȸ�� ����.
    public static float[] REGAIN_SATIETY_APPLE = new float[3] { 0.7f, 0.7f, 0.9f };
    public static float[] REGAIN_SATIETY_PLANT = new float[3] { 0.25f, 0.25f, 0.3f }; // 0.2->0.3

    public float repairment = 0.2f; // ���ּ��� ���� ����(0.0f~1.0f).
    public float satiety = 1.0f; // �����,ü��(0.0f~1.0f).
    public GUIStyle guistyle; // ��Ʈ ��Ÿ��.

    // Start is called before the first frame update
    void Start()
    {
        this.guistyle.fontSize = 24; // ��Ʈ ũ�⸦ 24��.
        repairment = 0.2f;
        satiety = 1.0f;
    }

    void OnGUI()
    {
        float x = Screen.width * 0.2f;
        float y = 20.0f;
        // ü���� ǥ��.
        GUI.Label(new Rect(x, y, 200.0f, 20.0f), "ü��:" +
        (this.satiety * 100.0f).ToString("000"), guistyle);
        x += 200;
        // ���� ������ ǥ��.
        GUI.Label(new Rect(x, y, 200.0f, 20.0f),
        "���� :" + (this.repairment * 100.0f).ToString("000"), guistyle);
    }
    // ���ּ� ������ ����
    public void addRepairment(float add)
    {
        this.repairment = Mathf.Clamp01(this.repairment + add); // 0.0~1.0 ���� ����
    }
    // ü���� �ø��ų� ����
    public void addSatiety(float add)
    {
        this.satiety = Mathf.Clamp01(this.satiety + add);
    }

    // ������ Ŭ�����ߴ��� �˻�
    public bool isGameClear()
    {
        bool is_clear = false;
        if (this.repairment >= 1.0f)
        { // ���� ������ 100% �̻��̸�.
            is_clear = true; // Ŭ�����ߴ�.
        }
        return (is_clear);
    }
    // ������ �������� �˻�
    public bool isGameOver()
    {
        bool is_over = false;
        if (this.satiety <= 0.0f || this.repairment <= 0.0f)
        { // ü���� 0���ϰų� ���ּ��� �������� 0 ���϶��.
            is_over = true; // ���� ����.
        }
        return (is_over);
    }

    // �踦 ������ �ϴ� �޼��� �߰�
    public void alwaysSatiety()
    {
        this.satiety = Mathf.Clamp01(this.satiety - CONSUME_SATIETY_ALWAYS[stage] * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
