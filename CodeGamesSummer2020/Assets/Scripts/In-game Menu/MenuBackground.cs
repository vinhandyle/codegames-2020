using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBackground : MonoBehaviour
{
    Image img;

    public static bool inMenu = false;

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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            MenuBtnBehavior.btn = "";
            if (inMenu)
            {
                Cursor.visible = false;
                inMenu = false;
                StartCoroutine(SceneSwitch(GlobalControl.prevArea));
            }
            else
            {
                Cursor.visible = true;
                inMenu = true;
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
}
