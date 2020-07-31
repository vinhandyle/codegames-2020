using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    // Non-static variables are separate of each other when applied to different objects (i.e. multiple enemies using this script)
    public int healthMax;
    public int healthCurr;
    public int damage;
    public List<Sprite> sprites;
    public bool hazard; // Enemy or Hazard

    /*-----Variables used in AI-----*/
    // Starting Position
    public float x;
    public float y;

    // Movement
    public float range;
    public float speed;

    // Time
    public int time;                // 145 frames ~ 1 second
    public int deAggroTime;

    // States -> refState,type,variant(if applicable)_num
    // type: Obstacles to Scope (1), Scope to Obstacles (2), Obstacles to Obstacles (3)
    // variant: multiple of same type in same num, use a->z
    // num: Pursuit (1), Aerial (2), Aquatic (3), Turret (4), Overseer (5), Containment (6), Subnautical (7), Emperor (8)
    // e.g. refState2b_5
    public string aiState;
    
    /* Pursuit */
    public static string refState_1;  
    public static string refState2_1; 
    public static string refState2a_1;
    public static string refState3_1; 

    // Start is called before the first frame update
    void Start()
    {
        // Retain alive.dead status when reloading scene
        if ((gameObject.name == "Patrol_1_0_0" && !GlobalControl.patrol_1_0_0) ||
            (gameObject.name == "Patrol_1_0_1" && !GlobalControl.patrol_1_0_1) ||
            (gameObject.name == "Patrol_1_0_2" && !GlobalControl.patrol_1_0_2) ||
            (gameObject.name == "Patrol_1_1_0" && !GlobalControl.patrol_1_1_0) ||
            (gameObject.name == "Patrol_1_2_0" && !GlobalControl.patrol_1_2_0) ||
            (gameObject.name == "Patrol_1_2_1" && !GlobalControl.patrol_1_2_1) ||
            (gameObject.name == "Errat_0" && !GlobalControl.errat_0) ||
            (gameObject.name == "Errat_1" && !GlobalControl.errat_1) ||
            (gameObject.name == "Errat_2" && !GlobalControl.errat_2) ||
            (gameObject.name == "Errat_3" && !GlobalControl.errat_3) ||
            (gameObject.name == "Errat_4" && !GlobalControl.errat_4) ||
            (gameObject.name == "Errat_5" && !GlobalControl.errat_5) ||
            (gameObject.name == "Pursuit_1_2_0" && !GlobalControl.pursuit_1_2_0) ||
            (gameObject.name == "Pursuit_1_2_1" && !GlobalControl.pursuit_1_2_1))
        {
            gameObject.SetActive(false);
        }

        // Set starting position
        x = transform.position.x;
        y = transform.position.y;

        // Initialize enemy health and damage

        /*---------------Enemies----------------*/

        // Patrol Machina
        if (gameObject.name.Substring(0, 6) == "Patrol")
        {
            if (gameObject.name.Substring(6, 2) == "_1")
            { // Tier 1: IT (1st), Sunset Garden
                healthMax = 4;
                damage = 1;
            }
            else if (gameObject.name.Substring(6, 2) == "_2")
            { // Tier 2: Twilight Town, Midnight Bay
                //healthMax = ;
                //damage = ;
            }
            else if (gameObject.name.Substring(6, 2) == "_3")
            { // Tier 3: IT (2nd), Grey Palace
                //healthMax = ;
                //damage = ;
            }
        }

        // Pursuit Machina
        else if (gameObject.name.Substring(0, 7) == "Pursuit")
        {
            if (gameObject.name.Substring(7, 2) == "_1")
            { // Tier 1: Sunset Garden
                healthMax = 3;
                damage = 2;
            }
            else if (gameObject.name.Substring(7, 2) == "_2")
            { // Tier 2: Grey Palace
                //healthMax = ;
                //damage = ;
            }
        }

        // Aerial Machina
        else if (gameObject.name.Substring(0, 6) == "Aerial")
        {
            if (gameObject.name.Substring(6, 2) == "_1")
            { // Tier 1: Twilight Town
                //healthMax = ;
                //damage = ;
            }
            else if (gameObject.name.Substring(6, 2) == "_2")
            { // Tier 2: Grey Palace
                //healthMax = ;
                //damage = ;
            }
        }

        // Aquatic Machina
        else if (gameObject.name.Substring(0, 7) == "Aquatic")
        {
            if (gameObject.name.Substring(7, 2) == "_1")
            { // Tier 1: Midnight Bay
                //healthMax = ;
                //damage = ;
            }
            else if (gameObject.name.Substring(7, 2) == "_2")
            { // Tier 2: Grey Palace
                //healthMax = ;
                //damage = ;
            }
        }

        // Turret Machina
        else if (gameObject.name.Substring(0, 6) == "Turret")
        {
            if (gameObject.name.Substring(6, 2) == "_1")
            { // Tier 1: IT (2nd)
                //healthMax = ;
                //damage = ;
            }
            else if (gameObject.name.Substring(6, 2) == "_2")
            { // Tier 2: Grey Palace
                //healthMax = ;
                //damage =;
            }
        }

        // Errat
        else if (gameObject.name.Substring(0, 5) == "Errat")
        { // Dreg Heap
            healthMax = 1;
            damage = 0;
        }

        /*---------------Hazards----------------*/

        // Toxic Sludge
        else if (gameObject.name.Substring(0, 6) == "Sludge")
        { // Dreg Heap
            damage = 3;
        }


        /*---------------Bosses----------------*/


        if (!hazard)
        {
            healthCurr = healthMax;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (healthCurr <= 0 && !hazard)
        { // Set enemy inactive when hp = 0
            gameObject.SetActive(false);

            // Add type to catalog on kill
            if (gameObject.name.Substring(0, 6) == "Patrol")
            {
                GlobalControl.downed_patrol = true;
            }
            else if (gameObject.name.Substring(0, 7) == "Pursuit")
            {
                GlobalControl.downed_pursuit = true;
            }
            else if (gameObject.name.Substring(0, 6) == "Aerial")
            {
                GlobalControl.downed_aerial = true;
            }
            else if (gameObject.name.Substring(0, 7) == "Aquatic")
            {
                GlobalControl.downed_aquatic = true;
            }
            else if (gameObject.name.Substring(0, 6) == "Turret")
            {
                GlobalControl.downed_turret = true;
            }
            else if (gameObject.name.Substring(0, 6) == "Errat")
            {
                GlobalControl.found_errat = true;
            }

            // Prevent respawn on scene switch

            // Testing Area
            if (gameObject.name == "Patrol_1_0_0")
            {
                GlobalControl.patrol_1_0_0 = false;
            }
            else if (gameObject.name == "Patrol_1_0_1")
            {
                GlobalControl.patrol_1_0_1 = false;
            }
            else if (gameObject.name == "Patrol_1_0_2")
            {
                GlobalControl.patrol_1_0_2 = false;
            }
            else if (gameObject.name == "Patrol_1_1_1")
            {
                GlobalControl.patrol_1_1_0 = false;
            }

            // Dreg Heap
            else if (gameObject.name.Substring(0, 5) == "Errat")
            {
                if (gameObject.name == "Errat_0")
                {
                    GlobalControl.errat_0 = false;
                }
                else if (gameObject.name == "Errat_1")
                {
                    GlobalControl.errat_1 = false;
                }
                else if (gameObject.name == "Errat_2")
                {
                    GlobalControl.errat_2 = false;
                }
                else if (gameObject.name == "Errat_3")
                {
                    GlobalControl.errat_3 = false;
                }
                else if (gameObject.name == "Errat_4")
                {
                    GlobalControl.errat_4 = false;
                }
                else if (gameObject.name == "Errat_5")
                {
                    GlobalControl.errat_5 = false;
                }
                GlobalControl.humansLeft--;
            }
        }

        /*-----Patrol Machina-----*/
        if (gameObject.name.Substring(0, 6) == "Patrol")
        {
            // Moving left
            if (aiState == "moveLeft")
            {
                if (transform.position.x > x - range)
                {
                    transform.position += new Vector3(-speed, 0, 0);
                }
                else
                {
                    aiState = "moveRight";
                }
            }
            // Moving right
            else if (aiState == "moveRight")
            {
                if (transform.position.x < x + range)
                {
                    transform.position += new Vector3(speed, 0, 0);
                }
                else
                {
                    aiState = "moveLeft";
                }
            }
        }

        /*-----Pursuit Machina-----*/
        else if (gameObject.name.Substring(0, 7) == "Pursuit")
        {
            /*---Scout Mode---*/
            if (aiState == "passive_right")
            {
                // Detect player when facing towards and at the same level
                if (Player.rb2D.position.y - Player.rb2D.gameObject.GetComponent<CircleCollider2D>().radius < transform.position.y + gameObject.GetComponent<BoxCollider2D>().size.y / 2 &&
                    Player.rb2D.position.y + Player.rb2D.gameObject.GetComponent<CircleCollider2D>().radius > transform.position.y - gameObject.GetComponent<BoxCollider2D>().size.y / 2 &&
                    Player.rb2D.position.x > transform.position.x && refState2_1 != "passive")
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];
                    aiState = "hostile_right";
                }

                // Move Right
                if (transform.position.x < x + range)
                {
                    transform.position += new Vector3(speed, 0, 0);
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
                    aiState = "passive_left";
                }
            }
            else if (aiState == "passive_left")
            {
                // Detect player when facing towards and at the same level
                if (Player.rb2D.position.y - Player.rb2D.gameObject.GetComponent<CircleCollider2D>().radius < transform.position.y + gameObject.GetComponent<BoxCollider2D>().size.y / 2 &&
                    Player.rb2D.position.y + Player.rb2D.gameObject.GetComponent<CircleCollider2D>().radius > transform.position.y - gameObject.GetComponent<BoxCollider2D>().size.y / 2 &&
                    Player.rb2D.position.x < transform.position.x && refState2_1 != "passive")
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[3];
                    aiState = "hostile_left";
                }

                // Move Left
                if (transform.position.x > x - range)
                {
                    transform.position += new Vector3(-speed, 0, 0);
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
                    aiState = "passive_right";
                }
            }

            /*---Chase Mode---*/
            else if (aiState == "hostile_right")
            {
                if (Player.rb2D.position.x < transform.position.x)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[3];
                    aiState = "hostile_left";
                }
                else if (refState2a_1 == "stop")
                {
                    refState3_1 = aiState;
                    aiState = "stop";
                }
                else
                {
                    transform.position += new Vector3(2 * speed, 0, 0);
                }
            }
            else if (aiState == "hostile_left")
            {
                if (Player.rb2D.position.x > transform.position.x)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];
                    aiState = "hostile_right";
                }
                else if (refState2a_1 == "stop")
                {
                    refState3_1 = aiState;
                    aiState = "stop";
                }
                else
                {
                    transform.position += new Vector3(-2 * speed, 0, 0);
                }
            }
            else if (aiState == "stop")
            {
                time++;
                if (time > deAggroTime)
                {
                    if (refState3_1 == "hostile_left")
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
                        aiState = "passive_right";
                    }
                    else if (refState3_1 == "hostile_right")
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
                        aiState = "passive_left";
                    }
                    time = 0;
                    refState2a_1 = "";
                }
                else if (Player.rb2D.position.x > transform.position.x && refState3_1 == "hostile_left")
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];
                    aiState = "hostile_right";
                    time = 0;
                    refState2a_1 = "";
                }
                else if (Player.rb2D.position.x < transform.position.x && refState3_1 == "hostile_right")
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[3];
                    aiState = "hostile_left";
                    time = 0;
                    refState2a_1 = "";
                }
            }
        }

        /*-----Aerial Machina-----*/
        else if (gameObject.name.Substring(0, 6) == "Aerial")
        {
            // Insert AI here
        }
        else if (gameObject.name.Substring(0, 7) == "Aquatic")
        {
            // Insert AI here
        }
        else if (gameObject.name.Substring(0, 6) == "Turret")
        {
            // Insert AI here
        }

        if (gameObject.name.Substring(0, 7) == "Pursuit")
        {
            refState_1 = aiState;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player Bullet")
        {
            if (gameObject.CompareTag("Enemy"))
            {
                healthCurr -= GlobalControl.damage;
                Debug.Log(healthCurr + " HP remaining!");

                if (gameObject.name.Substring(0, 7) == "Pursuit")
                {
                    if (Player.rb2D.position.x < transform.position.x)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[3];
                        aiState = "hostile_left";
                    }
                    else if (Player.rb2D.position.x > transform.position.x)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];
                        aiState = "hostile_right";
                    }
                    time = 0;
                }
            }
            else if (gameObject.name.Substring(0, 5) == "Errat" && GlobalControl.reactor == "imperial")
            {
                healthCurr = 0;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!(gameObject.name.Substring(0, 6) == "Turret" || gameObject.name.Substring(0, 5) == "Errat") && other.gameObject.CompareTag("Player") && !GlobalControl.immune)
        {
            if (GlobalControl.reactor == "unstable")
            {
                GlobalControl.healthCurr = 0;
            }
            else
            {
                GlobalControl.healthCurr -= damage;
                StartCoroutine(IFrame());
            }
        }
    }

    IEnumerator IFrame()
    {
        GlobalControl.immune = true;

        yield return new WaitForSeconds(1f);

        GlobalControl.immune = false;
    }
}
