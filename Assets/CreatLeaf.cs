using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatLeaf : MonoBehaviour {

    public GameObject prefab;
    public GameObject lateral2;
    public GameObject leaf;
    public GameObject anchor; 

    public Vector3[] lateralCentreBox;
    public Vector3 dirction;
    public float[] lateralRadiusBox;
    public Vector3 anchorPos;

    // Use this for initialization
    void Start () {
        lateral2 = GameObject.Find("lateral2");

        leaf = GameObject.Instantiate(prefab, new Vector3(-1000f,0,0)  , Quaternion.identity) as GameObject;
        GameObject anchor = GameObject.Find("anchor-leaf");
        anchorPos = anchor.GetComponent<Transform>().position; 
    }
	
	// Update is called once per frame
	void Update () { 

        lateralCentreBox = lateral2.GetComponent<Lateral2>().currentCentreBox;
        lateralRadiusBox = lateral2.GetComponent<Lateral2>().currentRadiusBox;
        dirction = lateral2.GetComponent<Lateral2>().axis2;



        //for (int i = 0; i < 3; i++)
        //{
        //    //飞盘生成的位置(随机).
        //    Vector3 pos = new Vector3(Random.Range(-4.0f, 4.0f), Random.Range(0.5f, 1.5f), Random.Range(4.0f, 10.0f));
        //    //实例化物体(飞盘).
        //    GameObject go = GameObject.Instantiate(prefab, pos, Quaternion.identity) as GameObject;
        //    //给生成的飞盘设置一个父物体.
        //    go.GetComponent<Transform>().SetParent(m_Transform);
        //}
    }
}
