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
        // Adjacent ez, diagonal hard
        // Conveyor belt breaks velocity conservation
        if (GlobalControl.nextDoor == gameObject.name)
        {
            if (direction == "top")
            { // player starts below the opening
                Player.x = Player.x2;
                Player.y = gameObject.transform.position.y - 0.5f;
                Player.rb2D.velocity = new Vector2(Player.v_x, Player.v_y);
            }
            else if (direction == "bottom")
            { // player starts below the opening
                Player.x = Player.x2;
                Player.y = gameObject.transform.position.y + 0.5f;
                if (gameObject.name == "SG_4_to_SG_3")
                {
                    Player.y -= 0.3f;
                }
                else
                {
                    Player.rb2D.velocity = new Vector2(Player.v_x, Player.v_y);
                }
            }
            else if (direction == "left")
            { // players starts to the right of the opening
                Player.x = gameObject.transform.position.x + 0.5f;
                if (gameObject.name == "SG_9_to_SG_3")
                {
                    Debug.Log(0);
                    Player.y = gameObject.transform.position.y - gameObject.GetComponent<BoxCollider2D>().size.y / 2;
                }
                else
                {
                    Player.y = Player.y2;
                }
            }
            else if (direction == "right")
            { // player starts to the left of the opening
                Player.x = gameObject.transform.position.x - 0.5f;
                if (gameObject.name == "SG_3_to_SG_9")
                {
                    Player.y = gameObject.transform.position.y - gameObject.GetComponent<BoxCollider2D>().size.y / 2;
                }
                else
                {
                    Player.y = Player.y2;
                }
            }
            GlobalControl.switched = true;
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
                StartCoroutine(SceneSwitch("Testing Area 2", "Opening2")); // Testing Area, bottom-left
            }
            else if (opName == "Opening2")
            {
                StartCoroutine(SceneSwitch("Testing Area", "Opening")); // Testing Area 2, bottom-right
            }
            else if (opName == "Opening3")
            {
                StartCoroutine(SceneSwitch("Testing Area 2", "Opening4")); // Testing Area, bottom-right
            }
            else if (opName == "Opening4")
            {
                StartCoroutine(SceneSwitch("Testing Area", "Opening3")); // Testing Area 2, bottom-left
            }

            // In-Game Openings -> here_to_there

            /*-----IT (Start)-----*/

            //      Start_ to IT_1
            else if (opName == "Start_to_IT_1")
            {
                StartCoroutine(SceneSwitch("IT_1", "IT_1_to_Start_"));
            }
            else if (opName == "IT_1_to_Start_")
            {
                StartCoroutine(SceneSwitch("Start_", "Start_to_IT_1"));
            }

            //        Start_ to IT_1S
            else if (opName == "Start_to_IT_1S")
            {
                StartCoroutine(SceneSwitch("IT_1S", "IT_1S_to_Start_"));
            }
            else if (opName == "IT_1S_to_Start_")
            {
                StartCoroutine(SceneSwitch("Start_", "Start_to_IT_1S"));
            }

            //        IT_1 to IT_2
            else if (opName == "IT_1_to_IT_2")
            {
                StartCoroutine(SceneSwitch("IT_2", "IT_2_to_IT_1"));
            }
            else if (opName == "IT_2_to_IT_1")
            {
                StartCoroutine(SceneSwitch("IT_1", "IT_1_to_IT_2"));
            }


            /*-----IT (Return)-----*/

            /*-----DH-----*/

            //          DH_1 to DH_2

            else if (opName == "DH_1_to_DH_2")
            {
                StartCoroutine(SceneSwitch("DH_2", "DH_2_to_DH_1"));
                GlobalControl.counter_1 += 2;
            }

            //          DH_2 to DH_3
            else if (opName == "DH_2_to_DH_3")
            {
                StartCoroutine(SceneSwitch("DH_3", "DH_3_to_DH_2"));
            }
            else if (opName == "DH_3_to_DH_2")
            {
                StartCoroutine(SceneSwitch("DH_2", "DH_2_to_DH_3"));
            }

            //          DH_3 to DH_4
            else if (opName == "DH_3_to_DH_4")
            {
                StartCoroutine(SceneSwitch("DH_4", "DH_4_to_DH_3"));
            }
            else if (opName == "DH_4_to_DH_3")
            {
                StartCoroutine(SceneSwitch("DH_3", "DH_3_to_DH_4"));
            }

            //          DH_4 to DH_5
            else if (opName == "DH_4_to_DH_5")
            {
                StartCoroutine(SceneSwitch("DH_5", "DH_5_to_DH_4"));
            }
            else if (opName == "DH_5_to_DH_4")
            {
                StartCoroutine(SceneSwitch("DH_4", "DH_4_to_DH_5"));
            }

            //          DH_5 to DH_5S
            else if (opName == "DH_5_to_DH_5S")
            {
                StartCoroutine(SceneSwitch("DH_5S", "DH_5S_to_DH_5"));
            }
            else if (opName == "DH_5S_to_DH_5")
            {
                StartCoroutine(SceneSwitch("DH_5", "DH_5_to_DH_5S"));
            }

            //          DH_5 to DH_6
            else if (opName == "DH_5_to_DH_6")
            {
                StartCoroutine(SceneSwitch("DH_6", "DH_6_to_DH_5"));
            }
            else if (opName == "DH_6_to_DH_5")
            {
                StartCoroutine(SceneSwitch("DH_5", "DH_5_to_DH_6"));
            }

            //          DH_5 to DH_7
            else if (opName == "DH_5_to_DH_7")
            {
                StartCoroutine(SceneSwitch("DH_7", "DH_7_to_DH_5"));
            }
            else if (opName == "DH_7_to_DH_5")
            {
                StartCoroutine(SceneSwitch("DH_5", "DH_5_to_DH_7"));
            }

            //          DH_6 to DH_8
            else if (opName == "DH_6_to_DH_8")
            {
                StartCoroutine(SceneSwitch("DH_8", "DH_8_to_DH_6"));
            }
            else if (opName == "DH_8_to_DH_6")
            {
                StartCoroutine(SceneSwitch("DH_6", "DH_6_to_DH_8"));
            }

            /*-----SG-----*/

            //          SG_1 to SG_2
            else if (opName == "SG_1_to_SG_2")
            {
                StartCoroutine(SceneSwitch("SG_2", "SG_2_to_SG_1"));
            }
            else if (opName == "SG_2_to_SG_1")
            {
                StartCoroutine(SceneSwitch("SG_1", "SG_1_to_SG_2"));
            }

            //          SG_1 to SG_10
            else if (opName == "SG_1_to_SG_10")
            {
                StartCoroutine(SceneSwitch("SG_10", "SG_10_to_SG_1"));
            }
            else if (opName == "SG_10_to_SG_1")
            {
                StartCoroutine(SceneSwitch("SG_1", "SG_1_to_SG_10"));
            }

            //          SG_2 to SG_2S
            else if (opName == "SG_2_to_SG_2S")
            {
                StartCoroutine(SceneSwitch("SG_2S", "SG_2S_to_SG_2"));
            }
            else if (opName == "SG_2S_to_SG_2")
            {
                StartCoroutine(SceneSwitch("SG_2", "SG_2_to_SG_2S"));
            }

            //          SG_2 to SG_3
            else if (opName == "SG_2_to_SG_3")
            {
                StartCoroutine(SceneSwitch("SG_3", "SG_3_to_SG_2"));
            }
            else if (opName == "SG_3_to_SG_2")
            {
                StartCoroutine(SceneSwitch("SG_2", "SG_2_to_SG_3"));
            }

            //          SG_3 to SG_4
            else if (opName == "SG_3_to_SG_4")
            {
                StartCoroutine(SceneSwitch("SG_4", "SG_4_to_SG_3"));
            }
            else if (opName == "SG_4_to_SG_3")
            {
                StartCoroutine(SceneSwitch("SG_3", "SG_3_to_SG_4"));
            }

            //          SG_3 to SG_5
            else if (opName == "SG_3_to_SG_5")
            {
                StartCoroutine(SceneSwitch("SG_5", "SG_5_to_SG_3"));
            }
            else if (opName == "SG_5_to_SG_3")
            {
                StartCoroutine(SceneSwitch("SG_3", "SG_3_to_SG_5"));
            }

            //          SG_3 to SG_9
            else if (opName == "SG_3_to_SG_9")
            {
                StartCoroutine(SceneSwitch("SG_9", "SG_9_to_SG_3"));
            }
            else if (opName == "SG_9_to_SG_3")
            {
                StartCoroutine(SceneSwitch("SG_3", "SG_3_to_SG_9"));
            }

            //          SG_3 to SG_10
            else if (opName == "SG_3_to_SG_10")
            {
                StartCoroutine(SceneSwitch("SG_10", "SG_10_to_SG_3"));
            }
            else if (opName == "SG_10_to_SG_3")
            {
                StartCoroutine(SceneSwitch("SG_3", "SG_3_to_SG_10"));
            }

            //          SG_4 to SG_6
            else if (opName == "SG_4_to_SG_6")
            {
                StartCoroutine(SceneSwitch("SG_6", "SG_6_to_SG_4"));
            }
            else if (opName == "SG_6_to_SG_4")
            {
                StartCoroutine(SceneSwitch("SG_4", "SG_4_to_SG_6"));
            }

            //          SG_5 to SG_6
            else if (opName == "SG_5_to_SG_6")
            {
                StartCoroutine(SceneSwitch("SG_6", "SG_6_to_SG_5"));
            }
            else if (opName == "SG_6_to_SG_5")
            {
                StartCoroutine(SceneSwitch("SG_5", "SG_5_to_SG_6"));
            }

            //          SG_5 to SG_7
            else if (opName == "SG_5_to_SG_7")
            {
                StartCoroutine(SceneSwitch("SG_7", "SG_7_to_SG_5"));
            }
            else if (opName == "SG_7_to_SG_5")
            {
                StartCoroutine(SceneSwitch("SG_5", "SG_5_to_SG_7"));
            }

            //          SG_5 to SG_9
            else if (opName == "SG_5_to_SG_9")
            {
                StartCoroutine(SceneSwitch("SG_9", "SG_9_to_SG_5"));
            }
            else if (opName == "SG_9_to_SG_5")
            {
                StartCoroutine(SceneSwitch("SG_5", "SG_5_to_SG_9"));
            }

            //          SG_6 to SG_8
            else if (opName == "SG_6_to_SG_8")
            {
                StartCoroutine(SceneSwitch("SG_8", "SG_8_to_SG_6"));
            }
            else if (opName == "SG_8_to_SG_6")
            {
                StartCoroutine(SceneSwitch("SG_6", "SG_6_to_SG_8"));
            }


            //          SG_9 to SG_9S
            else if (opName == "SG_9_to_SG_9S")
            {
                StartCoroutine(SceneSwitch("SG_9S", "SG_9S_to_SG_9"));
            }
            else if (opName == "SG_9S_to_SG_9")
            {
                StartCoroutine(SceneSwitch("SG_9", "SG_9_to_SG_9S"));
            }

            //          SG_9 to SG_10
            else if (opName == "SG_9_to_SG_10")
            {
                StartCoroutine(SceneSwitch("SG_10", "SG_10_to_SG_9"));
            }
            else if (opName == "SG_10_to_SG_9")
            {
                StartCoroutine(SceneSwitch("SG_9", "SG_9_to_SG_10"));
            }

            //          SG_11 to SG_11S
            else if (opName == "SG_11_to_SG_11S")
            {
                StartCoroutine(SceneSwitch("SG_11S", "SG_11S_to_SG_11"));
            }
            else if (opName == "SG_11S_to_SG_11")
            {
                StartCoroutine(SceneSwitch("SG_11", "SG_11_to_SG_11S"));
            }

            //          SG_11 to SG_12
            else if (opName == "SG_11_to_SG_12")
            {
                StartCoroutine(SceneSwitch("SG_12", "SG_12_to_SG_11"));
            }
            else if (opName == "SG_12_to_SG_11")
            {
                StartCoroutine(SceneSwitch("SG_11", "SG_11_to_SG_12"));
            }

            /*-----TT-----*/

            /*-----MB-----*/

            /*-----GP-----*/

            /*-----FS-----*/

            /*-----Transition (IT-GP)-----*/

            // IT_2 to GP_1
            else if (opName == "IT_2_to_GP_1")
            {
                StartCoroutine(SceneSwitch("GP_1", "GP_1_to_IT_2"));
            }
            else if (opName == "GP_1_to_IT_2")
            {
                StartCoroutine(SceneSwitch("IT_2", "IT_2_to_GP_1"));
            }

            /*-----Transition (SG-TT)-----*/

            // SG_7 to TT_1
            else if (opName == "SG_7_to_TT_1")
            {
                StartCoroutine(SceneSwitch("TT_1", "TT_1_to_SG_7"));
            }
            else if (opName == "TT_1_to_SG_7")
            {
                StartCoroutine(SceneSwitch("SG_7", "SG_7_to_TT_1"));
            }

            /*-----Transition (TT-MB)-----*/

            /*-----Transition (GH-FS)-----*/

            on = false;
            opName = "";
            GlobalControl.immune = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            on = true;
            opName = gameObject.name;
        }       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            on = false;
            opName = "";
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
