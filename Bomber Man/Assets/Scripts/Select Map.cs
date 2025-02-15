using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMap : MonoBehaviour
{
    private string selectedMap;

    public void ChooseMap(string map)
    {
        selectedMap = map;
    }

    public void ReturnMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(selectedMap);
    }
}
