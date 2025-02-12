using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum TYPE
    {
        ExtraBomb,
        Speed,
        ExtraBlast
    }

    public TYPE power;

    //NOTE: Tang cac chi so cua Player sau khi an Power
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SoundManager.instance.PowerUp();
            if (power.Equals(TYPE.Speed))
            {
                collision.gameObject.GetComponent<Player>().speed++;
            }
            else if (power.Equals(TYPE.ExtraBomb))
            {
                collision.gameObject.GetComponent<BombController>().AddBomb();
            }
            else if (power.Equals(TYPE.ExtraBlast))
            {
                collision.gameObject.GetComponent<BombController>().explosionRadius++;
            }

            Destroy(this.gameObject);
        }
    }
}
