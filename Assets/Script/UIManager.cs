using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject terminalUI;

    private bool isOpen = false;

    void Awake()
    {
        instance = this;
    }

    public void OpenTerminal()
    {
        terminalUI.SetActive(true);
        isOpen = true;
    }

    public void CloseTerminal()
    {
        terminalUI.SetActive(false);
        isOpen = false;
    }

    void Update()
    {
        if (isOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseTerminal();
        }
    }
}