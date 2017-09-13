using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Pools;

public class SimplePool_GameManager : MonoBehaviour
{
    public Text objCountTxt;
    public Text prefabCountTxt;

    private Pool<Bullet> bulletPool = new Pool<Bullet>();
    private Queue<Bullet> tempBulletQueue = new Queue<Bullet>();

    public Bullet bulletPrefab;
    public int startingInstanceCount;

    private void Start()
    {
        bulletPool.AddObjects(bulletPrefab, startingInstanceCount);
    }

    private void Update ()
    {
        objCountTxt.text = "Obj count: " + bulletPool.ObjectCount;
        prefabCountTxt.text = "Prefab count: " + bulletPool.PrefabCount;

        if (Input.GetKey(KeyCode.Space))
        {
            Bullet instance = bulletPool.GetObject(bulletPrefab);
            instance.Initialize(Random.rotation, Random.Range(0.0f, 20.0f));
            tempBulletQueue.Enqueue(instance);
        }

        if (Input.GetKey(KeyCode.R))
        {
            if (tempBulletQueue.Count > 0)
            {
                Bullet returnBullet = tempBulletQueue.Dequeue();
                bulletPool.ReturnObject(returnBullet, bulletPrefab);
            }
        }
    }
}
