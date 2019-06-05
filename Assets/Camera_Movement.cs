 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour {
    public float near = 20.0f;
    public float far = 200.0f;
 
    public float sensitivityX = 10f;
    public float sensitivityY = 10f;
    public float sensitivetyZ = 2f;
    public float sensitivetyMove = 2f;
    public float sensitivetyMouseWheel = 2f;

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.GetComponent<Transform>().Translate(Vector3.forward * 0.2f, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.GetComponent<Transform>().Translate(Vector3.back * 0.2f, Space.World);
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.GetComponent<Transform>().Translate(Vector3.left * 0.2f, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.GetComponent<Transform>().Translate(Vector3.right * 0.2f, Space.World);
        }
    }

}
