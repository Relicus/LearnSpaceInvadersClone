using UnityEngine;

namespace Assets.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {

        // Public Variables
        public GameObject enemyPrefab;
        [Header("Variables")]
        public float formWidth;
        public float formHeight;
        [Header("Formation Movement")]
        public bool moveFormation; //Todo: Change this to private later
        public float speed;
        public float padding;
        // Private Variables
        private float leftOrRight;
        private float xMin;
        private float xMax;
        private float yMin;
        private float yMax;

        #region Start & Update
        private void Start()
        {
            #region SpawnEnemies & Move Formation
            //1- We spawn an enemy for each child in EnemySpawner parent
            SpawnEnemyClones();

            //2- Clamp the space for enemy formation movement
            ClampSpace();

            //3- We call this method to set the speed + or - for direction randomly
            SelectLeftOrRight();
            #endregion SpawnEnemies & Move Formation
        }

        private void Update()
        {
            //1- We call the "Move Enemy Formation Code" Region
            MoveFormation();
        }
        #endregion Start & Update


        #region Custom Methods
        //--------------------------\\

        //1a- We spawn an enemy for each child in EnemySpawner parent
        private void SpawnEnemyClones()
        {
            foreach (Transform child in transform)
            {
                var enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity);
                enemy.transform.parent = child;
            }
        }

        //1b- We clamp using variables from Player Controller
        public void ClampSpace()
        {
            var zClamp = transform.position.z - Camera.main.transform.position.z;
            var leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, zClamp));
            var rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, zClamp));

            xMin = leftmost.x + padding;
            xMax = rightmost.x - padding;
            yMin = leftmost.y + padding;
            yMax = rightmost.y - padding;

            var newX = Mathf.Clamp(transform.position.x, xMin, xMax);
            var newY = Mathf.Clamp(transform.position.y, yMin, yMax);
            transform.position = new Vector3(newX, newY);
        }

        #region Move Enemy Formation Code
        //2- We draw cube region for enemy formation
        public void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(formWidth, formHeight));
        }

        //3- We randomize + or - variable for speed calculation for direction
        private void SelectLeftOrRight()
        {
            if (Random.value < 0.5f)
            {
                leftOrRight = -1;
                print("Formation Moving Left");
            }
            else
            {
                leftOrRight = +1;
                print("Formation Moving Right");
            }

            speed = speed * leftOrRight;

        }

        /*4a- We move enemy formation as per the "Speed" and reverse it when it hits the edges 
              of set boundaries*/
        private void MoveFormation()
        {
            if (moveFormation)
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0);
            }
            else
            {
                transform.position += new Vector3();
            }

            //3b- (Private Method Call) We reverse movement if hits the edge
            ReverseFormation();
        }

        //4b- We reverse movement if hits the edge
        private void ReverseFormation()
        {
            //i- We define the colliders of the cube gizmo formation
            var rightEdge = transform.position.x + (0.5 * formWidth);
            var leftEdge = transform.position.x - (0.5 * formWidth);

            /*ii- We seperate these so that movement doesn't break at high speed then we multiply 
                  by "-1" to reverse the movement*/
            if (leftEdge < xMin)
            {
                speed = speed * -1;
                Debug.Log("LeftEdge Hit");
            }

            else if (rightEdge > xMax)
            {
                speed = speed * -1;
                Debug.Log("RightEdge Hit");
            }
        }

        #endregion Move Enemy Formation Code

        #endregion Custom Methods
    }
}