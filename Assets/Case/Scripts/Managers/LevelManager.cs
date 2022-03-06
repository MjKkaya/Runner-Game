using Runner.Obstacles;
using Runner.ScripableObjects;
using Runner.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Runner.Managers
{
    public class LevelManager : MonoBehaviour
    {
        private static readonly string LAST_COMPLETED_LEVEL_INDEX = "LCLI";

        public int ActiveLevelNumber{
            get{
                return mLastCompletedLevelIndex + 2;
            }
        }

        public Action ActionOnLevelCreated;
        public List<SOLevelDesign> Levels = new List<SOLevelDesign>();

        private int mLastCompletedLevelIndex = -1;
        private SOLevelDesign mCurrentLevelDesign;
        private int mLineHorizontalLength;


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

        public float GetFloorLeftBorder()
        {
            return mLineHorizontalLength * -0.5f;
        }

        public float GetFloorRightBorder()
        {
            return (mLineHorizontalLength * 0.5f);
        }


        private void SetupLevelEnvironments()
        {
            mCurrentLevelDesign = Levels[mLastCompletedLevelIndex + 1];
            mContainerOfObstacles.DestroyAllChildren();

            //Set Floor
            float newScaleX = mCurrentLevelDesign.CalculateLineHorizontalLength();
            float newScaleY = mCurrentLevelDesign.CalculateLineCount();
            mFloor.localScale = new Vector3(newScaleX, newScaleY, mFloor.localScale.z);
            float newPositionZ = (newScaleY * 0.5f) - 0.5f;
            mFloor.localPosition= new Vector3(mFloor.localPosition.x, mFloor.localPosition.y, newPositionZ);


            //Set Lines
            SetLinesProperties((int)newScaleY);
            CreateLinesObstacle();
            ActionOnLevelCreated?.Invoke();
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
            ObstacleTypes lineObstacleTypes;
            GameObject prefabObstacle;
            List<float> objectsPositionX;
            mLineHorizontalLength = mCurrentLevelDesign.CalculateLineHorizontalLength();
            int obstacleAmount;
            float obstacleRandomXPos;
            int obstacleAmountOfColumn;

            GameObject createdObstacleGO;
            Obstacle createdObstacleBottom;
            Obstacle createdObstacle2;

            foreach (int lineNumber in mLineNumbersToFill)
            {
                lineObstacleTypes = mCurrentLevelDesign.GetRandomLineObstacleType();
                prefabObstacle = mCurrentLevelDesign.GetRandomObstacle(lineObstacleTypes);
                Debug.Log($"LevelManager-CreateLinesObstacle-lineNumber:{lineNumber}, lineObstacleTpes:{lineObstacleTypes}-{prefabObstacle.name}");

                obstacleAmount = mCurrentLevelDesign.CalculateObstacleAmountOfLine();
                objectsPositionX = ResetXPositionsListOfLine(mLineHorizontalLength);
                for (int i = 0; i < obstacleAmount; i++)
                {
                    obstacleRandomXPos = objectsPositionX[UnityEngine.Random.Range(0, objectsPositionX.Count)];
                    objectsPositionX.Remove(obstacleRandomXPos);

                    createdObstacleGO = Instantiate(prefabObstacle, mContainerOfObstacles);
                    createdObstacleGO.name = CreateObstacleName(prefabObstacle.name, lineNumber, i, 0);
                    createdObstacleBottom = createdObstacleGO.GetComponent<Obstacle>();
                    createdObstacleBottom.SetPosition(new Vector3(obstacleRandomXPos, mFloor.localPosition.y, lineNumber));

                    obstacleAmountOfColumn = mCurrentLevelDesign.CalculateObstacleAmountOfColumn();
                    for (int k = 1; k < obstacleAmountOfColumn; k++)
                    {
                        createdObstacleGO = Instantiate(prefabObstacle, mContainerOfObstacles);
                        createdObstacleGO.name = CreateObstacleName(prefabObstacle.name, lineNumber, i, k);
                        createdObstacle2 = createdObstacleGO.GetComponent<Obstacle>();
                        createdObstacle2.SetPositionByBottomObject(createdObstacleBottom.transform);

                        createdObstacleBottom = createdObstacle2;
                    }
                }
            }
        }

        private List<float> ResetXPositionsListOfLine(int lineWidth)
        {
            List<float> xPositions = new List<float>();
            int stepCount = Mathf.FloorToInt(lineWidth * 0.5f);

            for (int i = stepCount*(-1) ; i < stepCount; i++)
            {
                xPositions.Add(i);
            }
            return xPositions;
        }

        private string CreateObstacleName(string prefabName, int lineNumber, int rowNumber ,int columnNumber)
        {
            return string.Concat(prefabName, "_", lineNumber, "_", rowNumber, "_", columnNumber);
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