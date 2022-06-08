using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TornadoContorl : MonoBehaviour
{
    private GameStatus game_status = null;


    public static float TORNADO_MOVED_SPEED = 2.75f;
    public static float TORNADO_EXISTED_TIME = 17.5f;
    GameObject target;
    Vector3 targetVector;
    GameStatus gameStatus;
    bool isBorder;

    float dotTime, existTime;

    AudioSource hitSound;
    GameObject hitEffect, particleInst, player;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        gameStatus = GameObject.Find("GameRoot").GetComponent<GameStatus>();
        isBorder = false;

        dotTime = 0f;
        existTime = 0f;
        hitEffect = Resources.Load<GameObject>("Prefabs/HitEffect");
        hitSound = gameObject.GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");
        this.game_status = GameObject.Find("GameRoot").GetComponent<GameStatus>();
    }

    void moveToTarget()
    {
        if (this.game_status.isGameClear() || this.game_status.isGameOver()) return;

        Vector3 moveVector = Vector3.zero;
        Vector3 position = this.transform.position;

        targetVector = new Vector3(target.transform.position.x - transform.position.x, 0f, target.transform.position.z - transform.position.z);
        targetVector.Normalize();
        Quaternion quater = Quaternion.LookRotation(targetVector);
        //transform.rotation = quater;
        transform.rotation = Quaternion.Lerp(transform.rotation, quater, 0.2f);

        moveVector += targetVector;

        moveVector.Normalize();

        moveVector *= TORNADO_MOVED_SPEED * Time.deltaTime;
        if (isBorder) moveVector *= 0.7f;
        position += moveVector;
        transform.position = new Vector3(position.x, 1f, position.z);
    }

    // Update is called once per frame
    void Update()
    {
        existTime += Time.deltaTime;
        float dotCooldown = 0.3f;
        moveToTarget();
        if(isBorder)
        {
            if (this.game_status.isGameClear() || this.game_status.isGameOver()) { }
            else
            {
                dotTime += Time.deltaTime;
                if (dotTime >= dotCooldown)
                {
                    particleInst = Instantiate(hitEffect, player.transform.position, player.transform.rotation);
                    SoundControl.SetSound(hitSound, "MP_Realistic Punch");
                    this.gameStatus.addSatiety(-0.02f);
                    dotTime = 0f;
                }
            }
           
        }

        if (existTime >= TORNADO_EXISTED_TIME) Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player") this.isBorder = true;
        else if (other.tag == "Tornado")
        {
            this.transform.position = new Vector3(this.transform.position.x + Random.Range(-3.0f, 3.0f), this.transform.position.y, this.transform.position.z + Random.Range(-3.0f, 3.0f));
        }
            

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") this.isBorder = false;
    }
}
