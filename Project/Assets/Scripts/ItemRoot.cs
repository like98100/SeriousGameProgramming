using System.Collections;
using System.Collections.Generic; // List�� ����ϱ� ���ؼ�.
using UnityEngine;

public class Item
{
    public enum TYPE
    { // ������ ����.
        NONE = -1, IRON = 0, APPLE, PLANT, ENEMY, TORNADO, // ����, ö����, ���, �Ĺ�, ��, ����̵�.
        NUM,
    }; // �������� �� �����ΰ� ��Ÿ����(=4).
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

    protected List<Vector3> respawn_points; // ���� ���� List.

    protected List<Vector3> respawnIronPoints;
    protected List<Vector3> respawnEnemyPoints; // �� ���� ���� List.
    protected List<Vector3> respawnTornadoPoints; // ����̵� ���� ���� List.

    public float step_timer = 0.0f;
    public static float[] RESPAWN_TIME_APPLE = new float[3] { 15.0f, 17.0f, 20.0f }; // ��� ���� �ð� ���.
    public static float[] RESPAWN_TIME_IRON = new float[3] { 10.0f, 10.0f, 12.5f }; // ö���� ���� �ð� ���.
    public static float[] RESPAWN_TIME_PLANT = new float[3] { 1.2f, 2.5f, 3.0f }; // �Ĺ� ���� �ð� ���.

    public static float[] RESPAWN_TIME_ENEMY = new float[3] { 4.0f, 5.0f, 6.5f }; // �� ���� �ð� ���.

    public static float[] RESPAWN_TIME_TORNADO = new float[3] { 300.0f, 7.5f, 10.0f};  //����̵� ���� �ð� ���

    private float respawn_timer_apple = 0.0f; // ����� ���� �ð�.
    private float respawn_timer_iron = 0.0f; // ö������ ���� �ð�. 
    private float respawn_timer_plant = 0.0f; // �Ĺ��� ���� �ð�.

    private float respawn_timer_enemy = 0.0f; // ���� ���� �ð�.
    private float respawn_timer_tornado = 0.0f; // ����̵� ���� �ð�.

    private GameStatus game_status = null;

    // �������� ������ Item.TYPE������ ��ȯ�ϴ� �޼ҵ�.
    public Item.TYPE getItemType(GameObject item_go)
    {
        Item.TYPE type = Item.TYPE.NONE;
        if (item_go != null)
        { // �μ��� ���� GameObject�� ������� ������.
            switch (item_go.tag)
            { // �±׷� �б�.
                case "Iron": type = Item.TYPE.IRON; break;
                case "Apple": type = Item.TYPE.APPLE; break;
                case "Plant": type = Item.TYPE.PLANT; break;
                case "Enemy": type = Item.TYPE.ENEMY; break;
                case "Tornado": type = Item.TYPE.TORNADO; break;
            }
        }
        return (type);
    }

    // ö������ ������Ų��.
    public void respawnIron()
    {
        if(this.respawnIronPoints.Count > 0)
        {
            // ö���� �������� �ν��Ͻ�ȭ.
            GameObject go = GameObject.Instantiate(this.ironPrefab) as GameObject;
            // ö������ ���� ����Ʈ�� �������� ���.
            int n = Random.Range(0, this.respawnIronPoints.Count);
            Vector3 pos = this.respawnIronPoints[n];
            // ���� ��ġ�� ����.
            pos.y = 1.0f;
            pos.x += Random.Range(-1.0f, 1.0f);
            pos.z += Random.Range(-1.0f, 1.0f);
            // ö������ ��ġ�� �̵�.
            go.transform.position = pos;
        }
        
    }
    // ����� ������Ų��.
    public void respawnApple()
    {
        // ��� �������� �ν��Ͻ�ȭ.
        GameObject go = GameObject.Instantiate(this.applePrefab) as GameObject;
        // ����� ���� ����Ʈ�� ���.
        Vector3 pos = GameObject.Find("AppleRespawn").transform.position;
        // ���� ��ġ�� ����.
        pos.y = 1.0f;
        pos.x += Random.Range(-1.0f, 1.0f);
        pos.z += Random.Range(-1.0f, 1.0f);
        // ����� ��ġ�� �̵�.
        go.transform.position = pos;
    }

    // �Ĺ��� ������Ų��.
    public void respawnPlant()
    {
        if (this.respawn_points.Count > 0)
        { // List�� ������� ������.
          // �Ĺ� �������� �ν��Ͻ�ȭ.
            GameObject go = GameObject.Instantiate(this.plantPrefab) as GameObject;
            // �Ĺ��� ���� ����Ʈ�� �����ϰ� ���.
            int n = Random.Range(0, this.respawn_points.Count);
            Vector3 pos = this.respawn_points[n];
            // ���� ��ġ�� ����.
            pos.y = 1.0f;
            pos.x += Random.Range(-5.0f, 5.0f);
            pos.z += Random.Range(-5.0f, 5.0f);
            // �Ĺ��� ��ġ�� �̵�.
            go.transform.position = pos;
        }
    }

