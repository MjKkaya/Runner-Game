using Case.Utilities;
using UnityEngine;



namespace Case.Weapones
{
    [RequireComponent(typeof(ObjectPool))]
    public class Pistol : MonoBehaviour
    {
        public Transform ContainerOfBullet;

        private ObjectPool mBulletObjectPool;


        

        public void Shoot()
        {
            GameObject gameObjectOfBullet = mBulletObjectPool.GetPooledObjects();
            Bullet bullet = gameObjectOfBullet.GetComponent<Bullet>();
            bullet.Fire(Vector3.zero);
        }


        #region MonoBehaviour Events

        void Awake()
        {
            mBulletObjectPool = GetComponent<ObjectPool>();
            mBulletObjectPool.Initialize(ContainerOfBullet);
        }

        #endregion
    }
}