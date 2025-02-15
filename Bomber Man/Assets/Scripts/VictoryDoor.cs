using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryDoor : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    public GameManager gameManager;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (gameManager.totalEnemies <= 0 && gameManager.time > 0)
        {
            boxCollider2D.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(UnlockLevel());
        }
    }

    private IEnumerator UnlockLevel()
    {
        if(PlayerPrefs.GetInt("UnlockedLevel") >= 6)
        {
            SoundManager.instance.ClearAllStage();
            yield return new WaitForSeconds(13.0f);
        }
        else
        {
            SoundManager.instance.ClearStage();
            yield return new WaitForSeconds(3.0f);

            if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedLevel"))
            {
                PlayerPrefs.SetInt("ReachedLevel", SceneManager.GetActiveScene().buildIndex + 1);
                PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
                PlayerPrefs.Save();
            }
        }
        SceneManager.LoadScene("MainMenu");
    }
}
