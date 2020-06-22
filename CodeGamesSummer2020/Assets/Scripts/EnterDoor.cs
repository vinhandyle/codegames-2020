using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{
    public static bool inRange = false;
    public static string doorName = "";
    public static string sticky = ""; // used for interact text attachedto

    // Start is called before the first frame update
    void Start()
    {
        doorName = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown("w"))
        {
            if (doorName == "TestDoor")
            {
                StartCoroutine(SceneSwitch("Testing Area 2", "Testing Area"));
            }
            if (doorName == "TestDoor2")
            {
                StartCoroutine(SceneSwitch("Testing Area", "Testing Area 2"));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        inRange = true;
        sticky = doorName;
        InteractText.type = "door";
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        inRange = false;
        sticky = "";
        InteractText.type = "";
    }

    IEnumerator SceneSwitch(string load, string unload)
    {
        SceneManager.LoadScene(load, LoadSceneMode.Single);
        yield return null;
        SceneManager.UnloadSceneAsync(unload);
    }
}
