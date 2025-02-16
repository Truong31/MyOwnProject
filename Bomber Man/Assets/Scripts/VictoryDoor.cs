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
            Debug.Log("ReachedLevel" + PlayerPrefs.GetInt("ReachedLevel"));
            Debug.Log("UnlockedLevel" + PlayerPrefs.GetInt("UnlockedLevel", 1));
            collision.gameObject.GetComponent<Player>().enabled = false;
            StartCoroutine(UnlockLevel());
        }
    }

    private IEnumerator UnlockLevel()
    {
        gameManager.isFinish = true;
        if(PlayerPrefs.GetInt("UnlockedLevel") >= 6)
        {
            SoundManager.instance.ClearAllStage();
            yield return new WaitForSeconds(13.0f);

            SceneManager.LoadScene("MainMenu");
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
            SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("UnlockedLevel", 1));
        }
        
    }
}
