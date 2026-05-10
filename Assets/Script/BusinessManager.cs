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

        UpdateUI();

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

            // Income naik
            incomePerWork *= 2;

            // Harga upgrade berikutnya naik
            workUpgradeCost *= 2;

            UpdateUI();

            Debug.Log("Work Upgraded!");
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    void UpdateUI()
    {
        if (moneyText != null)
            moneyText.text = "Money: $" + money;

        if (workUpgradeText != null)
            workUpgradeText.text =
                "Upgrade Work ($" + workUpgradeCost + ")";
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("IncomePerWork", incomePerWork);
        PlayerPrefs.SetInt("WorkUpgradeLevel", workUpgradeLevel);
        PlayerPrefs.SetInt("WorkUpgradeCost", workUpgradeCost);

        PlayerPrefs.Save();

        Debug.Log("Game Saved!");
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("Money"))
        {
            money = PlayerPrefs.GetInt("Money");
            incomePerWork = PlayerPrefs.GetInt("IncomePerWork", 10);
            workUpgradeLevel = PlayerPrefs.GetInt("WorkUpgradeLevel", 1);
            workUpgradeCost = PlayerPrefs.GetInt("WorkUpgradeCost", 50);

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
            NightclubManager.instance.UpdateUI();
        }

        UpdateUI();

        if (progressBar != null)
            progressBar.value = 0;

        Debug.Log("Game Reset!");
    }
}