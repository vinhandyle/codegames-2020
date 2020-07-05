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
        // Determines at which door the player will spawn if there are multiple doors in one scene
        if (GlobalControl.nextDoor == doorName)
        {
            Player.x = gameObject.transform.position.x;
            Player.y = gameObject.transform.position.y;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetKeyDown("w"))
        {
            if (doorName == "TestDoor")
            {
                StartCoroutine(SceneSwitch("Testing Area 2", "Testing Area", "TestDoor2"));
            }
            if (doorName == "TestDoor2")
            {
                StartCoroutine(SceneSwitch("Testing Area", "Testing Area 2", "TestDoor"));
            }
            inRange = false;
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

    IEnumerator SceneSwitch(string load, string unload, string nextDoor)
    { // Open door -> destroy player -> switch scene -> create player at door position
        GlobalControl.nextDoor = nextDoor;
        GlobalControl.area = load;
        GlobalControl.prevArea = unload;

        SceneManager.LoadScene(load, LoadSceneMode.Single);
        yield return null;
        SceneManager.UnloadSceneAsync(unload);
    }
}
