using System.Collections;
using UnityEngine;

public class GameClock : MonoBehaviour
{
    [SerializeField] private WeaponSwitcher weaponSwitcher;
    [SerializeField] private Player player;
    [SerializeField] private float timerInterval = 10f; 
    [SerializeField] private float energyRegenerationInterval = 0.1f;
    [SerializeField] private GameObject changeWeaponScreen;
    [SerializeField] private RectTransform clockHand; 
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private int blinkCount = 1;
    [SerializeField] private MobSpawner mobSpawner1;
    [SerializeField] private MobSpawner mobSpawner2;
    [SerializeField] private MobSpawner mobSpawner3;
    [SerializeField] private MobSpawner mobSpawner4;

    private int counter = 0;
    private float timer = 0f;
    private float energyTimer = 0f;
    private CanvasGroup changeWeaponCanvasGroup;

    void Start()
    {
        if (changeWeaponScreen != null)
        {
            changeWeaponCanvasGroup = changeWeaponScreen.GetComponent<CanvasGroup>();
            if (changeWeaponCanvasGroup == null)
            {
                changeWeaponCanvasGroup = changeWeaponScreen.AddComponent<CanvasGroup>();
            }
            changeWeaponCanvasGroup.alpha = 0;
            changeWeaponScreen.SetActive(false);
        }

        StartCoroutine(MoveClockHandOverTime()); 
    }

    void Update()
    {
        energyTimer += Time.deltaTime;

        if (energyTimer >= energyRegenerationInterval)
        {
            energyTimer = 0f;
            player.addEnergy(1);
        }
    }

    private IEnumerator MoveClockHandOverTime()
    {
        while (true)
        {
            float startAngle = clockHand.eulerAngles.z;
            float targetAngle = startAngle - 90f; 
            float elapsedTime = 0f;

            while (elapsedTime < timerInterval)
            {
                float progress = elapsedTime / timerInterval;
                float newAngle = Mathf.Lerp(startAngle, targetAngle, progress);
                clockHand.eulerAngles = new Vector3(0, 0, newAngle);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            clockHand.eulerAngles = new Vector3(0, 0, targetAngle);

            counter++;
            weaponSwitcher.SwitchWeapon(1);
            mobSpawner1.spawnMobs();
            mobSpawner2.spawnMobs();
            mobSpawner3.spawnMobs();
            mobSpawner4.spawnMobs();
            ShowChangeWeaponScreen();

            switch (counter)
            {
                case 1:
                    changeDamage(1);
                    changeHealth(1);
                    break;
                case 2:
                    changeDamage(2);
                    changeHealth(1);
                    break;
            }

            if (counter >= 4)
            {
                StopCoroutine(MoveClockHandOverTime());
            }
        }
    }

    private void ShowChangeWeaponScreen()
    {
        if (changeWeaponScreen != null)
        {
            changeWeaponScreen.SetActive(true);
            StartCoroutine(BlinkScreen());
        }
    }

    private IEnumerator BlinkScreen()
    {
        int blinkCycle = 0;
        float elapsedTime = 0f;

        while (blinkCycle < blinkCount)
        {
            elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                changeWeaponCanvasGroup.alpha = Mathf.Lerp(0f, 0.5f, elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            changeWeaponCanvasGroup.alpha = 0.5f;

            elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                changeWeaponCanvasGroup.alpha = Mathf.Lerp(0.5f, 0f, elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            changeWeaponCanvasGroup.alpha = 0f;

            blinkCycle++;
        }

        changeWeaponScreen.SetActive(false);
    }

    private void changeHealth(int multiplier)
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        foreach (Enemy enemy in enemies)
        {
            switch (enemy.getEnemyType())
            {
                case Enemy.enemyType.Worlob:
                    enemy.setHealth(enemy.getHealth() + 30 * multiplier);
                    break;
                case Enemy.enemyType.Splob:
                    enemy.setHealth(enemy.getHealth() + 20 * multiplier);
                    break;
                case Enemy.enemyType.Rihlob:
                    enemy.setHealth(enemy.getHealth() + 20 * multiplier);
                    break;
                case Enemy.enemyType.Umbrlob:
                    enemy.setHealth(enemy.getHealth() + 10 * multiplier);
                    break;
            }
        }
    }

    private void changeDamage(int multiplier)
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        foreach (Enemy enemy in enemies)
        {
            switch (enemy.getEnemyType())
            {
                case Enemy.enemyType.Worlob:
                    enemy.setDamage(enemy.getDamage() + 50 * multiplier);
                    break;
                case Enemy.enemyType.Splob:
                    enemy.setDamage(enemy.getDamage() + 20 * multiplier);
                    break;
                case Enemy.enemyType.Rihlob:
                    enemy.setDamage(enemy.getDamage() + 30 * multiplier);
                    break;
                case Enemy.enemyType.Umbrlob:
                    enemy.setDamage(enemy.getDamage() + 10 * multiplier);
                    break;
            }
        }
    }
}
