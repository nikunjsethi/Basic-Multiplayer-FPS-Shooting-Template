using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookY : MonoBehaviour
{
    [SerializeField]
    private float _sensitivity = 2.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float _mouseY = Input.GetAxis("Mouse Y");
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x - (_mouseY * _sensitivity), transform.localEulerAngles.y, transform.localEulerAngles.z);
    }
}
