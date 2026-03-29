using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    EnemyController[] enemies;
    public UIHandler uiHandler;
    private int enemiesFixed = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemies = FindObjectsByType<EnemyController>(FindObjectsSortMode.None);

        foreach (var enemy in enemies)
        {
            enemy.OnFixed += HandleEnemyFixed;
        }
        uiHandler.SetCounter(0, enemies.Length);
        player.OnTalkedToNPC += HandlePlayerTalkedToNPC;
    }



    // Update is called once per frame
    void Update()
    {
        if (player.CurrentHealth <= 0)
        {
            uiHandler.DisplayLoseScreen();
            Invoke(nameof(ReloadScene), 3f);
        }
        // if (AllEnemiesFixed())
        // {
        //     uiHandler.DisplayWinScreen();
        //     Invoke(nameof(ReloadScene), 3f);
        // }
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

    void OnDestroy()
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
                enemy.OnFixed -= HandleEnemyFixed;
        }

        player.OnTalkedToNPC -= HandlePlayerTalkedToNPC;
    }

    void HandleEnemyFixed()
    {
        enemiesFixed++;
        uiHandler.SetCounter(enemiesFixed, enemies.Length);
    }

    void HandlePlayerTalkedToNPC()
    {
        if (AllEnemiesFixed())
        {
            uiHandler.DisplayWinScreen();
            Invoke(nameof(ReloadScene), 3f);
        }
        else
        {
            UIHandler.instance.DisplayDialogue();
        }
    }
}
