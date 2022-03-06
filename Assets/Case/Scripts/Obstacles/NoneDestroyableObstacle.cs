using UnityEngine;


namespace Case.Obstacles
{
    [RequireComponent(typeof(Rigidbody))]
    public class NoneDestroyableObstacle : Obstacle
    {
        public override void Hit()
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1, 1), Random.Range(0f, 0.1f), 1f) * 500);
            //GetComponent<Rigidbody>().AddForce(Vector3.forward * 500);
        }
    }
}