using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBackground : MonoBehaviour
{
    Image img;

    public static bool inMenu;
    public static bool station;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // Open and close menu
        if (Input.GetKeyDown(KeyCode.Tab) && !(GlobalControl.area == "GP_0A" && GlobalControl.counter_1 < 5))
        {
            MenuBtnBehavior.btn = "";
            if (inMenu)
            {
                Cursor.visible = false;
                inMenu = false;
                VacPod.inMotion = false;
                VacPod.once = false;
                if (GlobalControl.prevArea == "GP_2")
                {
                    StartCoroutine(SceneSwitch("GP_0A", "GP_0A_to_GP_2"));
                }
                else if (GlobalControl.prevArea == "IT_3")
                {
                    if(GlobalControl.lift_direction == "down")
                        StartCoroutine(SceneSwitch("DH_7", "DH_7_to_IT_3"));
                    else if(GlobalControl.lift_direction == "up")
                        StartCoroutine(SceneSwitch("IT_4", "IT_4_to_IT_3"));
                }
                else
                    StartCoroutine(SceneSwitch(GlobalControl.prevArea));
            }
            else
            {
                Cursor.visible = true;
                inMenu = true;

                if (!GlobalControl.intro)
                    GlobalControl.intro = true;

                if (GlobalControl.area == "TT_14" && VacPod.inMotion)
                {
                    if (GlobalControl.pod_location == "main" && GlobalControl.pod_direction == "left")
                    {
                        GlobalControl.pod_location = "far";
                        GlobalControl.pod_direction = "right";
                    }
                    else if (GlobalControl.pod_location == "main" && GlobalControl.pod_direction == "right")
                    {
                        GlobalControl.pod_location = "seaside";
                        GlobalControl.pod_direction = "left";
                    }
                    else if (GlobalControl.pod_location == "far" || GlobalControl.pod_location == "seaside")
                        GlobalControl.pod_location = "main";
                }

                StartCoroutine(SceneSwitch("In-Game Menu"));
                if (RepairStation.inRange)
                {
                    InteractText.type = "rest";
                }
            }
        }

        if (inMenu)
        {
            img.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            img.color = new Color(1f, 1f, 1f, 0f);
        }
    }

    IEnumerator SceneSwitch(string load)
    {
        if (GlobalControl.area != "In-Game Menu")
        {
            GlobalControl.pX = Player.rb2D.position.x;
            GlobalControl.pY = Player.rb2D.position.y;
            GlobalControl.prevArea = GlobalControl.area;
        }
        GlobalControl.area = load;
        GlobalControl.nextDoor = "";

        SceneManager.LoadScene(load, LoadSceneMode.Single);
        if (load == GlobalControl.prevArea)
        {
            Player.x = GlobalControl.pX;
            Player.y = GlobalControl.pY;
            GlobalControl.switched = true;
        }

        yield return null;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
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
