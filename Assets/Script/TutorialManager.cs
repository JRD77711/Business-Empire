using UnityEngine;
using TMPro;
using System.Collections;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    [Header("UI")]
    public TextMeshProUGUI tutorialText;
    public GameObject tutorialPanel;

    [Header("Tutorial State")]
    public int tutorialStep = 0;

    private int lastMoney = 0;
    private int lastStorage = 0;
    private bool observedWorkInProgress = false;
    private bool tutorialFinished = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (BusinessManager.instance != null)
            lastMoney = BusinessManager.instance.money;

        if (NightclubManager.instance != null)
            lastStorage = NightclubManager.instance.storageMoney;

        UpdateTutorialText();
    }

    void Update()
    {
        if (tutorialFinished)
            return;

        if (BusinessManager.instance == null)
            return;

        CheckTutorialProgress();

        lastMoney = BusinessManager.instance.money;

        if (NightclubManager.instance != null)
            lastStorage = NightclubManager.instance.storageMoney;
    }

    void CheckTutorialProgress()
    {
        if (tutorialStep == 0)
        {
            if (UIManager.instance != null &&
                UIManager.instance.terminalUI != null &&
                UIManager.instance.terminalUI.activeSelf)
            {
                NextStep();
            }
        }
        else if (tutorialStep == 1)
        {
            if (BusinessManager.instance.isWorking)
            {
                observedWorkInProgress = true;
            }

            if (observedWorkInProgress &&
                !BusinessManager.instance.isWorking &&
                BusinessManager.instance.money > lastMoney)
            {
                NextStep();
            }
        }
        else if (tutorialStep == 2)
        {
            if (NightclubManager.instance != null)
            {
                bool storageWasCollected =
                    lastStorage > 0 &&
                    NightclubManager.instance.storageMoney == 0 &&
                    BusinessManager.instance.money > lastMoney;

                if (storageWasCollected)
                {
                    NextStep();
                }
            }
        }
        else if (tutorialStep == 3)
        {
            bool workUpgraded = BusinessManager.instance.workUpgradeLevel > 1;

            bool nightclubUpgraded =
                NightclubManager.instance != null &&
                NightclubManager.instance.nightclubUpgradeLevel > 1;

            bool storageUpgraded =
                NightclubManager.instance != null &&
                NightclubManager.instance.storageUpgradeLevel > 1;

            if (workUpgraded || nightclubUpgraded || storageUpgraded)
            {
                NextStep();
            }
        }
    }

    public void CompleteSaveTutorial()
    {
        if (tutorialStep == 4)
        {
            NextStep();
        }
    }

    void NextStep()
    {
        tutorialStep++;
        UpdateTutorialText();
    }

    void UpdateTutorialText()
    {
        if (tutorialText == null)
            return;

        if (tutorialStep == 0)
        {
            tutorialText.text = "Tutorial 1/5: Approach the PC and press E to open the terminal.";
        }
        else if (tutorialStep == 1)
        {
            tutorialText.text = "Tutorial 2/5: Click the Work button to earn money.";
        }
        else if (tutorialStep == 2)
        {
            tutorialText.text = "Tutorial 3/5: Wait for the Nightclub Storage to fill up, then click Collect in the terminal.";
        }
        else if (tutorialStep == 3)
        {
            tutorialText.text = "Tutorial 4/5: Earn enough money, then purchase one upgrade.";
        }
        else if (tutorialStep == 4)
        {
            tutorialText.text = "Tutorial 5/5: Click Save to store your game progress.";
        }
        else
        {
            tutorialText.text = "Tutorial complete. Keep growing your business and upgrading your nightclub!";
            tutorialFinished = true;
            StartCoroutine(HideTutorialAfterDelay());
        }

        if (tutorialPanel != null)
            tutorialPanel.SetActive(true);
        else
            tutorialText.gameObject.SetActive(true);
    }

    IEnumerator HideTutorialAfterDelay()
    {
        yield return new WaitForSecondsRealtime(10f);

        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);
        else if (tutorialText != null)
            tutorialText.gameObject.SetActive(false);
    }
}