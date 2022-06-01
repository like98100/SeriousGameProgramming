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
        Debug.Log(this.gameObject.transform.parent.parent.gameObject.name + "의 공격");
        return hitSound;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (parentWeapon.GetAttack())    // 공격 중일 때
        {
            Debug.Log(this.gameObject.transform.parent.parent.gameObject.name + "의 공격이" + other.gameObject.name + " 에 맞았다.");
            switch (other.gameObject.tag)
            {
                case "Enemy":   // 맞은 대상이 적
                    if (this.gameObject.transform.parent.parent.gameObject.tag != "Enemy")
                    {
                        particleInst = Instantiate(hitEffect, other.gameObject.transform.position, other.gameObject.transform.rotation);
                        SoundControl.SetSound(hitSound, "MP_Realistic Punch");

                        //Debug.Log("적 파티클");
                        other.gameObject.GetComponent<EnemyControl>().hp -= 0.5f;
                        //Destroy(other.gameObject);
                    }
                    break;
                case "Player":  // 맞은 대상이 플레이어(구현 가능성을 위해 추가함)
                    particleInst = Instantiate(hitEffect, other.gameObject.transform.position, other.gameObject.transform.rotation);
                    //Debug.Log("플레이어 파티클");
                    break;
                case "Rocket":  // 맞은 대상이 로켓
                    //particleInst = Instantiate(hitEffect, this.gameObject.transform.position, this.gameObject.transform.rotation);  // 로켓은 크기가 커서 파티클이 가려지므로 무기의 pivot에서 출력되게 실행
                    //SoundControl.SetSound(hitSound, "MP_Wood Whack");

                    if (this.gameObject.transform.parent.parent.gameObject.tag == "Enemy")   //적군이 우주선을 때렸을 때
                    {
                        particleInst = Instantiate(hitEffect, this.gameObject.transform.position, this.gameObject.transform.rotation);  // 로켓은 크기가 커서 파티클이 가려지므로 무기의 pivot에서 출력되게 실행
                        SoundControl.SetSound(hitSound, "MP_Wood Whack");

                        this.gameStatus.addRepairment(this.itemRoot.GetGainDamage(this.gameObject.transform.parent.parent.gameObject));
                    }
                    //Debug.Log("로켓 파티클");
                    break;
                default:        // 그 외
                    break;
            }
        }

        Destroy(particleInst, 1.0f);    // 만들어진 이펙트를 1초 후 소멸시킴
    }
}
