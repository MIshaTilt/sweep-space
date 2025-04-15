using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerRespawn = other.GetComponent<PlayerHealth>();
            if (playerRespawn != null)
            {
                playerRespawn.SetCheckpoint(transform.position);
            }
        }
    }
}
