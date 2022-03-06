using UnityEngine;


namespace Runner.Obstacles
{
    [RequireComponent(typeof(Rigidbody))]
    public class NoneDestroyableObstacle : Obstacle
    {
        public override void Hit()
        {
            mRigidbody.AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 0.1f), 1f) * 2000);
            //GetComponent<Rigidbody>().AddForce(Vector3.forward * 500);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(ObstacleTypes.NoneDestroyableObstacle.ToString()))
            {
                Debug.Log("NoneDestroyableObstacle-OnTriggerEnter-:" + other.tag + "-" + other.gameObject.name);
                Hit();
            }
        }
    }
}