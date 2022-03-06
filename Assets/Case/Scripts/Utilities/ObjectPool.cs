using System.Collections.Generic;
using UnityEngine;


namespace Case.Utilities
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject PrefabPooledObject;
        [SerializeField] private int MinAmountToPool = 0;
        [SerializeField] private int MaxAmountToPool = 10;
        [SerializeField] private List<GameObject> mPooledObjects;

        private bool mIsReducePooledListInvokeActive;
        private Transform mTrasnformOfParent;


        public void Initialize(Transform parentObject)
        {
            mPooledObjects = new List<GameObject>();
            Debug.Log($"ObjectPool-Initialize-MinAmountToPool{MinAmountToPool},MaxAmountToPool:{MaxAmountToPool}");
            if (MinAmountToPool <= 0)
                return;
            mTrasnformOfParent = parentObject;

            for (int i = 0; i < MinAmountToPool; i++)
            {
                AddPooledObjects();
            }

            Debug.Log($"ObjectPool-Initialize-Created Pooled Objects:{mPooledObjects.Count}");
        }

        public GameObject GetPooledObjects()
        {
            GameObject pooledObject = null;

            for (int i = 0; i < mPooledObjects.Count; i++)
            {
                if (!mPooledObjects[i].activeInHierarchy)
                {
                    pooledObject = mPooledObjects[i];
                    break;
                }
            }

            if (pooledObject == null)
            {
                AddPooledObjects();
                pooledObject = mPooledObjects[mPooledObjects.Count - 1];
            }

            if (mPooledObjects.Count >= MaxAmountToPool && !mIsReducePooledListInvokeActive)
            {
                Invoke(nameof(ReducePooledListAamount), 3f);
                mIsReducePooledListInvokeActive = true;
            }

            return pooledObject;
        }


        private void AddPooledObjects()
        {
            GameObject pooledObject = Instantiate(PrefabPooledObject, mTrasnformOfParent);
            pooledObject.transform.localPosition = Vector3.zero;
            pooledObject.SetActive(false);
            mPooledObjects.Add(pooledObject);
            pooledObject.name = $"PooledObject_{mPooledObjects.Count}";
        }

        private void ReducePooledListAamount()
        {
            Debug.Log($"ObjectPool-ReducePooledListAamount-mPooledObjects:{mPooledObjects.Count}, MaxAmountToPool:{MaxAmountToPool}");
            if (mPooledObjects.Count >= MaxAmountToPool)
            {
                for (int i = mPooledObjects.Count - 1; i >= MaxAmountToPool; i--)
                {
                    if (!mPooledObjects[i].activeInHierarchy)
                    {
                        Destroy(mPooledObjects[i]);
                        mPooledObjects.RemoveAt(i);
                    }
                }
            }
            mIsReducePooledListInvokeActive = false;
        }
    }
}