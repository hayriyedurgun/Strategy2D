using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts
{
    public abstract class ObjectPooler<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected List<T> m_ActiveItems;
        protected List<T> m_PooledItems;

        protected int m_Counter;

        private static ObjectPooler<T> m_Instance = null;
        public static ObjectPooler<T> Instance => m_Instance;

        public List<T> ObjectsToPool;

        public Transform ItemsParent;

        private void Awake()
        {
            m_Instance = this;
        }

        private void OnDestroy()
        {
            m_Instance = null;
        }

        public T GetObject()
        {
            if (!ObjectsToPool.Any()) throw new NotImplementedException("Object to pool list empty!");

            T temp;
            if (m_PooledItems.Any())
            {
                temp = m_PooledItems.First();

                m_ActiveItems.Add(temp);
                m_PooledItems.Remove(temp);
            }
            else
            {
                //for dynamic pooling
                m_Counter++;
                temp = Instantiate(ObjectsToPool[m_Counter % ObjectsToPool.Count]);
                temp.name = m_Counter.ToString();
                temp.transform.SetParent(ItemsParent);
                temp.transform.localScale = Vector3.one;

                m_ActiveItems.Add(temp);
            }

            return temp;
        }

        public virtual void ReleaseObject(T obj)
        {
            m_ActiveItems.Remove(obj);
            m_PooledItems.Add(obj);
        }

        public virtual void Init(int count)
        {
            if (!ObjectsToPool.Any()) return;

            m_PooledItems = new List<T>(count);
            m_ActiveItems = new List<T>(count);

            T temp;

            for (int i = 0; i < count; i++)
            {
                m_Counter++;
                temp = Instantiate(ObjectsToPool[m_Counter % ObjectsToPool.Count]);
                temp.name = m_Counter.ToString();
                temp.transform.SetParent(ItemsParent);
                temp.transform.localScale = Vector3.one;

                var pos = temp.transform.localPosition;
                pos.z = 0;
                temp.transform.localPosition = pos;

                m_PooledItems.Add(temp);
            }
        }
    }
}
