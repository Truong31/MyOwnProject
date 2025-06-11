using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Type
    {
        Shield,
        Bullet,
        ExtraLife
    }

    public Type type;

    private void Update()
    {
        transform.position += Vector3.down * 5.0f * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player")
            || collision.gameObject.layer == LayerMask.NameToLayer("Shield"))
        {
            SoundManager.Instance.PowerUpSfx();
            Collect(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        switch (type)
        {
            case Type.Shield:
                player.GetComponent<PLayer>().ActiveShieldPower();
                break;
            case Type.Bullet:
                player.GetComponent<PLayer>().bulletCount++;
                break;
            case Type.ExtraLife:
                GameManager.Instance.AddLive(1);
                break;
        }
    }
}
