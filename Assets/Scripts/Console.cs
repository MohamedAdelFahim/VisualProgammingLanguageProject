using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Console : MonoBehaviour
{
    public static Console _instance;
    public Text consoleText;
    public GameObject helpPanel;
    private bool helpActive;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        consoleText.text = "";
        helpActive = false;
    }

    public string appendText(string text)
    {
        consoleText.text += "\n" + text;
        return consoleText.text;
    }

    public void updateText(string text)
    {
        consoleText.text = text;
    }

    public void clearText()
    {
        consoleText.text = "";
    }

    public string errorMessage(string text)
    {
        consoleText.text += "\n<color=#FF0000> " + text + " </color>";
        return consoleText.text;
    }

    public void showHelp()
    {
        helpActive = !helpActive;
        helpPanel.SetActive(helpActive);
    }

    public void exitFP7R()
    {
        Application.Quit();
    }
}
