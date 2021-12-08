using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class TakingDamage : MonoBehaviourPunCallbacks
{
    private float health;
    [SerializeField] private Image healthBar;

    public float startHealth = 100f;

    // Start is called before the first frame update
    void Start()
    {
        health = startHealth;
        healthBar.fillAmount = health / startHealth;
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        healthBar.fillAmount = health / startHealth;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (photonView.IsMine)
        {
            PixelGunGameManager.Instance.LeaveRoam();
        }
    }
}