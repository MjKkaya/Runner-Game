using UnityEngine;


namespace Case.Obstacles
{
    public enum ObstacleTpes
    {
        DestroyableObstacle = 0,
        NoneDestroyableObstacle = 1,

        Max = 2
    }


    public abstract class Obstacle : MonoBehaviour
    {
        public void SetPosition(Vector3 newPos)
        {
            float centerBoundsX = gameObject.GetComponent<Renderer>().bounds.center.x;
            float centerBoundsY = gameObject.GetComponent<Renderer>().bounds.center.y;

            float gapX = (transform.localScale.x * 0.5f) - centerBoundsX;
            float gapY = (transform.localScale.y * 0.5f) - centerBoundsY;

            Debug.Log($"name:{name}-bounds-gapX:{gapX}, gapY:{gapY}");
            transform.localPosition = new Vector3(newPos.x + gapX, newPos.y + gapY, newPos.z);
        }

        public abstract void Hit();
    }
}