using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    public BombExplosion explosionPrefabs;
    public int explosionRadius;
    public int bombRemain;
    public GameObject bombPrefabs;
    public float explosionTime;
    public LayerMask explosionLayer;
    public Tilemap brick;
    public GameObject brickDestruction;
    private void Update()
    {
        if(bombRemain > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(PlaceBomb());
        }
    }

    private IEnumerator PlaceBomb()
    {
        Vector2 position = this.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject bomb = Instantiate(bombPrefabs, position, Quaternion.identity);
        bombRemain--;
        yield return new WaitForSeconds(3.0f);

        position = bomb.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        BombExplosion explosion = Instantiate(explosionPrefabs, position, Quaternion.identity);
        explosion.Animation(explosion.explosionStart);
        explosion.DestroyAfter(explosionTime);

        Explode(Vector2.up, position, explosionRadius);
        Explode(Vector2.down, position, explosionRadius);
        Explode(Vector2.left, position, explosionRadius);
        Explode(Vector2.right, position, explosionRadius);

        Destroy(bomb);
        bombRemain++;
    }

    private void Explode(Vector2 direction, Vector2 position, int length)
    {
        if(length <= 0)
        {
            return;
        }
        position += direction;

        if (Physics2D.OverlapBox(position, Vector2.one / 2, 0, explosionLayer))
        {
            ClearBrick(position);
            return;
        }

        BombExplosion explosion = Instantiate(explosionPrefabs, position, Quaternion.identity);

        explosion.Animation(length > 1 ? explosion.explosionMiddle : explosion.explosionEnd);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionTime);

        Explode(direction, position, length - 1);
    }

    private void ClearBrick(Vector2 position)
    {
        Vector3Int cell = brick.WorldToCell(position);
        TileBase tile = brick.GetTile(cell);

        if(tile != null)
        {
            Instantiate(brickDestruction, position, Quaternion.identity);
            brick.SetTile(cell, null);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            collision.isTrigger = false;
        }
    }
}
