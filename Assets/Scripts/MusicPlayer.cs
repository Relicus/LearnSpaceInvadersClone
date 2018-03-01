using UnityEngine;

namespace Assets.Scripts
{
    public class MusicPlayer : MonoBehaviour {
        static MusicPlayer instance = null;
	
        private void Start () {
            if (instance != null && instance != this) {
                Destroy (gameObject);
                print ("Duplicate music player self-destructing!");
            } else {
                instance = this;
                GameObject.DontDestroyOnLoad(gameObject);
            }
		
        }
    }
}
