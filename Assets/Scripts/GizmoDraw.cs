using UnityEngine;

namespace Assets.Scripts
{
    public class GizmoDraw : MonoBehaviour
    {
        public float radius;
        private void OnDrawGizmos()
        {
            {
                Gizmos.DrawWireSphere(transform.position, radius);
            }
        }
    }
}
