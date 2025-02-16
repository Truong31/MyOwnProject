using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Button[] buttons;
    public GameObject levelButton;
    private void Awake()
    {
        AddLevelButton();
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        for(int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void loadLevel(int level)
    {
        SceneManager.LoadScene("Level" + level);
    }

    public void ResetLevel()
    {
        PlayerPrefs.SetInt("UnlockedLevel", 1);
        PlayerPrefs.SetInt("ReachedLevel", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void AddLevelButton()
    {
        int count = levelButton.transform.childCount;
        buttons = new Button[count];
        for(int i = 0; i < count; i++)
        {
            buttons[i] = levelButton.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }
}
