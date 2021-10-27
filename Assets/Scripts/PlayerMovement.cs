using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{  
    private CharacterController _controller;
    private float _speed = 3.5f;
    private float _gravity = 9.81f;
    [SerializeField]
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        var jump = 5f;
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;
        Vector3 dirg = new Vector3(Input.GetAxis("Horizontal"),jump , Input.GetAxis("Vertical"));
        Vector3 velocityg = dirg * _speed;

        velocity = transform.transform.TransformDirection(velocity);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _controller.Move(velocity * Time.deltaTime * 2);
            FindObjectOfType<AudioManager>().Play("Run");
            
        }
        else if(Input.GetKey(KeyCode.Space))
        {
            _controller.Move(velocityg * Time.deltaTime);
            
        }
        else
        {
            _controller.Move(velocity * Time.deltaTime);
           // FindObjectOfType<AudioManager>().Play("Walker");
            //audioSource.Play();
        }
    }



}
