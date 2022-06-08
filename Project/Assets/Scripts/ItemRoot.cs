using System.Collections;
using System.Collections.Generic; // List를 사용하기 위해서.
using UnityEngine;

public class Item
{
    public enum TYPE
    { // 아이템 종류.
        NONE = -1, IRON = 0, APPLE, PLANT, ENEMY, TORNADO, // 없음, 철광석, 사과, 식물, 적, 토네이도.
        NUM,
    }; // 아이템이 몇 종류인가 나타낸다(=4).
};

public class ItemRoot : MonoBehaviour
{
    public GameObject ironPrefab = null; // Prefab 'Iron'
    public GameObject plantPrefab = null; // Prefab 'Plant'
    public GameObject applePrefab = null; // Prefab 'Apple'

    public GameObject enemyPrefab = null; // Prefab 'Enemy'
    public GameObject enemyHpBar = null;    // Prefab 'EnemyHP'
    public GameObject tornadoPrefab = null; // Prefab 'Tornado'

    Vector2 hpbarCreatePoint;
    int enemyNum;

    protected List<Vector3> respawn_points; // 출현 지점 List.

    protected List<Vector3> respawnIronPoints;
    protected List<Vector3> respawnEnemyPoints; // 적 출현 지점 List.
    protected List<Vector3> respawnTornadoPoints; // 토네이도 출현 지점 List.

    public float step_timer = 0.0f;
    public static float[] RESPAWN_TIME_APPLE = new float[3] { 15.0f, 17.0f, 20.0f }; // 사과 출현 시간 상수.
    public static float[] RESPAWN_TIME_IRON = new float[3] { 10.0f, 10.0f, 12.5f }; // 철광석 출현 시간 상수.
    public static float[] RESPAWN_TIME_PLANT = new float[3] { 1.2f, 2.5f, 3.0f }; // 식물 출현 시간 상수.

    public static float[] RESPAWN_TIME_ENEMY = new float[3] { 4.0f, 5.0f, 6.5f }; // 적 출현 시간 상수.

    public static float[] RESPAWN_TIME_TORNADO = new float[3] { 300.0f, 7.5f, 10.0f};  //토네이도 출현 시간 상수

    private float respawn_timer_apple = 0.0f; // 사과의 출현 시간.
    private float respawn_timer_iron = 0.0f; // 철광석의 출현 시간. 
    private float respawn_timer_plant = 0.0f; // 식물의 출현 시간.

    private float respawn_timer_enemy = 0.0f; // 적의 출현 시간.
    private float respawn_timer_tornado = 0.0f; // 토네이도 출현 시간.

    private GameStatus game_status = null;

    // 아이템의 종류를 Item.TYPE형으로 반환하는 메소드.
    public Item.TYPE getItemType(GameObject item_go)
    {
        Item.TYPE type = Item.TYPE.NONE;
        if (item_go != null)
        { // 인수로 받은 GameObject가 비어있지 않으면.
            switch (item_go.tag)
            { // 태그로 분기.
                case "Iron": type = Item.TYPE.IRON; break;
                case "Apple": type = Item.TYPE.APPLE; break;
                case "Plant": type = Item.TYPE.PLANT; break;
                case "Enemy": type = Item.TYPE.ENEMY; break;
                case "Tornado": type = Item.TYPE.TORNADO; break;
            }
        }
        return (type);
    }

    // 철광석을 출현시킨다.
    public void respawnIron()
    {
        if(this.respawnIronPoints.Count > 0)
        {
            // 철광석 프리팹을 인스턴스화.
            GameObject go = GameObject.Instantiate(this.ironPrefab) as GameObject;
            // 철광석의 출현 포인트를 랜덤으로 취득.
            int n = Random.Range(0, this.respawnIronPoints.Count);
            Vector3 pos = this.respawnIronPoints[n];
            // 출현 위치를 조정.
            pos.y = 1.0f;
            pos.x += Random.Range(-1.0f, 1.0f);
            pos.z += Random.Range(-1.0f, 1.0f);
            // 철광석의 위치를 이동.
            go.transform.position = pos;
        }
        
    }
    // 사과를 출현시킨다.
    public void respawnApple()
    {
        // 사과 프리팹을 인스턴스화.
        GameObject go = GameObject.Instantiate(this.applePrefab) as GameObject;
        // 사과의 출현 포인트를 취득.
        Vector3 pos = GameObject.Find("AppleRespawn").transform.position;
        // 출현 위치를 조정.
        pos.y = 1.0f;
        pos.x += Random.Range(-1.0f, 1.0f);
        pos.z += Random.Range(-1.0f, 1.0f);
        // 사과의 위치를 이동.
        go.transform.position = pos;
    }

