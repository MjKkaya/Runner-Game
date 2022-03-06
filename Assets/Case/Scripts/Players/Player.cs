using UnityEngine;
using Case.Weapones;


namespace Players
{
    public class Player : MonoBehaviour
    {
        public Pistol Pistol;
        public float ShootingInterval = 0.05f;
        private float mElapsed = 0f;

        private bool mIsShootActive = false;


        private void Shoot()
        {
            mElapsed += Time.fixedDeltaTime;
            if (mElapsed >= ShootingInterval)
            {
                //Debug.Log("Shoot-a:" + Time.frameCount +","+ (Time.frameCount % ShootingInterval));
                //Debug.Log("Shoot-b:" + mElapsed);
                Pistol.Shoot();
                mElapsed = 0f;
            }
        }

        private void FixedUpdate()
        {
            if (mIsShootActive)
                Shoot();
        }
    }
}