    // �� ������Ʈ�� ������Ų��.
    public void respawnEnemy()
    {
        if(this.respawnEnemyPoints.Count > 0)
        {
            GameObject enemy = GameObject.Instantiate(this.enemyPrefab) as GameObject;
            GameObject enemyHp = GameObject.Instantiate(this.enemyHpBar, hpbarCreatePoint,
                Quaternion.identity, GameObject.Find("Canvas").transform) as GameObject;

            enemy.name = "Enemy" + enemyNum;
            enemyHp.name = enemy.name + "HP";

            enemyHp.transform.SetAsFirstSibling();  // ���̾��Ű �� ���� ���� �÷��� �̴ϸʿ� ���������� ����

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

    // ��� �ִ� �����ۿ� ���� ������ ��ô ���¡��� ��ȯ
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
            { // ��� �ִ� �������� ������ ��������.
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

    // ��� �ִ� �����ۿ� ���� ��ü�� ���� ���¡��� ��ȯ
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
            { // ��� �ִ� �������� ������ ��������.
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

    // ��� �ִ� �����ۿ� ���� ��ü�� ȸ�� ���¡��� ��ȯ
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
            { // ��� �ִ� �������� ������ ��������.
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
        // �޸� ���� Ȯ��.
        this.respawn_points = new List<Vector3>();
        this.respawnEnemyPoints = new List<Vector3>();
        this.respawnTornadoPoints = new List<Vector3>();
        this.respawnIronPoints = new List<Vector3>();

        this.game_status = this.gameObject.GetComponent<GameStatus>();

        hpbarCreatePoint = new Vector2(0, 0);
        enemyNum = 0;

        // "PlantRespawn" �±װ� ���� ��� ������Ʈ�� �迭�� ����.
        GameObject[] respawns = GameObject.FindGameObjectsWithTag("PlantRespawn");
        // �迭 respawns ���� ������ GameObject�� ������� ó���Ѵ�.
        foreach (GameObject go in respawns)
        {
            // ������ ȹ��.
            MeshRenderer renderer = go.GetComponentInChildren<MeshRenderer>();
            if (renderer != null)
            { // �������� �����ϸ�.
                renderer.enabled = false; // �� �������� ������ �ʰ�.
            }
            // ���� ����Ʈ List�� ��ġ ������ �߰�.
            this.respawn_points.Add(go.transform.position);
        }
        // ����� ���� ����Ʈ�� ����ϰ�, �������� ������ �ʰ�.
        GameObject applerespawn = GameObject.Find("AppleRespawn");
        applerespawn.GetComponent<MeshRenderer>().enabled = false;
        // ö������ ���� ����Ʈ�� ����ϰ�, �������� ������ �ʰ�.
        GameObject[] ironrespawns = GameObject.FindGameObjectsWithTag("IronRespawn");

        foreach(GameObject iron in ironrespawns)
        {
            MeshRenderer renderer = iron.GetComponentInChildren<MeshRenderer>();
            if (renderer != null)    // �������� �����ϸ� ������ �ʰ�
            {
                renderer.enabled = false;
            }

            this.respawnIronPoints.Add(iron.transform.position);
        }

        // EnemyRespawn �±װ� ���� ������Ʈ���� �迭�� ����
        GameObject[] enemyRespawns = GameObject.FindGameObjectsWithTag("EnemyRespawn");
        // �迭 �� GameObejct�� ������� ó����.
        foreach(GameObject enemy in enemyRespawns)
        {
            MeshRenderer renderer = enemy.GetComponentInChildren<MeshRenderer>();
            if(renderer != null)    // �������� �����ϸ� ������ �ʰ�
            {
                renderer.enabled = false;
            }

            this.respawnEnemyPoints.Add(enemy.transform.position);
        }

        GameObject[] tornadoRespawns = GameObject.FindGameObjectsWithTag("TornadoRespawn");
        foreach(GameObject tornado in tornadoRespawns)
        {
            MeshRenderer renderer = tornado.GetComponentInChildren<MeshRenderer>();
            if (renderer != null)    // �������� �����ϸ� ������ �ʰ�
            {
                renderer.enabled = false;
            }

            this.respawnTornadoPoints.Add(tornado.transform.position);
        }

        this.respawnIron(); // ö������ �ϳ� ����.
        this.respawnPlant(); // �Ĺ��� �ϳ� ����.
        // ó���� �Ĺ��� �� �� ��ġ�ǰ� ����.
        this.respawnPlant();
        this.respawnPlant();

        
    }

    // �� �������� Ÿ�̸� ���� ���� �ð��� �ʰ��ϸ� �ش� �������� ����.
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
            this.respawnApple(); // ����� ������Ų��.
        }
        if (respawn_timer_iron > RESPAWN_TIME_IRON[GameStatus.stage])
        {
            respawn_timer_iron = 0.0f;
            this.respawnIron(); // ö������ ������Ų��.
        }
        if (respawn_timer_plant > RESPAWN_TIME_PLANT[GameStatus.stage])
        {
            respawn_timer_plant = 0.0f;
            this.respawnPlant(); // �Ĺ��� ������Ų��.
        }
        if(respawn_timer_enemy > RESPAWN_TIME_ENEMY[GameStatus.stage])
        {
            if(!this.game_status.isGameClear() && !this.game_status.isGameOver())
            {
                respawn_timer_enemy = 0f;
                this.respawnEnemy();    // ���� ������Ų��.
            }

        }
        if(respawn_timer_tornado > RESPAWN_TIME_TORNADO[GameStatus.stage])
        {
            respawn_timer_tornado = 0f;
            this.respawnTornado();
        }
    }

}
