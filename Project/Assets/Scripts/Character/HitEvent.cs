using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEvent : MonoBehaviour
{
    GameObject hitEffect, particleInst;
    WeaponControl parentWeapon;

    AudioSource hitSound;

    private ItemRoot itemRoot = null;
    private GameStatus gameStatus = null;
    // Start is called before the first frame update
    void Awake()
    {
        hitEffect = Resources.Load<GameObject>("Prefabs/HitEffect");
        parentWeapon = this.gameObject.transform.parent.GetComponent<WeaponControl>();

        hitSound = gameObject.AddComponent<AudioSource>();

        this.gameStatus = GameObject.Find("GameRoot").GetComponent<GameStatus>();
        this.itemRoot = GameObject.Find("GameRoot").GetComponent<ItemRoot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AudioSource GetAudioSource()
    {
        Debug.Log(this.gameObject.transform.parent.parent.gameObject.name + "�� ����");
        return hitSound;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (parentWeapon.GetAttack())    // ���� ���� ��
        {
            Debug.Log(this.gameObject.transform.parent.parent.gameObject.name + "�� ������" + other.gameObject.name + " �� �¾Ҵ�.");
            switch (other.gameObject.tag)
            {
                case "Enemy":   // ���� ����� ��
                    if (this.gameObject.transform.parent.parent.gameObject.tag != "Enemy")
                    {
                        particleInst = Instantiate(hitEffect, other.gameObject.transform.position, other.gameObject.transform.rotation);
                        SoundControl.SetSound(hitSound, "MP_Realistic Punch");

                        //Debug.Log("�� ��ƼŬ");
                        other.gameObject.GetComponent<EnemyControl>().hp -= 0.5f;
                        //Destroy(other.gameObject);
                    }
                    break;
                case "Player":  // ���� ����� �÷��̾�(���� ���ɼ��� ���� �߰���)
                    particleInst = Instantiate(hitEffect, other.gameObject.transform.position, other.gameObject.transform.rotation);
                    //Debug.Log("�÷��̾� ��ƼŬ");
                    break;
                case "Rocket":  // ���� ����� ����
                    //particleInst = Instantiate(hitEffect, this.gameObject.transform.position, this.gameObject.transform.rotation);  // ������ ũ�Ⱑ Ŀ�� ��ƼŬ�� �������Ƿ� ������ pivot���� ��µǰ� ����
                    //SoundControl.SetSound(hitSound, "MP_Wood Whack");

                    if (this.gameObject.transform.parent.parent.gameObject.tag == "Enemy")   //������ ���ּ��� ������ ��
                    {
                        particleInst = Instantiate(hitEffect, this.gameObject.transform.position, this.gameObject.transform.rotation);  // ������ ũ�Ⱑ Ŀ�� ��ƼŬ�� �������Ƿ� ������ pivot���� ��µǰ� ����
                        SoundControl.SetSound(hitSound, "MP_Wood Whack");

                        this.gameStatus.addRepairment(this.itemRoot.GetGainDamage(this.gameObject.transform.parent.parent.gameObject));
                    }
                    //Debug.Log("���� ��ƼŬ");
                    break;
                default:        // �� ��
                    break;
            }
        }

        Destroy(particleInst, 1.0f);    // ������� ����Ʈ�� 1�� �� �Ҹ��Ŵ
    }
}
