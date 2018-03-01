using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        [Header("Controller")]
        public float speed;
        [Header("Clamping")]
        private float xMin;
        private float yMin;
        private float xMax;
        private float yMax;
        public float padding;

        #endregion

        #region Start & Update

        // Update is called once per frame
        private void Update()
        {
            //0- Used for live testing
            //DebugCode();

            //1- We clamp the movement space
            ClampSpace();
        
            //2- We call the player controller for movement
            Controller();
        }

        #endregion Start & Update

        #region Custom Methods
        //1- We clamp & pad the player movement space as per the "World Point"
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

        //3- We set the player movement controller
        private void Controller()
        {
            #region Variables
            //3a- We map the keys to key variables
            var leftKey = Input.GetKey(KeyCode.LeftArrow);
            var rightKey = Input.GetKey(KeyCode.RightArrow);
            var upKey = Input.GetKey(KeyCode.UpArrow);
            var downKey = Input.GetKey(KeyCode.DownArrow);
            //3b- We set the variables for player movement - deltatime for smooth movement
            var transLeft = Vector3.left * speed * Time.deltaTime;
            var transRight = Vector3.right * speed * Time.deltaTime;
            var transUp = Vector3.up * speed * Time.deltaTime;
            var transDown = Vector3.down * speed * Time.deltaTime;
            #endregion
            //3c- We map the key variables to the movement variables
            if (leftKey == true)
            {
                transform.position += transLeft;
            }
            else if (rightKey == true)
            {
                transform.position += transRight;
            }
            if (upKey == true)
            {
                transform.position += transUp;
            }
            else if (downKey == true)
            {
                transform.position += transDown;
            }

        }

        private void DebugCode()
        {
            //        var cameraDistance = Camera.main.transform.position.z;
            //        var zDistance = transform.position.z;
            //        var newZ = zDistance - cameraDistance;
            //        print("cameraDistance " + cameraDistance);
            //        print("zDistance " + zDistance);
            //        print("newZ " + newZ);
        }
        #endregion Custom Methods
    }
}