    // 식물을 출현시킨다.
    public void respawnPlant()
    {
        if (this.respawn_points.Count > 0)
        { // List가 비어있지 않으면.
          // 식물 프리팹을 인스턴스화.
            GameObject go = GameObject.Instantiate(this.plantPrefab) as GameObject;
            // 식물의 출현 포인트를 랜덤하게 취득.
            int n = Random.Range(0, this.respawn_points.Count);
            Vector3 pos = this.respawn_points[n];
            // 출현 위치를 조정.
            pos.y = 1.0f;
            pos.x += Random.Range(-5.0f, 5.0f);
            pos.z += Random.Range(-5.0f, 5.0f);
            // 식물의 위치를 이동.
            go.transform.position = pos;
        }
    }

    // 적 오브젝트를 출현시킨다.
    public void respawnEnemy()
    {
        if(this.respawnEnemyPoints.Count > 0)
        {
            GameObject enemy = GameObject.Instantiate(this.enemyPrefab) as GameObject;
            GameObject enemyHp = GameObject.Instantiate(this.enemyHpBar, hpbarCreatePoint,
                Quaternion.identity, GameObject.Find("Canvas").transform) as GameObject;

            enemy.name = "Enemy" + enemyNum;
            enemyHp.name = enemy.name + "HP";

            enemyHp.transform.SetAsFirstSibling();  // 하이어라키 내 가장 위로 올려서 미니맵에 가려지도록 구현

            int spawnNum = Random.Range(0, this.respawnEnemyPoints.Count);
            Vector3 pos = this.respawnEnemyPoints[spawnNum];

            pos.y = 1.5f;
            pos.x += Random.Range(-1.0f, 1.0f);
            pos.z += Random.Range(-1.0f, 1.0f);

            enemy.transform.position = pos;
            enemyNum++;
        }
    }

    public void respawnTornado()
    {
        if(this.respawnTornadoPoints.Count > 0)
        {
            GameObject tornado = GameObject.Instantiate(this.tornadoPrefab) as GameObject;

            int spawnNum = Random.Range(0, this.respawnTornadoPoints.Count);
            Vector3 pos = this.respawnTornadoPoints[spawnNum];

            pos.y = 1.0f;
            pos.x += Random.Range(-5.0f, 5.0f);
            pos.z += Random.Range(-5.0f, 5.0f);

            tornado.transform.position = pos;
;        }
    }

    // 들고 있는 아이템에 따른 ‘수리 진척 상태’를 반환
    public float getGainRepairment(GameObject item_go)
    {
        float gain = 0.0f;
        if (item_go == null)
        {
            gain = 0.0f;
        }
        else
        {
            Item.TYPE type = this.getItemType(item_go);
            switch (type)
            { // 들고 있는 아이템의 종류로 갈라진다.
                case Item.TYPE.IRON:
                    gain = GameStatus.GAIN_REPAIRMENT_IRON[GameStatus.stage]; break;
                case Item.TYPE.PLANT:
                    gain = GameStatus.GAIN_REPAIRMENT_PLANT[GameStatus.stage]; break;
            }
        }
        return (gain);
    }

    public float GetGainDamage(GameObject enemy)
    {
        float damage = 0f;
        if (enemy == null) damage = 0f;
        else
        {
            Item.TYPE type = this.getItemType(enemy);
            switch(type)
            {
                case Item.TYPE.ENEMY:
                    damage = GameStatus.DAMAGEENEMY[GameStatus.stage] * -1f;
                    break;
            }
        }

        return (damage);
    }

    // 들고 있는 아이템에 따른 ‘체력 감소 상태’를 반환
    public float getConsumeSatiety(GameObject item_go)
    {
        float consume = 0.0f;
        if (item_go == null)
        {
            consume = 0.0f;
        }
        else
        {
            Item.TYPE type = this.getItemType(item_go);
            switch (type)
            { // 들고 있는 아이템의 종류로 갈라진다.
                case Item.TYPE.IRON:
                    consume = GameStatus.CONSUME_SATIETY_IRON[GameStatus.stage]; break;
                case Item.TYPE.APPLE:
                    consume = GameStatus.CONSUME_SATIETY_APPLE[GameStatus.stage]; break;
                case Item.TYPE.PLANT:
                    consume = GameStatus.CONSUME_SATIETY_PLANT[GameStatus.stage]; break;
            }
        }
        return (consume);
    }

    // 들고 있는 아이템에 따른 ‘체력 회복 상태’를 반환
    public float getRegainSatiety(GameObject item_go)
    {
        float regain = 0.0f;
        if (item_go == null)
        {
            regain = 0.0f;
        }
        else
        {
            Item.TYPE type = this.getItemType(item_go);
            switch (type)
            { // 들고 있는 아이템의 종류로 갈라진다.
                case Item.TYPE.APPLE:
                    regain = GameStatus.REGAIN_SATIETY_APPLE[GameStatus.stage]; break;
                case Item.TYPE.PLANT:
                    regain = GameStatus.REGAIN_SATIETY_PLANT[GameStatus.stage]; break;
            }
        }
        return (regain);
    }

    public int GetEnemyNum()
    {
        return enemyNum;
    }

