using UnityEngine;
using TMPro;

public class BusinessManager : MonoBehaviour
{
    public static BusinessManager instance;

    public int money = 0;
    public int incomePerClick = 10;

    public TextMeshProUGUI moneyText;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddMoney()
    {
        money += incomePerClick;
        UpdateUI();
    }

    void UpdateUI()
    {
        moneyText.text = "Money: $" + money;
    }
}