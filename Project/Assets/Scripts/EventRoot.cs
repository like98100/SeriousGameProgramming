using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ó���� �̺�Ʈ�� ������ ��Ÿ���� class.
public class Event
{ // �̺�Ʈ ����.
    public enum TYPE
    {
        NONE = -1, // ����.
        ROCKET = 0, // ���ּ� ����.
        NUM, // �̺�Ʈ�� �� ���� �ִ��� ��Ÿ����(=1).
    };
};

public class EventRoot : MonoBehaviour
{
    public Event.TYPE getEventType(GameObject event_go)
    {
        Event.TYPE type = Event.TYPE.NONE;
        if (event_go != null)
        { // �μ��� GameObject�� ������� ������.
            if (event_go.tag == "Rocket")
            {
                type = Event.TYPE.ROCKET;
            }
        }
        return (type);
    }
    // ö�����̳� �Ĺ��� �� ���¿��� ���ּ��� �����ߴ��� Ȯ��
    public bool isEventIgnitable(Item.TYPE carried_item, GameObject event_go)
    {
        bool ret = false;
        Event.TYPE type = Event.TYPE.NONE;
        if (event_go != null)
        {
            type = this.getEventType(event_go); // �̺�Ʈ Ÿ���� ���Ѵ�.
        }
        switch (type)
        {
            case Event.TYPE.ROCKET:
                if (carried_item == Item.TYPE.IRON)
                { // ������ �ִ� ���� ö�����̶��.
                    ret = true; // '�̺�Ʈ�� �� �־�䣡'��� �����Ѵ�.
                }
                if (carried_item == Item.TYPE.PLANT)
                { // ������ �ִ� ���� �Ĺ��̶��.
                    ret = true; // '�̺�Ʈ�� �� �־�䣡'��� �����Ѵ�.
                }
                break;
        }
        return (ret);
    }
    // ������ ���� ������Ʈ�� �̺�Ʈ Ÿ�� ��ȯ
    public string getIgnitableMessage(GameObject event_go)
    {
        string message = "";
        Event.TYPE type = Event.TYPE.NONE;
        if (event_go != null)
        {
            type = this.getEventType(event_go);
        }
        switch (type)
        {
            case Event.TYPE.ROCKET:
                message = "�����Ѵ�";
                break;
        }
        return (message);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