    public void SetEnemyNum(bool isAdd)
    {
        if (isAdd) enemyNum++;
        else enemyNum--;
    }
    // Start is called before the first frame update
    void Start()
    {
        // 메모리 영역 확보.
        this.respawn_points = new List<Vector3>();
        this.respawnEnemyPoints = new List<Vector3>();
        this.respawnTornadoPoints = new List<Vector3>();
        this.respawnIronPoints = new List<Vector3>();

        this.game_status = this.gameObject.GetComponent<GameStatus>();

        hpbarCreatePoint = new Vector2(0, 0);
        enemyNum = 0;

        // "PlantRespawn" 태그가 붙은 모든 오브젝트를 배열에 저장.
        GameObject[] respawns = GameObject.FindGameObjectsWithTag("PlantRespawn");
        // 배열 respawns 내의 개개의 GameObject를 순서대로 처리한다.
        foreach (GameObject go in respawns)
        {
            // 렌더러 획득.
            MeshRenderer renderer = go.GetComponentInChildren<MeshRenderer>();
            if (renderer != null)
            { // 렌더러가 존재하면.
                renderer.enabled = false; // 그 렌더러를 보이지 않게.
            }
            // 출현 포인트 List에 위치 정보를 추가.
            this.respawn_points.Add(go.transform.position);
        }
        // 사과의 출현 포인트를 취득하고, 렌더러를 보이지 않게.
        GameObject applerespawn = GameObject.Find("AppleRespawn");
        applerespawn.GetComponent<MeshRenderer>().enabled = false;
        // 철광석의 출현 포인트를 취득하고, 렌더러를 보이지 않게.
        GameObject[] ironrespawns = GameObject.FindGameObjectsWithTag("IronRespawn");

        foreach(GameObject iron in ironrespawns)
        {
            MeshRenderer renderer = iron.GetComponentInChildren<MeshRenderer>();
            if (renderer != null)    // 렌더러가 존재하면 보이지 않게
            {
                renderer.enabled = false;
            }

            this.respawnIronPoints.Add(iron.transform.position);
        }

        // EnemyRespawn 태그가 붙은 오브젝트들을 배열에 저장
        GameObject[] enemyRespawns = GameObject.FindGameObjectsWithTag("EnemyRespawn");
        // 배열 내 GameObejct를 순서대로 처리함.
        foreach(GameObject enemy in enemyRespawns)
        {
            MeshRenderer renderer = enemy.GetComponentInChildren<MeshRenderer>();
            if(renderer != null)    // 렌더러가 존재하면 보이지 않게
            {
                renderer.enabled = false;
            }

            this.respawnEnemyPoints.Add(enemy.transform.position);
        }

        GameObject[] tornadoRespawns = GameObject.FindGameObjectsWithTag("TornadoRespawn");
        foreach(GameObject tornado in tornadoRespawns)
        {
            MeshRenderer renderer = tornado.GetComponentInChildren<MeshRenderer>();
            if (renderer != null)    // 렌더러가 존재하면 보이지 않게
            {
                renderer.enabled = false;
            }

            this.respawnTornadoPoints.Add(tornado.transform.position);
        }

        this.respawnIron(); // 철광석을 하나 생성.
        this.respawnPlant(); // 식물을 하나 생성.
        // 처음에 식물이 세 개 배치되게 수정.
        this.respawnPlant();
        this.respawnPlant();

        
    }

    // 각 아이템의 타이머 값이 출현 시간을 초과하면 해당 아이템을 출현.
    void Update()
    {
        respawn_timer_apple += Time.deltaTime;
        respawn_timer_iron += Time.deltaTime;
        respawn_timer_plant += Time.deltaTime;
        respawn_timer_enemy += Time.deltaTime;
        respawn_timer_tornado += Time.deltaTime;

        if (respawn_timer_apple > RESPAWN_TIME_APPLE[GameStatus.stage])
        {
            respawn_timer_apple = 0.0f;
            this.respawnApple(); // 사과를 출현시킨다.
        }
        if (respawn_timer_iron > RESPAWN_TIME_IRON[GameStatus.stage])
        {
            respawn_timer_iron = 0.0f;
            this.respawnIron(); // 철광석을 출현시킨다.
        }
        if (respawn_timer_plant > RESPAWN_TIME_PLANT[GameStatus.stage])
        {
            respawn_timer_plant = 0.0f;
            this.respawnPlant(); // 식물을 출현시킨다.
        }
        if(respawn_timer_enemy > RESPAWN_TIME_ENEMY[GameStatus.stage])
        {
            if(!this.game_status.isGameClear() && !this.game_status.isGameOver())
            {
                respawn_timer_enemy = 0f;
                this.respawnEnemy();    // 적을 출현시킨다.
            }

        }
        if(respawn_timer_tornado > RESPAWN_TIME_TORNADO[GameStatus.stage])
        {
            respawn_timer_tornado = 0f;
            this.respawnTornado();
        }
    }

}
