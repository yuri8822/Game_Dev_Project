using UnityEngine;

public class FireShieldPowerUp : MonoBehaviour
{
    public GameObject fire_shield; 
    public float shieldDuration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMechanics player = other.GetComponent<PlayerMechanics>();
            if (player != null)
            {
                player.ActivateFireShield();
                if (fire_shield != null)
                {
                    Destroy(fire_shield);
                }
            }
        }
    }
}
