using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    public WavesEnemy enemyManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyManager.ActivateFirstWave();
            //gameObject.SetActive(false); // Деактивируем триггер после активации первой волны
        }
    }
}
