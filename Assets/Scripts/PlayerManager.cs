using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using Photon;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoint;
    int numberPlayers;
    PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        CheckPlayers();
        //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Soldier_demo"), new Vector3(0f, 1f, 0f), Quaternion.Euler(0f, 90f, 0f));
        if (numberPlayers == 1)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Soldier_demo"), spawnPoint[0].position, spawnPoint[0].rotation, 0);
            numberPlayers = 2;
            
        }

        else if (numberPlayers == 2)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Soldier_demo"), spawnPoint[1].position, spawnPoint[1].rotation, 0);
            numberPlayers = 3;
           
        }
        else if (numberPlayers == 3)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Soldier_demo"), spawnPoint[2].position, spawnPoint[2].rotation, 0);
            numberPlayers = 4;
            
        }
        else if (numberPlayers == 4)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Soldier_demo"), spawnPoint[3].position, spawnPoint[3].rotation, 0);
            numberPlayers = 1;
           
        }
    }
    void CheckPlayers()
    {
        numberPlayers = PhotonNetwork.CountOfPlayers;
        //if the number of player is heigher than the number of spawnpoint in the game (in this case 4),
        //spawn the players in round order
        for (int i = 0; i <= numberPlayers; i++)
        {
            if (numberPlayers > 4)
            {
                numberPlayers -= 4;
            }

        }
    }
}
