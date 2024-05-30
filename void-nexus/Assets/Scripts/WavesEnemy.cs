using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesEnemy : MonoBehaviour
{
    [SerializeField] private Target[] firstWaveEnemies;
    [SerializeField] private Target[] secondWaveEnemies;
    [SerializeField] private Target[] thirdWaveEnemies;

    private bool firstWaveActivated = false;
    private bool firstWaveCleared = false;
    private bool secondWaveCleared = false;

    void Start()
    {
        // ������������ ���� ������ � ������ ����
        SetActiveEnemies(firstWaveEnemies, false);
        SetActiveEnemies(secondWaveEnemies, false);
        SetActiveEnemies(thirdWaveEnemies, false);
    }

    void Update()
    {
        if (firstWaveActivated && !firstWaveCleared && AllEnemiesAreDead(firstWaveEnemies))
        {
            firstWaveCleared = true;
            Debug.Log("��� ����� �� ������ ����� ������. ���������� ������ ����� ������.");
            SetActiveEnemies(secondWaveEnemies, true);
        }

        if (firstWaveCleared && !secondWaveCleared && AllEnemiesAreDead(secondWaveEnemies))
        {
            secondWaveCleared = true;
            Debug.Log("��� ����� �� ������ ����� ������. ���������� ������ ����� ������.");
            SetActiveEnemies(thirdWaveEnemies, true);
        }
    }

    private bool AllEnemiesAreDead(Target[] enemies)
    {
        foreach (Target enemy in enemies)
        {
            if (!enemy.died)
            {
                return false;
            }
        }
        return true;
    }

    private void SetActiveEnemies(Target[] enemies, bool isActive)
    {
        foreach (Target enemy in enemies)
        {
            enemy.gameObject.SetActive(isActive);
        }
    }

    public void ActivateFirstWave()
    {
        firstWaveActivated = true;
        SetActiveEnemies(firstWaveEnemies, true);
        Debug.Log("������ ����� ������ ������������.");
    }
}
