using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Gun : MonoBehaviour
{
    //public Health hth;
    int totalAmmo = 100;
    int clipSize = 6;
    public float damage = 100f;
    public float range = 10f;
    public float fireRate = .25f;
    public Camera fpsCam;
    public int maxAmmo = 10;
    private int currentAmmo = -1;
    public float reloadTime = 3f;
    private float _nextFire;
    private bool _isReload = false;
    public Text ammo;
    public Animator animator;
    //public Text totalAmmo;
    PhotonView PV;
    public Animator shooting;
    int dam = 50;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<PhotonView>();
        PV = GetComponent<PhotonView>();
        if (currentAmmo == -1)
        {
            currentAmmo = clipSize;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (PV.IsMine)
        {
            ammo.text = currentAmmo + "/" + totalAmmo;
        }

            if (_isReload)
                return;
            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }

        if (Input.GetButtonDown("Fire1") && Time.time > _nextFire)
        {
            if (PV.IsMine)
            {
                _nextFire = Time.time + fireRate;

                
                Shoot();
                
                shooting.Play("demo_combat_shoot");
                
            }
        }
        
    }

    IEnumerator Reload()
    {
        _isReload = true;

        //animator.SetBool("Reloading", true);
        
        yield return new WaitForSeconds(reloadTime);
        //animator.SetBool("Reloading", false);
        
        if (PV.IsMine)
        {
            currentAmmo = clipSize;
            totalAmmo = totalAmmo - clipSize;
        }
        _isReload = false;
    }

    void Shoot()
    {
        
        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);
            if (hit.collider.gameObject.CompareTag("Player") && !hit.collider.gameObject.GetComponent<PhotonView>().IsMine)
            {
                hit.collider.gameObject.GetComponent<PhotonView>().RPC("TakeDamageFromBullet", RpcTarget.AllBuffered, 10f);
                Health.hp = Health.hp - dam;
            }
            



            FindObjectOfType<AudioManager>().Play("ShootPistol");
        }
    }

}