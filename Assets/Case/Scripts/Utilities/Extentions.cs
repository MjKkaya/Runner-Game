using System.Collections.Generic;
using UnityEngine;


namespace Runner.Utilities
{
    public static class Extentions
    {
        #region List

        public static void ToStringFull<T>(this List<T> list)
        {
            string fullLog = string.Empty;
            for (int i = 0; i < list.Count; i++)
            {
                fullLog += $"{i} : {list[i].ToString()} \n";
            }
            Debug.Log(fullLog);
        }

        #endregion


        #region Transforms

        public static void DestroyAllChildren(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(transform.GetChild(i).gameObject);
            }
        }

        #endregion
    }
}