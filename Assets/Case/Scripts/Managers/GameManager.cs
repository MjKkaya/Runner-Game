using System;
using UnityEngine;


namespace Case.Managers
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager mInstance;
        public static GameManager Instance
        {
            get
            {
                return mInstance;
            }
            private set
            {
                if (mInstance != null)
                    Destroy(mInstance.gameObject);
                mInstance = value;
            }
        }

        public Action ActionOnGameStarted;

        public GameObject Management;
        public LevelManager LevelManager;
        public UIManager UIManager;


        #region MonoBehaviour Events

        void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(Management);
            LevelManager.Initiazlize();
            UIManager.Initiazlize();

            ActionOnGameStarted += OnGameStarted;
        }

        #endregion


        #region Events

        private void OnGameStarted()
        {
            Debug.Log("GameManager-OnGameStarted!");
        }

        #endregion
    }
}
