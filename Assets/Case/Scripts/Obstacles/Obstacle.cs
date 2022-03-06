using UnityEngine;


namespace Runner.Obstacles
{
    public enum ObstacleTypes
    {
        DestroyableObstacle = 0,
        NoneDestroyableObstacle = 1,

        Max = 2
    }


    public abstract class Obstacle : MonoBehaviour
    {
        protected Rigidbody mRigidbody;

        public void SetPosition(Vector3 newPos)
        {
            float centerBoundsX = gameObject.GetComponent<Renderer>().bounds.center.x;
            float centerBoundsY = gameObject.GetComponent<Renderer>().bounds.center.y;

            float gapX = (transform.localScale.x * 0.5f) - centerBoundsX;
            float gapY = (transform.localScale.y * 0.5f) - centerBoundsY;

            Debug.Log($"name:{name}-bounds-gapX:{gapX}, gapY:{gapY}");
            transform.localPosition = new Vector3(newPos.x + gapX, newPos.y + gapY, newPos.z);
        }

        public void SetPositionByBottomObject(Transform bottomObjectTransform)
        {
            Vector3 newPos = bottomObjectTransform.localPosition;
            float objectHeight = bottomObjectTransform.localScale.y;
            transform.localPosition = new Vector3(newPos.x, newPos.y + objectHeight, newPos.z);
        }

        public abstract void Hit();

        private void Awake()
        {
            mRigidbody = GetComponent<Rigidbody>();
        }
    }
}