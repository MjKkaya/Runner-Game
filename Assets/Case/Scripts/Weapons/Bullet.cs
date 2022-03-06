using Runner.Obstacles;
using UnityEngine;


namespace Runner.Weapones
{
    //[RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        public int Speed = 70;
        public float DestroyTime = 5;

        //private Rigidbody rigidbody;
        private Vector3 basePoint;


        public void Fire(Vector3 firePoint)
        {
            basePoint = firePoint;
            transform.localPosition = firePoint;
            gameObject.SetActive(true);

            //rigidbody.velocity = transform.forward * Speed;
            //transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }

        private void Disable()
        {
            CancelInvoke();
            //rigidbody.velocity = Vector3.zero;
            //rigidbody.angularVelocity = Vector3.zero;
            //rigidbody.Sleep();
            transform.localPosition = basePoint;
            gameObject.SetActive(false);
        }


        #region MonoBehaviour Events

        //private void Awake()
        //{
        //    rigidbody = GetComponent<Rigidbody>();
        //}

        private void OnEnable()
        {
            Invoke(nameof(Disable), DestroyTime);
        }

        private void FixedUpdate()
        {
            transform.Translate(Vector3.forward * Speed * Time.fixedDeltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(ObstacleTypes.DestroyableObstacle.ToString()))
            {
                Debug.Log("Bullet-OnTriggerEnter-DestroyableObstacle:" + other.tag + "-" + other.gameObject.name);
                Disable();
                other.gameObject.GetComponent<Obstacle>().Hit();
            }
            else if (other.CompareTag(ObstacleTypes.NoneDestroyableObstacle.ToString()))
            {
                Debug.Log("Bullet-OnTriggerEnter-NoneDestroyableObstacle:" + other.tag + "-" + other.gameObject.name);
                Disable();
                other.gameObject.GetComponent<Obstacle>().Hit();
            }
        }
        #endregion
    }
}