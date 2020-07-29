using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{
    public Sprite lockedDoor;
    public Sprite unlockedDoor;

    public static bool inRange = false;
    public static bool locked = false;
    public static bool triggeredOnce = false;

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
            GlobalControl.switched = true;
        }

        if (gameObject.name == "Start_to_IT_X")
        {
            if (GlobalControl.locked_1)
            {
                locked = true;
                gameObject.GetComponent<SpriteRenderer>().sprite = lockedDoor;
            }
            else
            {
                locked = false;
                gameObject.GetComponent<SpriteRenderer>().sprite = unlockedDoor;
            }
        }
        else if (gameObject.name == "GP_1_to_GP_2")
        {
            if (GlobalControl.locked_2)
            {
                locked = true;
                gameObject.GetComponent<SpriteRenderer>().sprite = lockedDoor;
            }
            else
            {
                locked = false;
                gameObject.GetComponent<SpriteRenderer>().sprite = unlockedDoor;
            }
        }
        else if (gameObject.name == "DH_4_to_DH_6")
        {
            if (GlobalControl.locked_3)
            {
                locked = true;
                gameObject.GetComponent<SpriteRenderer>().sprite = lockedDoor;
            }
            else
            {
                locked = false;
                gameObject.GetComponent<SpriteRenderer>().sprite = unlockedDoor;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Open Door
        if (inRange && !locked && Input.GetKeyDown("w"))
        {
            // Test Doors
            if (doorName == "TestDoor")
            {
                StartCoroutine(SceneSwitch("Testing Area 2", "TestDoor2")); // Testing Area, top-center
            }
            else if (doorName == "TestDoor2")
            {
                StartCoroutine(SceneSwitch("Testing Area", "TestDoor")); // Testing Area 2, bottom-center
            }

            // In-Game Doors

            // Birthplace to Origin
            else if (doorName == "Start_to_IT_X")
            {
                StartCoroutine(SceneSwitch("IT_X", "IT_X_to_Start_"));
            }

            // Palace Entrance to The Lift to Heaven
            else if (doorName == "GP_1_to_GP_2")
            {
                StartCoroutine(SceneSwitch("GP_2", "GP_2_to_GP_1"));
                GlobalControl.locked_2 = true;
            }

            // Waste Deposit to Encampment
            else if (doorName == "DH_4_to_DH_6")
            {
                StartCoroutine(SceneSwitch("DH_6", "DH_6_to_DH_4"));
            }
            else if (doorName == "DH_6_to_DH_4")
            {
                StartCoroutine(SceneSwitch("DH_4", "DH_4_to_DH_6"));
                GlobalControl.locked_3 = false;
            }

            // Injection Point to Disposal Area
            else if (doorName == "DH_8_to_SG_1")
            {
                StartCoroutine(SceneSwitch("SG_1", "SG_1_to_DH_8"));
            }
            else if (doorName == "SG_1_to_DH_8")
            {
                StartCoroutine(SceneSwitch("DH_8", "DH_8_to_SG_1"));
                GlobalControl.locked_3 = false;
            }

            // Mysterious Path to The Lift from Hell
            else if (doorName == "DH_7_to_IT_3")
            {
                StartCoroutine(SceneSwitch("IT_3", "IT_3_to_X"));
            }

            inRange = false;
            GlobalControl.immune = false;
        }

        // Other
        if(doorName == "GP_2_to_GP_1" && !triggeredOnce)
        { // The Lift to Heaven
            StartCoroutine(waitThenExecute(2f, SceneSwitch("GP_0A", "GP_0A_to_GP_2")));
        }
        if (doorName == "GP_0A_to_GP_2")
        { // Audience Chamber (First)
            if (GlobalControl.counter_1 == 3 || GlobalControl.counter_1 == 4)
            {
                StartCoroutine(waitThenExecute(2f, SceneSwitch("DH_1", "DH_1_to_GP_0A")));
                if (GlobalControl.counter_1 == 4)
                {
                    GlobalControl.healthCurr = 1;
                }
                GlobalControl.energyCurr = 0;
            }
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

    IEnumerator waitThenExecute(float time, IEnumerator action)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(action);
        triggeredOnce = true;
    }
}
