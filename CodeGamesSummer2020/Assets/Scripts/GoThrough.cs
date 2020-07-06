using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoThrough : MonoBehaviour
{
    public string direction; // Where on the screen the opening is located
    public static string opName = ""; // Prevents interacting with multiple openings in a scene
    public static bool on = false; // Whether the player is on the opening


    // Start is called before the first frame update
    void Start()
    {
        // Determines at which door the player will spawn if there are multiple doors in one scene
        if (GlobalControl.nextDoor == gameObject.name)
        {
            if (direction == "top")
            { // player starts below the opening
                Player.x = gameObject.transform.position.x;
                Player.y = gameObject.transform.position.y - 0.5f;
            }
            else if (direction == "bottom-left")
            { // player starts above and to the right of the opening
                Player.x = gameObject.transform.position.x + 0.5f;
                Player.y = gameObject.transform.position.y + 0.5f;
            }
            else if (direction == "bottom-right")
            { // player starts above and to the left of the opening
                Player.x = gameObject.transform.position.x - 0.5f;
                Player.y = gameObject.transform.position.y + 0.5f;
            }
            else if (direction == "left")
            { // players starts to the right of the opening
                Player.x = gameObject.transform.position.x + 0.5f;
                Player.y = gameObject.transform.position.y;
            }
            else if (direction == "right")
            { // player starts to the left of the opening
                Player.x = gameObject.transform.position.x - 0.5f;
                Player.y = gameObject.transform.position.y;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
            // Test Openings
            if (opName == "Opening")
            {
                StartCoroutine(SceneSwitch("Testing Area 2", "Testing Area", "Opening2"));
            }
            else if (opName == "Opening2")
            {
                StartCoroutine(SceneSwitch("Testing Area", "Testing Area 2", "Opening"));
            }
            else if (opName == "Opening3")
            {
                StartCoroutine(SceneSwitch("Testing Area 2", "Testing Area", "Opening4"));
            }
            else if (opName == "Opening4")
            {
                StartCoroutine(SceneSwitch("Testing Area", "Testing Area 2", "Opening3"));
            }

            // In-Game Openings

            on = false;
            opName = "";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        on = true;
        opName = gameObject.name;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        on = false;
        opName = "";
    }

    IEnumerator SceneSwitch(string load, string unload, string nextDoor)
    {
        GlobalControl.nextDoor = nextDoor;
        GlobalControl.area = load;
        GlobalControl.prevArea = unload;

        SceneManager.LoadScene(load, LoadSceneMode.Single);
        yield return null;
        SceneManager.UnloadSceneAsync(unload);
    }
}
