using Case.Obstacles;
using System.Collections.Generic;
using UnityEngine;


namespace Case.ScripableObjects
{
    [CreateAssetMenu(fileName = "LevelDesign_", menuName = "Levels/Create New Level Design", order = 1)]
    public class SOLevelDesign : ScriptableObject
    {
        #region Obstacles Properties

        [Header("Obstacles")]
        [SerializeField] private List<GameObject> DestroyableObstacles = new List<GameObject>();
        [SerializeField] private List<GameObject> NoneDestroyableObstacles = new List<GameObject>();

        //[Space(10)]
        //[Tooltip("Max X values is controlling at runtime.")]
        //public float ObstacleMinScaleX = 0.25f;
        //[Tooltip("Max Y values is controlling at runtime.")]
        //public float ObstacleMinScaleY = 0.25f;

        #endregion


        #region Line Properties

        [Header("Line Properties")]
        [SerializeField] private int MinLineCount = 10;
        [SerializeField] private int MaxLineCount = 100;

        [Space(10)]
        [Range(1, 10)] [SerializeField] private int MinEmptySpaceBetweenTwoLines = 5;
        [Range(1, 10)] [SerializeField] private int MaxEmptySpaceBetweenTwoLines = 10;

        [Space(10)]
        [Range(1,6)] [SerializeField] private int MaxObstacleAmountOfLine = 3;
        private int mMinObstacleAmountOfLine = 1;
        
        [Space(10)]
        [Range(1,3)] [SerializeField] private int MaxObstacleAmountOfColumn = 1;
        private int mMinObstacleAmountOfColumn = 1;

        #endregion


        #region Calculations

        public int CalculateLineCount()
        {
            return Random.Range(MinLineCount, MaxLineCount + 1);
        }

        public int CalculateEmptySpaceBetweenTwoLines()
        {
            return Random.Range(MinEmptySpaceBetweenTwoLines, MaxEmptySpaceBetweenTwoLines + 1);
        }

        public int CalculateObstacleAmountOfLine()
        {
            return Random.Range(mMinObstacleAmountOfLine, MaxObstacleAmountOfLine + 1);
        }

        public int CalculateObstacleAmountOfColumn()
        {
            return Random.Range(mMinObstacleAmountOfColumn, MaxObstacleAmountOfColumn + 1);
        }

        #endregion


        #region Get Random Objects

        public ObstacleTpes GetRandomLineObstacleType()
        {
            int randomInt = Random.Range(0, (int)ObstacleTpes.Max);
            return (ObstacleTpes)randomInt;
        }

        public GameObject GetRandomObstacle(ObstacleTpes obstacleTpes)
        {
            GameObject randomGameObject;
            int randomIndex;

            if (obstacleTpes.Equals(ObstacleTpes.DestroyableObstacle))
            {
                randomIndex = Random.Range(0, DestroyableObstacles.Count);
                randomGameObject = DestroyableObstacles[randomIndex];
            }
            else
            {
                randomIndex = Random.Range(0, NoneDestroyableObstacles.Count);
                randomGameObject = NoneDestroyableObstacles[randomIndex];
            }

            return randomGameObject;
        }

        #endregion
    }
}