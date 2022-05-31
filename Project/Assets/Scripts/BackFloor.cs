using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackFloor : MonoBehaviour
{
    [SerializeField]
    Material[] mesh;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = mesh[GameStatus.stage];
    }

    //private void Update()
    //{
    //    this.gameObject.GetComponent<MeshRenderer>().material = mesh[GameStatus.stage];
    //}
}
