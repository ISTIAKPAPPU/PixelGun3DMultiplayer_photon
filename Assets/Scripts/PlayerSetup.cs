using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerSetup : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI playerNameTxt;

    [SerializeField] private GameObject fpsCamera;

    // Start is called before the first frame update
    private void Start()
    {
        if (photonView.IsMine)
        {
           
        }
        else
        {
         
        }

        SetPlayerUi();
    }

    private void SetPlayerUi()
    {
        playerNameTxt.text = photonView.Owner.NickName;
    }
}