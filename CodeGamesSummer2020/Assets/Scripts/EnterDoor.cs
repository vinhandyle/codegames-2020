using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{
    public static bool inRange = false;
    public static string doorName = ""; // Prevents interacting with multiple doors in one scene
    public static string sticky = ""; // used for interact text attachedto

    // Start is called before the first frame update
    void Start()
    {
        // Determines at which door the player will spawn if there are multiple doors in one scene
        if (GlobalControl.nextDoor == gameObject.name)
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
            // Test Doors
            if (doorName == "TestDoor")
            {
                StartCoroutine(SceneSwitch("Testing Area 2", "TestDoor2")); // Testing Area, top-center
            }
            else if (doorName == "TestDoor2")
            {
                StartCoroutine(SceneSwitch("Testing Area",  "TestDoor")); // Testing Area 2, bottom-center
            }

            // In-Game Doors

            inRange = false;
            GlobalControl.immune = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            inRange = true;
            doorName = gameObject.name;
            sticky = gameObject.name;
            InteractText.type = "door";
        }       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            inRange = false;
            doorName = "";
            sticky = "";
            InteractText.type = "";
        }        
    }

    IEnumerator SceneSwitch(string load, string nextDoor)
    { 
        GlobalControl.nextDoor = nextDoor;
        GlobalControl.area = load;

        SceneManager.LoadScene(load, LoadSceneMode.Single);
        yield return null;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
    }
}
