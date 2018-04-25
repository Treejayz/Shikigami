using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

    public static string scene;
    public Text loadingText;
    static LevelLoader instance;

    // Use this for initialization

    void Start () {
        print(scene);
        Time.timeScale = 1f;
        StartCoroutine(LoadNewScene(scene));
        StartCoroutine(LoadText());
        print(scene);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator LoadText() {

        string[] things = { "Loading<color=#00000000>...</color>", "Loading.<color=#00000000>..</color>", "Loading..<color=#00000000>.</color>", "Loading..."};
        int i = 0;
        while(true)
        {
            loadingText.text = things[i];
            if (i == 3)
            {
                yield return new WaitForSeconds(0.6f);
                i = 0;
            } else
            {
                yield return new WaitForSeconds(0.2f);
                i += 1;
            }
            
        }

    }

    IEnumerator LoadNewScene(string newscene)
    {

        // This line waits for 3 seconds before executing the next line in the coroutine.
        // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
        

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = SceneManager.LoadSceneAsync(newscene);
        yield return new WaitForSeconds(1f);
        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone)
        {
            AkSoundEngine.SetRTPCValue("LoadProgress", Mathf.Clamp01(1 - async.progress/0.9f));
            Debug.Log(Mathf.Clamp01(1 - async.progress / 0.9f));
            yield return null;
        }

    }

    void OnDisable() {
        AkSoundEngine.ResetRTPCValue("LoadProgress");
    }
}
