using UnityEngine;
using Runner.Managers;
using Runner.Weapones;


namespace Runner.Players
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Pistol mPistol;
        [SerializeField] private PlayerMovement mPlayerMovement;

        public float ShootingInterval = 0.1f;
        private float mElapsed = 0f;

        private bool mIsShootActive = false;


        private void Shoot()
        {
            mElapsed += Time.fixedDeltaTime;
            if (mElapsed >= ShootingInterval)
            {
                //Debug.Log("Shoot-a:" + Time.frameCount +","+ (Time.frameCount % ShootingInterval));
                //Debug.Log("Shoot-b:" + mElapsed);
                mPistol.Shoot();
                mElapsed = 0f;
            }
        }


        #region MonoBehaviour Events

        private void Start()
        {
            GameManager.Instance.LevelManager.ActionOnLevelCreated += OnLevelCreated;
        }

        private void FixedUpdate()
        {
            //if(Input.GetKeyDown(KeyCode.F))
            if (mIsShootActive)
                Shoot();
        }

        #endregion


        #region Events

        private void OnLevelCreated()
        {
            mIsShootActive = true;
            mPlayerMovement.SetData(GameManager.Instance.LevelManager.GetFloorLeftBorder(), GameManager.Instance.LevelManager.GetFloorRightBorder());
        }

        #endregion
    }
}