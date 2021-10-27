using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using ExitGames.Client.Photon;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;
    [SerializeField] GameObject cameraHolder;
    [SerializeField] Item[] items;
    int itemIndex;
    int previousItemIndex=-1;
    Rigidbody rb;
    float verticalLookRotation;
    bool grounded;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;
    public Animator anim;
    bool isIdeal = true;

    PhotonView PV;
   

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        

          /*  if(PV.IsMine)
        {
            EquipItem(0);
        }
        */
            if(!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
        }
    }
    private void Update()
    {


         //anim.Play("demo_combat_idle");
        anim.SetBool("isrun", false);
        //anim.Play("Walking");

        if (!PV.IsMine)
        {
            return;
        }
        Look();
        Move();
        Jump();

       /* for(int i=0;i<items.Length;i++)
        {
            if(Input.GetKeyDown((i+1).ToString()))
            {
                EquipItem(i);
                break;
            }
        }

        if(Input.GetAxisRaw("Mouse ScrollWheel")>0f)
        {
            if (itemIndex >= items.Length - 1)
            {
                EquipItem(0);
            }
            else
            {
                EquipItem(itemIndex + 1);
            }
        }
        else if(Input.GetAxisRaw("Mouse ScrollWheel")<0f)
        {
            if (itemIndex <= 0)
            {
                EquipItem(items.Length - 1);
            }
            else
            {
                EquipItem(itemIndex - 1);
            }
        }
        */
    }
    
    void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);
        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    void Move()
    {



        //  anim.Play("Walking");
        if (PV.IsMine)
        {
            Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //anim.Play("demo_combat_run");
                anim.SetBool("isrun", true);
            }
            else
            {
                //anim.Play("demo_combat_idle");
                anim.SetBool("isrun", false);
            }

            moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
        }
    
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Debug.Log("Jumping");
            anim.Play("demo_combat_run");
            rb.AddForce(transform.up * jumpForce);
        }
    }

   /* void EquipItem(int _index)
    {
        if (_index == previousItemIndex)
            return;
        itemIndex = _index;
        items[itemIndex].itemGameObject.SetActive(true);

        if(previousItemIndex!=-1)
        {
            items[previousItemIndex].itemGameObject.SetActive(false);
        }
        previousItemIndex = itemIndex;

        if(PV.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("itemIndex", itemIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }
    
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if(!PV.IsMine && targetPlayer==PV.Owner)
        {
            EquipItem((int)changedProps["itemIndex"]);
        }
    } */
    public void SetGroundedState(bool _grounded)
    {
        grounded = _grounded;
    }

    private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }
}
