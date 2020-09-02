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
                if (gameObject.name == "TT_13_to_TT_2" || gameObject.name == "TT_15_to_TT_16" || gameObject.name == "MB_3_to_MB_2")
                {
                    Player.x = gameObject.transform.position.x;
                }
                Player.y = gameObject.transform.position.y - 0.5f;
                Player.rb2D.velocity = new Vector2(Player.v_x, Player.v_y);
            }
            else if (direction == "bottom")
            { // player starts below the opening
                Player.x = Player.x2;
                Player.y = gameObject.transform.position.y + 0.5f;
                if (gameObject.name == "SG_4_to_SG_3")
                {
                    Player.x += 0.3f;
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
                    Player.y = gameObject.transform.position.y - gameObject.GetComponent<BoxCollider2D>().size.y / 2;
                }
                else
                {
                    Player.y = Player.y2;
                }
                Player.rb2D.velocity = new Vector2(Player.v_x, Player.v_y);
            }
            else if (direction == "right")
            { // player starts to the left of the opening
                Player.x = gameObject.transform.position.x - 0.5f;
                if (gameObject.name == "SG_3_to_SG_9" || gameObject.name == "MB_2_to_MB_3")
                {
                    Player.y = gameObject.transform.position.y - gameObject.GetComponent<BoxCollider2D>().size.y / 2;
                }
                else
                {
                    Player.y = Player.y2;
                }
                Player.rb2D.velocity = new Vector2(Player.v_x, Player.v_y);
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

            //        IT_4 to IT_5
            else if (opName == "IT_4_to_IT_5")
            {
                StartCoroutine(SceneSwitch("IT_5", "IT_5_to_IT_4"));
            }
            else if (opName == "IT_5_to_IT_4")
            {
                StartCoroutine(SceneSwitch("IT_4", "IT_4_to_IT_5"));
            }

            //        IT_4 to IT_6
            else if (opName == "IT_4_to_IT_6")
            {
                StartCoroutine(SceneSwitch("IT_6", "IT_6_to_IT_4"));
            }
            else if (opName == "IT_6_to_IT_4")
            {
                StartCoroutine(SceneSwitch("IT_4", "IT_4_to_IT_6"));
            }

            //        IT_6 to IT_7
            else if (opName == "IT_6_to_IT_7")
            {
                StartCoroutine(SceneSwitch("IT_7", "IT_7_to_IT_6"));
            }
            else if (opName == "IT_7_to_IT_6")
            {
                StartCoroutine(SceneSwitch("IT_6", "IT_6_to_IT_7"));
            }

            //        IT_6 to IT_9
            else if (opName == "IT_6_to_IT_9")
            {
                StartCoroutine(SceneSwitch("IT_9", "IT_9_to_IT_6"));
            }
            else if (opName == "IT_9_to_IT_6")
            {
                StartCoroutine(SceneSwitch("IT_6", "IT_6_to_IT_9"));
            }

            //        IT_7 to IT_8
            else if (opName == "IT_7_to_IT_8")
            {
                StartCoroutine(SceneSwitch("IT_8", "IT_8_to_IT_7"));
            }
            else if (opName == "IT_8_to_IT_7")
            {
                StartCoroutine(SceneSwitch("IT_7", "IT_7_to_IT_8"));
            }

            //        IT_8 to IT_9
            else if (opName == "IT_8_to_IT_9")
            {
                StartCoroutine(SceneSwitch("IT_9", "IT_9_to_IT_8"));
            }
            else if (opName == "IT_9_to_IT_8")
            {
                StartCoroutine(SceneSwitch("IT_8", "IT_8_to_IT_9"));
            }

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

            //          DH_4 to DH_5S
            else if (opName == "DH_4_to_DH_5S")
            {
                StartCoroutine(SceneSwitch("DH_5S", "DH_5S_to_DH_4"));
            }
            else if (opName == "DH_5S_to_DH_4")
            {
                StartCoroutine(SceneSwitch("DH_4", "DH_4_to_DH_5S"));
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

            //          TT_1 to TT_2
            else if (opName == "TT_1_to_TT_2")
            {
                StartCoroutine(SceneSwitch("TT_2", "TT_2_to_TT_1"));
            }
            else if (opName == "TT_2_to_TT_1")
            {
                StartCoroutine(SceneSwitch("TT_1", "TT_1_to_TT_2"));
            }

            //          TT_2 to TT_11
            else if (opName == "TT_2_to_TT_11")
            {
                StartCoroutine(SceneSwitch("TT_11", "TT_11_to_TT_2"));
            }
            else if (opName == "TT_11_to_TT_2")
            {
                StartCoroutine(SceneSwitch("TT_2", "TT_2_to_TT_11"));
            }

            //          TT_2 to TT_13
            else if (opName == "TT_13_to_TT_2")
            {
                StartCoroutine(SceneSwitch("TT_2", "TT_2_to_TT_13"));
            }            

            //          TT_4 to TT_5
            else if (opName == "TT_4_to_TT_5")
            {
                StartCoroutine(SceneSwitch("TT_5", "TT_5_to_TT_4"));
            }
            else if (opName == "TT_5_to_TT_4")
            {
                StartCoroutine(SceneSwitch("TT_4", "TT_4_to_TT_5"));
            }

            //          TT_5 to TT_8
            else if (opName == "TT_8_to_TT_5")
            {
                StartCoroutine(SceneSwitch("TT_5", "TT_5_to_TT_8"));
            }

            //          TT_6 to TT_6S
            else if (opName == "TT_6_to_TT_6S")
            {
                StartCoroutine(SceneSwitch("TT_6S", "TT_6S_to_TT_6"));
            }
            else if (opName == "TT_6S_to_TT_6")
            {
                StartCoroutine(SceneSwitch("TT_6", "TT_6_to_TT_6S"));
            }

            //          TT_6 to TT_7
            else if (opName == "TT_6_to_TT_7")
            {
                StartCoroutine(SceneSwitch("TT_7", "TT_7_to_TT_6"));
            }
            else if (opName == "TT_7_to_TT_6")
            {
                StartCoroutine(SceneSwitch("TT_6", "TT_6_to_TT_7"));
            }

            //          TT_6S to TT_7S
            else if (opName == "TT_6S_to_TT_7S")
            {
                StartCoroutine(SceneSwitch("TT_7S", "TT_7S_to_TT_6S"));
            }
            else if (opName == "TT_7S_to_TT_6S")
            {
                StartCoroutine(SceneSwitch("TT_6S", "TT_6S_to_TT_7S"));
            }

            //          TT_7 to TT_7S
            else if (opName == "TT_7_to_TT_7S")
            {
                StartCoroutine(SceneSwitch("TT_7S", "TT_7S_to_TT_7"));
            }
            else if (opName == "TT_7S_to_TT_7")
            {
                StartCoroutine(SceneSwitch("TT_7", "TT_7_to_TT_7S"));
            }

            //          TT_8 to TT_9
            else if (opName == "TT_8_to_TT_9")
            {
                StartCoroutine(SceneSwitch("TT_9", "TT_9_to_TT_8"));
            }
            else if (opName == "TT_9_to_TT_8")
            {
                StartCoroutine(SceneSwitch("TT_8", "TT_8_to_TT_9"));
            }

            //          TT_9 to TT_10
            else if (opName == "TT_9_to_TT_10")
            {
                StartCoroutine(SceneSwitch("TT_10", "TT_10_to_TT_9"));
            }

            //          TT_10 to TT_11
            else if (opName == "TT_10_to_TT_11")
            {
                StartCoroutine(SceneSwitch("TT_11", "TT_11_to_TT_10"));
            }
            else if (opName == "TT_11_to_TT_10")
            {
                StartCoroutine(SceneSwitch("TT_10", "TT_10_to_TT_11"));
            }

            //          TT_11 to TT_12
            else if (opName == "TT_11_to_TT_12")
            {
                StartCoroutine(SceneSwitch("TT_12", "TT_12_to_TT_11"));
            }
            else if (opName == "TT_12_to_TT_11")
            {
                StartCoroutine(SceneSwitch("TT_11", "TT_11_to_TT_12"));
            }

            //          TT_15 to TT_16
            else if (opName == "TT_15_to_TT_16")
            {
                StartCoroutine(SceneSwitch("TT_16", "TT_16_to_TT_15"));
            }

            /*-----MB-----*/

            //          MB_1 to MB_2
            else if (opName == "MB_1_to_MB_2")
            {
                StartCoroutine(SceneSwitch("MB_2", "MB_2_to_MB_1"));
            }
            else if (opName == "MB_2_to_MB_1")
            {
                StartCoroutine(SceneSwitch("MB_1", "MB_1_to_MB_2"));
            }

            //          MB_2 to MB_3
            else if (opName == "MB_2_to_MB_3")
            {
                StartCoroutine(SceneSwitch("MB_3", "MB_3_to_MB_2"));
            }
            else if (opName == "MB_3_to_MB_2")
            {
                StartCoroutine(SceneSwitch("MB_2", "MB_2_to_MB_3"));
            }

            //          MB_3 to MB_3S
            else if (opName == "MB_3_to_MB_3S")
            {
                StartCoroutine(SceneSwitch("MB_3S", "MB_3S_to_MB_3"));
            }
            else if (opName == "MB_3S_to_MB_3")
            {
                StartCoroutine(SceneSwitch("MB_3", "MB_3_to_MB_3S"));
            }

            //          MB_3 to MB_3S2
            else if (opName == "MB_3_to_MB_3S2")
            {
                StartCoroutine(SceneSwitch("MB_3S2", "MB_3S2_to_MB_3"));
            }
            else if (opName == "MB_3S2_to_MB_3")
            {
                StartCoroutine(SceneSwitch("MB_3", "MB_3_to_MB_3S2"));
            }

            //          MB_3 to MB_9
            else if (opName == "MB_3_to_MB_9")
            {
                StartCoroutine(SceneSwitch("MB_9", "MB_9_to_MB_3"));
            }
            else if (opName == "MB_9_to_MB_3")
            {
                StartCoroutine(SceneSwitch("MB_3", "MB_3_to_MB_9"));
            }

            //          MB_3 to MB_10
            else if (opName == "MB_3_to_MB_10")
            {
                StartCoroutine(SceneSwitch("MB_10", "MB_10_to_MB_3"));
            }
            else if (opName == "MB_10_to_MB_3")
            {
                StartCoroutine(SceneSwitch("MB_3", "MB_3_to_MB_10"));
            }

            //          MB_4 to MB_5
            else if (opName == "MB_4_to_MB_5")
            {
                StartCoroutine(SceneSwitch("MB_5", "MB_5_to_MB_4"));
            }
            else if (opName == "MB_5_to_MB_4")
            {
                StartCoroutine(SceneSwitch("MB_4", "MB_4_to_MB_5"));
            }

            //          MB_6 to MB_7
            else if (opName == "MB_6_to_MB_7")
            {
                StartCoroutine(SceneSwitch("MB_7", "MB_7_to_MB_6"));
            }
            else if (opName == "MB_7_to_MB_6")
            {
                StartCoroutine(SceneSwitch("MB_6", "MB_6_to_MB_7"));
            }

            //          MB_8 to MB_9
            else if (opName == "MB_8_to_MB_9")
            {
                StartCoroutine(SceneSwitch("MB_9", "MB_9_to_MB_8"));
            }
            else if (opName == "MB_9_to_MB_8")
            {
                StartCoroutine(SceneSwitch("MB_8", "MB_8_to_MB_9"));
            }

            //          MB_10 to MB_11
            else if (opName == "MB_10_to_MB_11")
            {
                StartCoroutine(SceneSwitch("MB_11", "MB_11_to_MB_10"));
            }
            else if (opName == "MB_11_to_MB_10")
            {
                StartCoroutine(SceneSwitch("MB_10", "MB_10_to_MB_11"));
            }

            /*-----GP-----*/

            /*-----FS-----*/

            /*-----Transition (IT-GP)-----*/

            //          IT_2 to GP_1
            else if (opName == "IT_2_to_GP_1")
            {
                StartCoroutine(SceneSwitch("GP_1", "GP_1_to_IT_2"));
            }
            else if (opName == "GP_1_to_IT_2")
            {
                StartCoroutine(SceneSwitch("IT_2", "IT_2_to_GP_1"));
            }

            /*-----Transition (SG-TT)-----*/

            //          SG_7 to TT_1
            else if (opName == "SG_7_to_TT_1")
            {
                StartCoroutine(SceneSwitch("TT_1", "TT_1_to_SG_7"));
            }
            else if (opName == "TT_1_to_SG_7")
            {
                StartCoroutine(SceneSwitch("SG_7", "SG_7_to_TT_1"));
            }

            /*-----Transition (TT-MB)-----*/

            //          TT_16 to MB_1
            else if (opName == "TT_16_to_MB_1")
            {
                StartCoroutine(SceneSwitch("MB_1", "MB_1_to_TT_16"));
            }
            else if (opName == "MB_1_to_TT_16")
            {
                StartCoroutine(SceneSwitch("TT_16", "TT_16_to_MB_1"));
            }

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
