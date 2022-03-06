using Case.Obstacles;
using Case.ScripableObjects;
using Case.Utilities;
using System.Collections.Generic;
using UnityEngine;


namespace Case.Managers
{
    public class LevelManager : MonoBehaviour
    {
        private static readonly string LAST_COMPLETED_LEVEL_INDEX = "LCLI";

        public int ActiveLevelNumber{
            get{
                return mLastCompletedLevelIndex + 2;
            }
        }

        public List<SOLevelDesign> Levels = new List<SOLevelDesign>();

        private int mLastCompletedLevelIndex = -1;
        private SOLevelDesign mCurrentLevelDesign;


        #region Level Environments

        [SerializeField] private Transform mFloor;
        [SerializeField] private Transform mContainerOfObstacles;
        [SerializeField] List<int> mLineNumbersToFill = new List<int>();

        #endregion


        public void Initiazlize()
        {
            GameManager.Instance.ActionOnGameStarted += OnGameStarted;
            mLastCompletedLevelIndex = GetLastCompletedLevelIndex(mLastCompletedLevelIndex);
            Debug.Log($"LevelManager-Initiazlize-mLastLevelIndex:{mLastCompletedLevelIndex}");
            mContainerOfObstacles.DestroyAllChildren();
        }


        private void SetupLevelEnvironments()
        {
            mCurrentLevelDesign = Levels[mLastCompletedLevelIndex + 1];
            mContainerOfObstacles.DestroyAllChildren();



            //Set Floor
            float newScaleY = mCurrentLevelDesign.CalculateLineCount();
            mFloor.localScale = new Vector3(mFloor.localScale.x, newScaleY, mFloor.localScale.z);
            float newPositionZ = (newScaleY * 0.5f) - 0.5f;
            mFloor.localPosition= new Vector3(mFloor.localPosition.x, mFloor.localPosition.y, newPositionZ);


            //Set Lines
            SetLinesProperties((int)newScaleY);
            CreateLinesObstacle();
        }

        private void SetLinesProperties(int lineLength)
        {
            mLineNumbersToFill.Clear();
            int beginingLinePoint = Mathf.CeilToInt(lineLength * 0.1f);
            int endLinePoint = lineLength - Mathf.CeilToInt(lineLength * 0.1f);
            Debug.Log($"LevelManager-SetLines-lineLength:{lineLength}, beginingLinePoint:{beginingLinePoint}, endLinePoint:{endLinePoint}");

            int emptySpaceBetweenTwoLines = mCurrentLevelDesign.CalculateEmptySpaceBetweenTwoLines();
            Debug.Log($"LevelManager-SetLines-emptySpaceBetweenTwoLines:{emptySpaceBetweenTwoLines},");

            int lineNumber = beginingLinePoint + Mathf.FloorToInt(emptySpaceBetweenTwoLines * 0.5f);

            do
            {
                mLineNumbersToFill.Add(lineNumber);
                Debug.Log($"LevelManager-SetLines-lineNumberstoFill:{mLineNumbersToFill.Count}, lineNumber:{lineNumber}");
                lineNumber += emptySpaceBetweenTwoLines;
            } while (lineNumber <= endLinePoint);
        }

        private void CreateLinesObstacle()
        {
            ObstacleTpes lineObstacleTpes;
            GameObject prefabObstacle;

            foreach (int lineNumber in mLineNumbersToFill)
            {
                lineObstacleTpes = mCurrentLevelDesign.GetRandomLineObstacleType();
                prefabObstacle = mCurrentLevelDesign.GetRandomObstacle(lineObstacleTpes);
                Debug.Log($"LevelManager-CreateLinesObstacle-lineNumber:{lineNumber}, lineObstacleTpes:{lineObstacleTpes}-{prefabObstacle.name}");

                int obstacleAmount = mCurrentLevelDesign.CalculateObstacleAmountOfLine();

                for (int i = 0; i < obstacleAmount; i++)
                {
                    GameObject createdObstacleGO = Instantiate(prefabObstacle, mContainerOfObstacles);
                    Obstacle createdObstacle = createdObstacleGO.GetComponent<Obstacle>();
                    createdObstacle.SetPosition(new Vector3(0, mFloor.localPosition.y, lineNumber));
                }
            }
        }



        #region Player Prefs

        private int GetLastCompletedLevelIndex(int defaultIndex)
        {
            return PlayerPrefs.GetInt(LAST_COMPLETED_LEVEL_INDEX, defaultIndex);
        }

        private void SaveLastCompletedLevelIndex(int indexNo)
        {
            PlayerPrefs.SetInt(LAST_COMPLETED_LEVEL_INDEX, indexNo);
            Debug.Log($"LevelManager-SaveLastLevelIndex-indexNo:{indexNo}");
        }

        #endregion


        #region Events

        private void OnGameStarted()
        {
            Debug.Log($"LevelManager-OnGameStarted");
            SetupLevelEnvironments();
        }

        #endregion
    }
}