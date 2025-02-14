using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerSelect : MonoBehaviour
{
    private int numberOfPlayers;
    public TMP_Dropdown dropDown;
    public Toggle[] player1;
    public Toggle[] player2;
    public Toggle[] player3;
    public Toggle[] player4;

    //Vô hiệu hóa toàn bộ các nút bấm
    private void Start()
    {
        foreach(Toggle player in player1)
        {
            player.interactable = false;
        }

        foreach (Toggle player in player2)
        {
            player.interactable = false;
        }

        foreach (Toggle player in player3)
        {
            player.interactable = false;
        }

        foreach (Toggle player in player4)
        {
            player.interactable = false;
        }

    }

    private void Update()
    {
        if (dropDown.interactable)
        {
            numberOfPlayers = int.Parse(dropDown.options[dropDown.value].text);
        }
    }

    //Mở khóa chọn người chơi sau khi chốt số lượng
    public void UnlockSelectPlayer()
    {
        List<Toggle[]> list = new List<Toggle[]> { player1, player2, player3, player4 };
        for (int i = 0; i < numberOfPlayers; i++)
        {
            foreach (Toggle player in list[i])
            {
                player.interactable = true;
            }
        }
    }

    //Quay trở về màn hình chính
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //Load đến map đang được chọn
    public void LoadScene()
    {
        //if(selectedMap != null)
        //{
        //    SceneManager.LoadScene("SelectMap");
        //}
    }
}
