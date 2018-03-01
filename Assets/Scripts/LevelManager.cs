using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class LevelManager : MonoBehaviour {

        public void LoadLevel(string name){
            Debug.Log ("New Level load: " + name);
            SceneManager.LoadScene(name);
        }

        public void QuitRequest(){
            Debug.Log ("Quit requested");
            Application.Quit ();
        }

    }
}
