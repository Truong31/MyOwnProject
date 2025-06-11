using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    public Queue<BulletTestPooling> objects;
    public BulletTestPooling objectToPool;
    public int amountToPool;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        objects = new Queue<BulletTestPooling>();
        BulletTestPooling tmp;
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.gameObject.SetActive(false);
            objects.Enqueue(tmp);
        }
    }

    public BulletTestPooling GetObjectPool()
    {
        BulletTestPooling tmp;

        if(objects.Count > 0)
        {
            tmp = objects.Dequeue();
            tmp.gameObject.SetActive(true);
        }
        else
        {
            tmp = Instantiate(objectToPool);
        }

        return tmp;
    } 

    public void ReturnObjectToPool(BulletTestPooling bullet)
    {
        objects.Enqueue(bullet);
        bullet.gameObject.SetActive(false);
    }
}
