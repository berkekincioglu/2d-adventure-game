using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    EnemyController[] enemies;
    public UIHandler uiHandler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemies = FindObjectsByType<EnemyController>(FindObjectsSortMode.None);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.CurrentHealth <= 0)
        {
            uiHandler.DisplayLoseScreen();
            Invoke(nameof(ReloadScene), 3f);
        }
        if (AllEnemiesFixed())
        {
            uiHandler.DisplayWinScreen();
            Invoke(nameof(ReloadScene), 3f);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    bool AllEnemiesFixed()
    {
        foreach (EnemyController enemy in enemies)
        {
            if (enemy.isBroken)
            {
                return false;
            }
        }
        return true;
    }
}
