using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    public static float ENEMY_MOVED_SPEED = 2.0f;
    public static float SLOW_MOVESPEED = 0.3f; // �̵� �ӵ� ���� �ۼ�������

    float[] Maxhp;
    public float hp;
    public GameObject hpbar;
    public enum STEP
    { 
        NONE = -1,  // ���� ����
        MOVE = 0,   // �̵� ��
        ATTACK,     // ���� ��
        NUM,        // ���� ����(3)
    };

    public STEP step = STEP.NONE;
    public STEP next_step = STEP.MOVE;
    public float step_timer = 0f;


    Transform target;
    Vector3 targetVector;
    // Start is called before the first frame update
    void Start()
    {
        this.step = STEP.NONE;
        this.next_step = STEP.MOVE;

        target = GameObject.Find("rocket").transform;
        this.Maxhp = new float[3] { 0.5f, 1.0f, 1.5f };
        this.hp = Maxhp[GameStatus.stage];
        hpbar = GameObject.Find("Canvas/" + this.gameObject.name + "HP");
    }

    private void moveControl()
    {
        Vector3 moveVector = Vector3.zero;          // �̵��� ����
        Vector3 position = this.transform.position; // ���� ��ġ ����
        
        //transform.LookAt(target);
        targetVector = new Vector3(target.position.x - transform.position.x, 0f, target.position.z - transform.position.z);    // Ÿ�� ���⺤��
        targetVector.Normalize();       // ����ȭ
        Quaternion quater = Quaternion.LookRotation(targetVector);  // �Ĵٺ� ���� ����
        //transform.rotation = quater;
        transform.rotation = Quaternion.Lerp(transform.rotation, quater, 0.2f); // 0.2f��ŭ �ӵ��� ȸ��

        moveVector += targetVector;

        moveVector.Normalize(); // ���̸� 1��

        //if (this.gameObject.GetComponent<DeburfControl>().GetDeburfActivate()) moveVector *= SLOW_MOVESPEED;

        moveVector *= ENEMY_MOVED_SPEED * Time.deltaTime;
        position += moveVector;

        transform.position = position;
        
    }

    private void attackControl()
    {
        do
        {
            if (!this.gameObject.transform.GetChild(1).gameObject.GetComponent<WeaponControl>().GetAttack())
                SoundControl.SetSound(this.gameObject.transform.GetChild(1).GetChild(0)
                .gameObject.GetComponent<HitEvent>().GetAudioSource(), "MP_swosh-sword-swing");

            // ���� �ִϸ��̼� ����
            //GameObject.Find("Weapon").GetComponent<WeaponControl>().SetAttack();
            this.gameObject.transform.GetChild(1).gameObject.GetComponent<WeaponControl>().SetAttack();
        } while (false);
    }

    // Update is called once per frame
    void Update()
    {
        this.step_timer += Time.deltaTime;

        //float attack_time = 0.2f;

        //new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z)
        //Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), targetVector * 10, Color.blue);
        //Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z),
        //    targetVector * 4f, Color.red);

        float distance = (this.gameObject.transform.position - target.transform.position).magnitude;

        // ���¸� ��ȭ��Ų��.
        if (this.next_step == STEP.NONE)
        {   // ���� ������ ������
            switch(this.step)
            {
                case STEP.MOVE: // �̵� ��
                    if (distance <= 3.6f) this.next_step = STEP.ATTACK;
                    break;
                case STEP.ATTACK:   // ���� ��
                    //if (this.step_timer > attack_time)
                    //    this.next_step = STEP.MOVE;
                    if (distance > 3.6f) this.next_step = STEP.MOVE;
                    break;
            }
        }

        // ���°� ��ȭ���� ��
        while(this.next_step != STEP.NONE)
        {   // ���°� NONE�� �ƴ� ��(��ȭ���� ��)
            this.step = next_step;
            this.next_step = STEP.NONE;
            switch(this.step)
            {
                case STEP.MOVE:
                    
                    break;
                case STEP.ATTACK:
                    break;
            }
            this.step_timer = 0f;
        }

        // �� ��Ȳ���� �ݺ�
        switch(this.step)
        {
            case STEP.MOVE:
                this.moveControl();
                break;
            case STEP.ATTACK:
                this.attackControl();
                break;
        }

        // Hp Bar Tracking
        hpbar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.5f, 0));
        hpbar.transform.GetChild(1).gameObject.GetComponent<Image>().fillAmount = this.hp / this.Maxhp[GameStatus.stage];

        if (this.hp <= 0f)
        {
            //GameObject.Find("GameRoot").GetComponent<ItemRoot>().SetEnemyNum(false);
            Destroy(hpbar);
            Destroy(this.gameObject);
        }

    }
}
