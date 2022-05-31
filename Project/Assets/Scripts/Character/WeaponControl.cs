using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    Animator animator;
    

    GameObject weaponTrail;
    TrailRenderer weaponTrailRender;

    float time;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        time = 0;
        weaponTrail = this.gameObject.transform.GetChild(1).transform.GetChild(0).gameObject;

        weaponTrailRender = weaponTrail.GetComponent<TrailRenderer>();
    }

    public bool GetAttack()
    {
        return animator.GetBool("isAttack");
    }

    // Update is called once per frame
    void Update()
    { 
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            animator.SetBool("isAttack", false);
        }

        weaponTrailRender.enabled = animator.GetBool("isAttack");
    }

    public void SetAttack()
    {
        animator.SetBool("isAttack", true);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(animator.GetBool("isAttack"))    // 공격 중일 때
    //    {
    //        Debug.Log("공격이" + other.gameObject.name + " 에 맞았다.");
    //        switch (other.gameObject.tag)
    //        {
    //            case "Enemy":   // 맞은 대상이 적
    //                particleInst = Instantiate(hitEffect, other.gameObject.transform);
    //                Debug.Log("적 파티클");

    //                break;
    //            case "Player":  // 맞은 대상이 플레이어(구현 가능성을 위해 추가함)
    //                particleInst = Instantiate(hitEffect, other.gameObject.transform);
    //                Debug.Log("플레이어 파티클");
    //                break;
    //            case "Rocket":  // 맞은 대상이 로켓
    //                Debug.Log("로켓 파티클");
    //                break;
    //            default:        // 예외
    //                particleInst = Instantiate(hitEffect, other.gameObject.transform);
    //                Debug.Log("그 외 파티클");
    //                break;
    //        }
    //    }

    //    Destroy(particleInst, 1.0f);    // 만들어진 이펙트를 1초 후 소멸시킴
    //}
}
