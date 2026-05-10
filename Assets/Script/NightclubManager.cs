using UnityEngine;
using TMPro;

public class NightclubManager : MonoBehaviour
{
    public static NightclubManager instance;

    [Header("Nightclub Settings")]
    public int storageMoney = 0;
    public int maxStorage = 1000;

    public int passiveIncome = 5;
    public float incomeInterval = 3f;

    [Header("UI")]
    public TextMeshProUGUI nightclubText;

    private float timer;

    void Awake()
    {
        instance = this;
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

    public void UpdateUI()
    {
        nightclubText.text =
            "Nightclub Storage: $" +
            storageMoney +
            " / " +
            maxStorage;
    }

    public void CollectMoney()
    {
        if (storageMoney <= 0)
            return;

        BusinessManager.instance.money += storageMoney;

        BusinessManager.instance.SendMessage("UpdateUI");

        storageMoney = 0;

        UpdateUI();

        Debug.Log("Nightclub Money Collected!");
    }
}