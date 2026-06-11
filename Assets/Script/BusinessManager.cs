using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class BusinessManager : MonoBehaviour
{
    public static BusinessManager instance;

    [Header("Money")]
    public int money = 0;

    [Header("Work Settings")]
    public bool isWorking = false;
    public float workTime = 3f;
    public int incomePerWork = 10;

    [Header("Upgrade System")]
    public int workUpgradeLevel = 1;
    public int workUpgradeCost = 50;

    [Header("UI")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI workUpgradeText;
    public Slider progressBar;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateUI();

        if (progressBar != null)
            progressBar.value = 0;
    }

    public void StartWork()
    {
        if (!isWorking)
        {
            StartCoroutine(WorkCoroutine());
        }
    }

    IEnumerator WorkCoroutine()
    {
        isWorking = true;

        float timer = 0;

        while (timer < workTime)
        {
            timer += Time.unscaledDeltaTime;

            if (progressBar != null)
                progressBar.value = timer / workTime;

            yield return null;
        }

        money += incomePerWork;

        if (FloatingTextSpawner.instance != null)
            FloatingTextSpawner.instance.SpawnText("+$" + incomePerWork);

        if (AudioManager.instance != null)
            AudioManager.instance.PlayCash();

        UpdateUI();

        SaveGame();

        if (progressBar != null)
            progressBar.value = 0;

        isWorking = false;
    }

    public void UpgradeWork()
    {
        if (money >= workUpgradeCost)
        {
            money -= workUpgradeCost;

            workUpgradeLevel++;
            incomePerWork *= 2;
            workUpgradeCost *= 2;

            UpdateUI();
            SaveGame();
            if (AudioManager.instance != null)
                AudioManager.instance.PlayUpgrade();

            Debug.Log("Work Upgraded!");
        }
        else
        {
            Debug.Log("Not enough money!");
            if (AudioManager.instance != null)
                AudioManager.instance.PlayError();
        }
    }

    public void UpdateUI()
    {
        if (moneyText != null)
            moneyText.text = "Money: $" + money;

        if (workUpgradeText != null)
            workUpgradeText.text = "Upgrade Work ($" + workUpgradeCost + ")";
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("Money", money);

        PlayerPrefs.SetInt("IncomePerWork", incomePerWork);
        PlayerPrefs.SetInt("WorkUpgradeLevel", workUpgradeLevel);
        PlayerPrefs.SetInt("WorkUpgradeCost", workUpgradeCost);

        if (NightclubManager.instance != null)
        {
            PlayerPrefs.SetInt("NightclubStorageMoney", NightclubManager.instance.storageMoney);
            PlayerPrefs.SetInt("NightclubMaxStorage", NightclubManager.instance.maxStorage);
            PlayerPrefs.SetInt("NightclubPassiveIncome", NightclubManager.instance.passiveIncome);

            PlayerPrefs.SetInt("NightclubUpgradeLevel", NightclubManager.instance.nightclubUpgradeLevel);
            PlayerPrefs.SetInt("NightclubUpgradeCost", NightclubManager.instance.nightclubUpgradeCost);

            PlayerPrefs.SetInt("StorageUpgradeLevel", NightclubManager.instance.storageUpgradeLevel);
            PlayerPrefs.SetInt("StorageUpgradeCost", NightclubManager.instance.storageUpgradeCost);
        }

        PlayerPrefs.Save();

        Debug.Log("Game Saved!");
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("Money"))
        {
            money = PlayerPrefs.GetInt("Money", 0);

            incomePerWork = PlayerPrefs.GetInt("IncomePerWork", 10);
            workUpgradeLevel = PlayerPrefs.GetInt("WorkUpgradeLevel", 1);
            workUpgradeCost = PlayerPrefs.GetInt("WorkUpgradeCost", 50);

            if (NightclubManager.instance != null)
            {
                NightclubManager.instance.storageMoney = PlayerPrefs.GetInt("NightclubStorageMoney", 0);
                NightclubManager.instance.maxStorage = PlayerPrefs.GetInt("NightclubMaxStorage", 1000);
                NightclubManager.instance.passiveIncome = PlayerPrefs.GetInt("NightclubPassiveIncome", 10);

                NightclubManager.instance.nightclubUpgradeLevel = PlayerPrefs.GetInt("NightclubUpgradeLevel", 1);
                NightclubManager.instance.nightclubUpgradeCost = PlayerPrefs.GetInt("NightclubUpgradeCost", 100);

                NightclubManager.instance.storageUpgradeLevel = PlayerPrefs.GetInt("StorageUpgradeLevel", 1);
                NightclubManager.instance.storageUpgradeCost = PlayerPrefs.GetInt("StorageUpgradeCost", 150);

                NightclubManager.instance.UpdateUI();
            }

            UpdateUI();

            Debug.Log("Game Loaded!");
        }
        else
        {
            Debug.Log("No Save Data Found");
        }
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();

        money = 0;

        incomePerWork = 10;
        workUpgradeLevel = 1;
        workUpgradeCost = 50;

        if (NightclubManager.instance != null)
        {
            NightclubManager.instance.storageMoney = 0;
            NightclubManager.instance.maxStorage = 1000;
            NightclubManager.instance.passiveIncome = 10;

            NightclubManager.instance.nightclubUpgradeLevel = 1;
            NightclubManager.instance.nightclubUpgradeCost = 100;

            NightclubManager.instance.storageUpgradeLevel = 1;
            NightclubManager.instance.storageUpgradeCost = 150;

            NightclubManager.instance.UpdateUI();
        }

        UpdateUI();

        if (progressBar != null)
            progressBar.value = 0;

        Debug.Log("Game Reset!");
    }
}