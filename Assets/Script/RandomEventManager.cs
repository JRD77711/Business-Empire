using UnityEngine;
using TMPro;

public class RandomEventManager : MonoBehaviour
{
    public static RandomEventManager instance;

    [Header("Event Settings")]
    public float eventInterval = 10f;
    private float timer;

    [Header("UI")]
    public TextMeshProUGUI eventLogText;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateLog("Event Log: Waiting for market update...");
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= eventInterval)
        {
            timer = 0;
            TriggerRandomEvent();
        }
    }

    void TriggerRandomEvent()
    {
        int randomEvent = Random.Range(0, 4);

        if (randomEvent == 0)
        {
            BusinessManager.instance.money += 50;
            BusinessManager.instance.UpdateUI();

            UpdateLog("Market Boom! You gained $50.");
        }
        else if (randomEvent == 1)
        {
            BusinessManager.instance.money -= 30;

            if (BusinessManager.instance.money < 0)
                BusinessManager.instance.money = 0;

            BusinessManager.instance.UpdateUI();

            UpdateLog("Maintenance Cost! You lost $30.");
        }
        else if (randomEvent == 2)
        {
            NightclubManager.instance.storageMoney += 100;

            if (NightclubManager.instance.storageMoney > NightclubManager.instance.maxStorage)
                NightclubManager.instance.storageMoney = NightclubManager.instance.maxStorage;

            NightclubManager.instance.UpdateUI();

            UpdateLog("Nightclub Bonus! Storage gained $100.");
        }
        else if (randomEvent == 3)
        {
            NightclubManager.instance.storageMoney -= 50;

            if (NightclubManager.instance.storageMoney < 0)
                NightclubManager.instance.storageMoney = 0;

            NightclubManager.instance.UpdateUI();

            UpdateLog("Storage Issue! Nightclub lost $50.");
        }
    }

    void UpdateLog(string message)
    {
        if (eventLogText != null)
            eventLogText.text = message;

        if (AudioManager.instance != null)
            AudioManager.instance.PlayEvent();

        Debug.Log(message);
    }
}
