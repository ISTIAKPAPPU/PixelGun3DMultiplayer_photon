using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Camera fpsCamera;

    [SerializeField] private float fireRate = 0.1f;

    [SerializeField] private float _fireTime;

    // Update is called once per frame
    void Update()
    {
        if (_fireTime < fireRate)
        {
            _fireTime += Time.deltaTime;
        }

        if (!Input.GetButton("Fire1") || !(_fireTime > fireRate)) return;
        _fireTime = 0;
        var ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0f, 0.5f));
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.CompareTag("Player") &&
                !hit.collider.gameObject.GetComponent<PhotonView>().IsMine)
            {
                hit.collider.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.AllBuffered, 10f);
            }
        }
    }
}