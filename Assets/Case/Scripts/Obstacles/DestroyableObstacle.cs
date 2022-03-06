namespace Runner.Obstacles
{
    public class DestroyableObstacle : Obstacle
    {
        public override void Hit()
        {
            Destroy(gameObject);
        }
    }
}