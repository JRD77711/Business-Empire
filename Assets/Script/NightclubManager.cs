using UnityEngine;
using TMPro;

public class NightclubManager : MonoBehaviour
{
    public static NightclubManager instance;

    [Header("Nightclub Storage")]
    public int storageMoney = 0;
    public int maxStorage = 1000;

    [Header("Passive Income")]
    public int passiveIncome = 10;
    public float incomeInterval = 3f;

    [Header("Upgrade")]
    public int nightclubUpgradeLevel = 1;
    public int nightclubUpgradeCost = 100;

    public int storageUpgradeLevel = 1;
    public int storageUpgradeCost = 150;

    [Header("UI")]
    public TextMeshProUGUI nightclubText;
    public TextMeshProUGUI nightclubUpgradeText;
    public TextMeshProUGUI storageUpgradeText;

    private float timer;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        GenerateIncome();
    }

    void GenerateIncome()
    {
        if (storageMoney >= maxStorage)
            return;

        timer += Time.deltaTime;

        if (timer >= incomeInterval)
        {
            timer = 0;

            storageMoney += passiveIncome;

            if (storageMoney > maxStorage)
                storageMoney = maxStorage;

            UpdateUI();
        }
    }

    public void CollectMoney()


    {
        if (storageMoney <= 0)
            return;

        BusinessManager.instance.money += storageMoney;

        if (FloatingTextSpawner.instance != null)
            FloatingTextSpawner.instance.SpawnText("+$" + storageMoney + " Collected");
        storageMoney = 0;

        BusinessManager.instance.UpdateUI();
        UpdateUI();

        BusinessManager.instance.SaveGame();

        Debug.Log("Nightclub Money Collected!");

        if (AudioManager.instance != null)
            AudioManager.instance.PlayCash();
    }

    public void UpgradeNightclubIncome()
    {
        if (BusinessManager.instance.money >= nightclubUpgradeCost)
        {
            BusinessManager.instance.money -= nightclubUpgradeCost;

            nightclubUpgradeLevel++;
            passiveIncome *= 2;
            nightclubUpgradeCost *= 2;

            BusinessManager.instance.UpdateUI();
            UpdateUI();

            BusinessManager.instance.SaveGame();

            Debug.Log("Nightclub Income Upgraded!");
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    public void UpgradeStorage()
    {
        if (BusinessManager.instance.money >= storageUpgradeCost)
        {
            BusinessManager.instance.money -= storageUpgradeCost;

            storageUpgradeLevel++;
            maxStorage += 1000;
            storageUpgradeCost *= 2;

            BusinessManager.instance.UpdateUI();
            UpdateUI();

            BusinessManager.instance.SaveGame();

            if (AudioManager.instance != null)
                AudioManager.instance.PlayUpgrade();

            Debug.Log("Storage Upgraded!");
        }
        else
        {
            if (AudioManager.instance != null)
                AudioManager.instance.PlayError();
            Debug.Log("Not enough money!");
        }
    }

    public void UpdateUI()
    {
        if (nightclubText != null)
        {
            if (storageMoney >= maxStorage)
            {
                nightclubText.text = "Nightclub Storage: $" + storageMoney + " / " + maxStorage + " (FULL)";
            }
            else
            {
                nightclubText.text = "Nightclub Storage: $" + storageMoney + " / " + maxStorage;
            }
        }

        if (nightclubUpgradeText != null)
            nightclubUpgradeText.text = "Upgrade Nightclub ($" + nightclubUpgradeCost + ")";

        if (storageUpgradeText != null)
            storageUpgradeText.text = "Upgrade Storage ($" + storageUpgradeCost + ")";
    }
}