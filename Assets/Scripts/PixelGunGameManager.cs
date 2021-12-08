using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class PixelGunGameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject pfPlayer;

    public static PixelGunGameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            if (pfPlayer != null)
            {
                var randomPoint = Random.Range(-20, 20);
                PhotonNetwork.Instantiate(pfPlayer.name, new Vector3(randomPoint, 0, randomPoint),
                    Quaternion.identity);
            }
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name + " PlayerCount: " +
                  PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public void LeaveRoam()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("GameLauncherScene");
    }
}