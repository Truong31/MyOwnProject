using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    public SpriteAnimation explosionStart;
    public SpriteAnimation explosionMiddle;
    public SpriteAnimation explosionEnd;

    public void Animation(SpriteAnimation spriteAnimation)
    {
        explosionStart.enabled = spriteAnimation == explosionStart;
        explosionMiddle.enabled = spriteAnimation == explosionMiddle;
        explosionEnd.enabled = spriteAnimation == explosionEnd;
    }

    public void SetDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void DestroyAfter(float time)
    {
        Destroy(this.gameObject, time);
    }
}
