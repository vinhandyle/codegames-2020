using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{
    public Sprite lockedDoor;
    public Sprite unlockedDoor;

    public bool inRange = false;
    public bool locked = false;
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
            Player.y = gameObject.transform.position.y - gameObject.GetComponent<BoxCollider2D>().size.y / 2;
            GlobalControl.switched = true;
        }

        locked = false;
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
        else if (gameObject.name == "SG_10_to_SG_3 (D)")
        {
            if (GlobalControl.locked_4)
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
        else if (gameObject.name == "MB_3_to_MB_12")
        {
            if (GlobalControl.locked_5)
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
        else if (gameObject.name == "MB_12_to_MB_3")
        {
            if (GlobalControl.locked_6)
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
            }

            // Lower Prep Area to Backdoor
            else if (doorName == "SG_3_to_SG_10 (D)")
            {
                StartCoroutine(SceneSwitch("SG_10", "SG_10_to_SG_3 (D)"));
                GlobalControl.locked_4 = false;
            }
            else if (doorName == "SG_10_to_SG_3 (D)")
            {
                StartCoroutine(SceneSwitch("SG_3", "SG_3_to_SG_10 (D)"));
            }

            // Storage Area to Overpass
            else if (doorName == "SG_9_to_SG_11")
            {
                StartCoroutine(SceneSwitch("SG_11", "SG_11_to_SG_9"));
            }
            else if (doorName == "SG_11_to_SG_9")
            {
                StartCoroutine(SceneSwitch("SG_9", "SG_9_to_SG_11"));
            }

            // Central Plaza to City Intersection
            else if (doorName == "TT_2_to_TT_4")
            {
                StartCoroutine(SceneSwitch("TT_4", "TT_4_to_TT_2"));
            }
            else if (doorName == "TT_4_to_TT_2")
            {
                StartCoroutine(SceneSwitch("TT_2", "TT_2_to_TT_4"));
            }

            // Central Plaza to Main Station
            else if (doorName == "TT_2_to_TT_13")
            {
                StartCoroutine(SceneSwitch("TT_13", "TT_13_to_TT_2"));
            }

            // Secluded Path to Dead End
            else if (doorName == "TT_3_to_TT_6")
            {
                StartCoroutine(SceneSwitch("TT_6", "TT_6_to_TT_3"));
                GlobalControl.block_TT_6 = false;
            }
            else if (doorName == "TT_6_to_TT_3")
            {
                StartCoroutine(SceneSwitch("TT_3", "TT_3_to_TT_6"));
            }

            // Residential Section to Azimuth Hall
            else if (doorName == "TT_5_to_TT_8")
            {
                StartCoroutine(SceneSwitch("TT_8", "TT_8_to_TT_5"));
            }

            // City Intersection to Alleyway
            else if (doorName == "TT_4_to_TT_7 (A)")
            {
                StartCoroutine(SceneSwitch("TT_7", "TT_7_to_TT_4 (A)"));
            }
            else if (doorName == "TT_4_to_TT_7 (B)")
            {
                StartCoroutine(SceneSwitch("TT_7", "TT_7_to_TT_4 (B)"));
            }
            else if (doorName == "TT_7_to_TT_4 (A)")
            {
                StartCoroutine(SceneSwitch("TT_4", "TT_4_to_TT_7 (A)"));
            }
            else if (doorName == "TT_7_to_TT_4 (B)")
            {
                StartCoroutine(SceneSwitch("TT_4", "TT_4_to_TT_7 (B)"));
            }

            // 
            else if (doorName == "TT_10_to_TT_9")
            {
                StartCoroutine(SceneSwitch("TT_9", "TT_9_to_TT_10"));
            }

            // Stations to Vacuum Pod
            else if (doorName == "TT_13_to_TT_14")
            {
                StartCoroutine(SceneSwitch("TT_14", "TT_14_to_TT_1X"));
            }
            else if (doorName == "TT_14S_to_TT_14")
            {
                StartCoroutine(SceneSwitch("TT_14", "TT_14_to_TT_1X"));
            }
            else if (doorName == "TT_15_to_TT_14")
            {
                StartCoroutine(SceneSwitch("TT_14", "TT_14_to_TT_1X"));
            }
            else if (doorName == "TT_14_to_TT_1X")
            {
                if (GlobalControl.pod_location == "main")
                {
                    StartCoroutine(SceneSwitch("TT_13", "TT_13_to_TT_14"));
                }
                else if (GlobalControl.pod_location == "far")
                {
                    StartCoroutine(SceneSwitch("TT_14S", "TT_14S_to_TT_14"));
                }
                else if (GlobalControl.pod_location == "seaside")
                {
                    StartCoroutine(SceneSwitch("TT_15", "TT_15_to_TT_14"));
                }
            }

            // City Outskirts to Seaside Station
            else if (doorName == "TT_16_to_TT_15")
            {
                StartCoroutine(SceneSwitch("TT_15", "TT_15_to_TT_16"));
            }

            // Seaside Station to City Outskirts
            else if (doorName == "TT_15_to_TT_16")
            {
                StartCoroutine(SceneSwitch("TT_16", "TT_16_to_TT_15"));
            }

            // Hull to Northern Arm
            else if (doorName == "MB_3_to_MB_5")
            {
                StartCoroutine(SceneSwitch("MB_5", "MB_5_to_MB_3"));
                GlobalControl.block_TT_6 = false;
            }
            else if (doorName == "MB_5_to_MB_3")
            {
                StartCoroutine(SceneSwitch("MB_3", "MB_3_to_MB_5"));
            }

            // Hull to Eastern Arm
            else if (doorName == "MB_3_to_MB_6")
            {
                StartCoroutine(SceneSwitch("MB_6", "MB_6_to_MB_3"));
                GlobalControl.block_TT_6 = false;
            }
            else if (doorName == "MB_6_to_MB_3")
            {
                StartCoroutine(SceneSwitch("MB_3", "MB_3_to_MB_6"));
            }

            // Hull to Heart of the Beast
            else if (doorName == "MB_3_to_MB_12")
            {
                StartCoroutine(SceneSwitch("MB_12", "MB_12_to_MB_3"));
                GlobalControl.block_TT_6 = false;
            }
            else if (doorName == "MB_12_to_MB_3")
            {
                StartCoroutine(SceneSwitch("MB_3", "MB_3_to_MB_12"));
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

            if (InteractText.notif)
            {
                InteractText.type = "";
                InteractText.stickied = "";
                InteractText.stickied2 = "";
                InteractText.interacted = false;
                InteractText.notif = false;
                InteractText.triggerOnce = false;
            }

            doorName = gameObject.name;
            sticky = gameObject.name;
            InteractText.type = "door";
            InteractText.locked = locked;
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
