using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    // 아래 변수 추가
    private GameStatus game_status = null;
    private PlayerControl player_control = null;

    private PauseImage pauseImage = null;

    // 아래 변수 추가
    private float GAME_OVER_TIME = 60.0f; // 제한시간은 60초.

    public enum STEP
    { // 게임 상태.
        NONE = -1, // 상태 정보 없음.
        PLAY = 0, // 플레이 중.
        CLEAR, // 클리어 상태.
        GAMEOVER, // 게임 오버 상태.
        NUM, // 상태가 몇 종류인지 나타낸다(=3).
    };

    public STEP step = STEP.NONE; // 현대 단계.
    public STEP next_step = STEP.NONE; // 다음 단계.
    public float step_timer = 0.0f; // 타이머.
    private float clear_time = 0.0f; // 클리어 시간.
    public GUIStyle guistyle; // 폰트 스타일.

    private float pauseScale = 1.0f;

    public float GetPauseScale()
    {
        return pauseScale;
    }

    public void SetPauseScale(float idx)
    {
        pauseScale = idx;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.game_status = this.gameObject.GetComponent<GameStatus>();
        this.player_control =
        GameObject.Find("Player").GetComponent<PlayerControl>();
        this.step = STEP.PLAY;
        this.next_step = STEP.PLAY;
        this.guistyle.fontSize = 64;

        this.pauseImage = this.gameObject.GetComponent<PauseImage>();
    }

    // 게임을 클리어했는지 또는 게임 오버인지 판정하고 게임 상태를 전환
    // Update is called once per frame
    void Update()
    {
        this.step_timer += Time.deltaTime;
        if (this.next_step == STEP.NONE)
        {
            switch (this.step)
            {
                case STEP.PLAY:
                    if (this.game_status.isGameClear())
                    {
                        // 클리어 상태로 이동.
                        this.next_step = STEP.CLEAR;
                    }
                    if (this.game_status.isGameOver())
                    {
                        // 게임 오버 상태로 이동.
                        this.next_step = STEP.GAMEOVER;
                    }
                    //GameObject.Find("Island").transform.localScale.x < 5.0f
                    //if (this.step_timer > GAME_OVER_TIME)
                    //{
                    //    // 제한 시간을 넘었으면 게임 오버.
                    //    this.next_step = STEP.GAMEOVER;
                    //}
                        break;
                // 클리어 시 및 게임 오버 시의 처리.
                case STEP.CLEAR:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        // 마우스 버튼이 눌렸으면 다음 스테이지로 이동한다.
                        GameStatus.stage += 1;
                        Debug.Log(GameStatus.stage);
                        if(GameStatus.stage >= 3) LoadingSceneManager.LoadScene("ClearScene");
                        else LoadingSceneManager.LoadScene("IngameScene");
                    }
                    break;
                case STEP.GAMEOVER:
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        // 마우스 버튼이 눌렸으면 GameScene을 다시 읽는다.
                        //SceneManager.LoadScene("IngameScene");
                        LoadingSceneManager.LoadScene("IngameScene");
                    }
                    else if(Input.GetKeyDown(KeyCode.Escape))
                    {
                        LoadingSceneManager.LoadScene("TitleScene");
                    }
                    break;
            }
        }

        while (this.next_step != STEP.NONE)
        {
            this.step = this.next_step;
            this.next_step = STEP.NONE;
            switch (this.step)
            {
                case STEP.CLEAR:
                    // PlayerControl을 제어 불가로.
                    this.player_control.enabled = false;
                    // 현재의 경과 시간으로 클리어 시간을 갱신.
                    this.clear_time = this.step_timer;

                    break;
                case STEP.GAMEOVER:
                    // PlayerControl를 제어 불가.
                    this.player_control.enabled = false;

                    break;
            }
            this.step_timer = 0.0f;
        }

        // 각 상황에서 반복할 것----------.
        switch (this.step)
        {
            case STEP.PLAY:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Debug.Log("일시정지");
                    if (GetPauseScale() == 1.0f) SetPauseScale(0);
                    else SetPauseScale(1.0f);

                    this.pauseImage.SetPauseImage(GetPauseScale());
                }
                    break;
        }

    }

    void OnGUI()
    {
        float pos_x = Screen.width * 0.1f;
        float pos_y = Screen.height * 0.5f;
        switch (this.step)
        {
            case STEP.PLAY:
                GUI.color = Color.black;
                GUI.Label(new Rect(pos_x, pos_y, 200, 20), // 경과 시간을 표시.
                this.step_timer.ToString("0.00"), guistyle);

                // 제한 시간에 도달할 때까지 남은 시간을 표시.
                //float blast_time = GAME_OVER_TIME - this.step_timer;
                //GUI.Label(new Rect(pos_x, pos_y + 64, 200, 20),
                //blast_time.ToString("0.00"));

                break;
            case STEP.CLEAR:
                GUI.color = Color.black;
                // 클리어 메시지와 클리어 시간 표시.
                GUI.Label(new Rect(pos_x, pos_y, 200, 20),
                "탈출" + this.clear_time.ToString("0.00"), guistyle);

                pos_y -= 32;
                //int ct = (int)clear_time; // 클리어 시간(float)를 int로 변환.
                //if (ct > 50)
                //{ // 50초〜제한시간내.
                //    GUI.Label(new Rect(pos_x, pos_y, 200, 20),
                //    "아슬아슬탈출! 50초 이내를 목표로 하세요!");
                //}
                //else if (ct > 40)
                //{ // 40〜50초.
                //    GUI.Label(new Rect(pos_x, pos_y, 200, 20),
                //    "멋져요！40초 안을 목표로 하세요！");
                //}
                //else if (ct > 30)
                //{ // 30〜40초.
                //    GUI.Label(new Rect(pos_x, pos_y, 200, 20),
                //    "대단해요！30초 이내를 목표로 하세요!");
                //}
                //else
                //{ // 30초이내！
                //    GUI.Label(new Rect(pos_x, pos_y, 200, 20),
                //    "빨라요！플라플라 마스터-！");
                //}
                GUI.Label(new Rect(pos_x, pos_y, 300, 20),
                "빨리 이 지긋지긋한 행성에서 탈출하자!");
                GUI.Label(new Rect(pos_x, pos_y + 100, 300, 20),
                "다음 스테이지 이동 : Space", guistyle);

                break;
            case STEP.GAMEOVER:
                GUI.color = Color.black;
                // 게임 오버 메시지를 표시.
                GUI.Label(new Rect(pos_x, pos_y, 200, 20),
                "게임 오버", guistyle);
                GUI.Label(new Rect(pos_x, pos_y + 100, 200, 20),
                "다시하기 : Space", guistyle);
                GUI.Label(new Rect(pos_x, pos_y + 180, 200, 20),
                "타이들로 돌아가기 : ESC", guistyle);
                break;
        }
    }


}
