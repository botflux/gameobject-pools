using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pools
{
    public class Pool<T> where T : MonoBehaviour
    {
        private Dictionary<int, Queue<T>> poolDictonary = new Dictionary<int, Queue<T>>();

        public int ObjectCount
        {
            get
            {
                return objectCount;
            }
        }

        public int PrefabCount
        {
            get
            {
                return poolDictonary.Count;
            }
        }

        private int objectCount = 0;

        public void AddObjects(T prefab, int count)
        {
            int key = prefab.GetInstanceID();
            if (!poolDictonary.ContainsKey(key))
            {
                poolDictonary.Add(key, new Queue<T>());
            }

            for (int i = 0; i < count; i++)
            {
                T newInstance = MonoBehaviour.Instantiate<T>(prefab);
                newInstance.gameObject.SetActive(false);

                poolDictonary[key].Enqueue(newInstance);
                objectCount++;
            }
        }

        public T GetObject(T prefab)
        {
            int key = prefab.GetInstanceID();

            if (!poolDictonary.ContainsKey(key))
            {
                Debug.Log("Prefab not in pool");
                AddObjects(prefab, 1);
            }

            if (poolDictonary[key].Count == 0)
            {
                Debug.Log("No instance");
                AddObjects(prefab, 1);
            }

            T instance = poolDictonary[key].Dequeue();
            instance.gameObject.SetActive(true);
            return instance;
        }

        public void ReturnObject(T objToReturn, T prefab)
        {
            int key = prefab.GetInstanceID();

            if (!poolDictonary.ContainsKey(key))
                throw new System.InvalidOperationException("Can't return object without register prefab");

            objToReturn.gameObject.SetActive(false);
            poolDictonary[key].Enqueue(objToReturn);

            objToReturn = null;
        }
    }
}