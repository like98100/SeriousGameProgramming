using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    // 스테이지 단계
    public static int stage;

    // 철광석, 식물을 사용했을 때 각각의 수리 정도.
    public static float[] GAIN_REPAIRMENT_IRON = new float[3] { 0.5f, 0.4f, 0.4f };
    public static float[] GAIN_REPAIRMENT_PLANT = new float[3] { 0.15f, 0.15f, 0.2f };

    // 적의 우주선 손상 대미지
    public static float[] DAMAGEENEMY = new float[3] { 0.01f, 0.02f, 0.02f };

    // 철광석, 사과, 식물을 운반했을 때 각각의 체력 소모 정도.
    public static float[] CONSUME_SATIETY_IRON = new float[3] { 0.05f, 0.05f, 0.05f };
    public static float[] CONSUME_SATIETY_APPLE = new float[3] { 0.02f, 0.02f, 0.02f };
    public static float[] CONSUME_SATIETY_PLANT = new float[3] { 0.01f, 0.01f, 0.01f };

    // 아래 변수 추가
    public static float[] CONSUME_SATIETY_ALWAYS = new float[3] { 0.01f, 0.01f, 0.01f };

    // 사과, 식물을 먹었을 때 각각의 체력 회복 정도.
    public static float[] REGAIN_SATIETY_APPLE = new float[3] { 0.7f, 0.7f, 0.9f };
    public static float[] REGAIN_SATIETY_PLANT = new float[3] { 0.25f, 0.25f, 0.3f }; // 0.2->0.3

    public float repairment = 0.2f; // 우주선의 수리 정도(0.0f~1.0f).
    public float satiety = 1.0f; // 배고픔,체력(0.0f~1.0f).
    public GUIStyle guistyle; // 폰트 스타일.

    // Start is called before the first frame update
    void Start()
    {
        this.guistyle.fontSize = 24; // 폰트 크기를 24로.
        repairment = 0.2f;
        satiety = 1.0f;
    }

    void OnGUI()
    {
        float x = Screen.width * 0.2f;
        float y = 20.0f;
        // 체력을 표시.
        GUI.Label(new Rect(x, y, 200.0f, 20.0f), "체력:" +
        (this.satiety * 100.0f).ToString("000"), guistyle);
        x += 200;
        // 수리 정도를 표시.
        GUI.Label(new Rect(x, y, 200.0f, 20.0f),
        "로켓 :" + (this.repairment * 100.0f).ToString("000"), guistyle);
    }
    // 우주선 수리를 진행
    public void addRepairment(float add)
    {
        this.repairment = Mathf.Clamp01(this.repairment + add); // 0.0~1.0 강제 지정
    }
    // 체력을 늘리거나 줄임
    public void addSatiety(float add)
    {
        this.satiety = Mathf.Clamp01(this.satiety + add);
    }

    // 게임을 클리어했는지 검사
    public bool isGameClear()
    {
        bool is_clear = false;
        if (this.repairment >= 1.0f)
        { // 수리 정도가 100% 이상이면.
            is_clear = true; // 클리어했다.
        }
        return (is_clear);
    }
    // 게임이 끝났는지 검사
    public bool isGameOver()
    {
        bool is_over = false;
        if (this.satiety <= 0.0f || this.repairment <= 0.0f)
        { // 체력이 0이하거나 우주선의 수리도가 0 이하라면.
            is_over = true; // 게임 오버.
        }
        return (is_over);
    }

    // 배를 고프게 하는 메서드 추가
    public void alwaysSatiety()
    {
        this.satiety = Mathf.Clamp01(this.satiety - CONSUME_SATIETY_ALWAYS[stage] * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
