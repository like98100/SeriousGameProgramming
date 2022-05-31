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
    //    if(animator.GetBool("isAttack"))    // ���� ���� ��
    //    {
    //        Debug.Log("������" + other.gameObject.name + " �� �¾Ҵ�.");
    //        switch (other.gameObject.tag)
    //        {
    //            case "Enemy":   // ���� ����� ��
    //                particleInst = Instantiate(hitEffect, other.gameObject.transform);
    //                Debug.Log("�� ��ƼŬ");

    //                break;
    //            case "Player":  // ���� ����� �÷��̾�(���� ���ɼ��� ���� �߰���)
    //                particleInst = Instantiate(hitEffect, other.gameObject.transform);
    //                Debug.Log("�÷��̾� ��ƼŬ");
    //                break;
    //            case "Rocket":  // ���� ����� ����
    //                Debug.Log("���� ��ƼŬ");
    //                break;
    //            default:        // ����
    //                particleInst = Instantiate(hitEffect, other.gameObject.transform);
    //                Debug.Log("�� �� ��ƼŬ");
    //                break;
    //        }
    //    }

    //    Destroy(particleInst, 1.0f);    // ������� ����Ʈ�� 1�� �� �Ҹ��Ŵ
    //}
}
