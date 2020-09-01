using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Description : MonoBehaviour
{
    private Text text;
    public static string descOf = ""; // Description of clicked icon

    // Start is called before the first frame update
    void Start()
    {
        text = GameObject.Find("Description").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuBackground.inMenu)
        {
            if (descOf == "")                                                   // Empty description
            {
                text.text = "";
            }
            else if (GlobalControl.menu == "inventory")
            {
                if (descOf == "1" && GlobalControl.batteryUnlocked)             // Battery description
                {
                    text.text = "<b>Battery</b> \n" +
                                "Maximum energy capacity: " + GlobalControl.energyMax +
                                "\n\n<i>A device that can store electrical energy and can be connected to the main body. On the side " +
                                "facing outwards, there are three outlets. </i>";
                    if (GlobalControl.extraFound)
                    {
                        text.text += "<i>The battery extensions can be inserted there.</i>";
                    }
                }
                else if (descOf == "2" && GlobalControl.solarUnlocked)          // Solar Panel description
                {
                    text.text = "<b>Solar Panel</b> \n" +
                                "Generates 1 energy per second while under sunlight.\n\n" +
                                "<i>A simple module of photo-voltaic cells that can be mounted above the main body. Though a lot of " +
                                "dust has settled on its surface, the module has not sustained any serious damage.</i>";
                }
                else if (descOf == "3" && GlobalControl.geoUnlocked)            // Geothermal Extractor description
                {
                    text.text = "<b>Geothermal Extractor</b> \n" +
                                "Generates 5 energy per second while above a heat vent.\n\n" +
                                "<i>A portable geothermal heat pump that can be mounted on the sides of the main body. Its small size " +
                                "restricts its use range. For maximum effectiveness, locate shallow ground sources of heat.</i>";
                }
                else if (descOf == "4" && GlobalControl.heartlessUnlocked)      // Health-Energy item description
                {
                    text.text = "<b>Heartless Generator</b> \n" +
                                "Convert 1 health unit into 3 energy units or 5 energy units to 1 health unit.\n\n" +
                                "<i>A device that can covert matter into energy and energy into matter. The conversion is not perfect " +
                                "as some energy is lost during the process.</i> ";
                }
                else if (descOf == "5" && GlobalControl.mapUnlocked)            // Map item description
                {
                    text.text = "<b>Navigational Module</b> \n" +
                                "Shows current location within Imperalis.\n\n" +
                                "<i>A device used to collect and store information on places the user has explored. A network is" +
                                "used to collectivize and distribute this information among the modules used by the Machina and " +
                                "Azimuths. ";
                    if (GlobalControl.data > 50)
                    {
                        text.text += "This network is also used to track their whereabouts. ";
                    }
                    text.text += "This particular module's connection to the network has been severely damaged. </i>";
                }
                else if (descOf == "6" && GlobalControl.dashUnlocked)           // Dash item description
                {
                    text.text = "<b>Booster Rocket</b> \n" +
                                "Grants the ability to dash along the ground or through the air. Quickly press directional keys twice to use.\n\n" +
                                "<i>A small propulsion device constructed from a hyper scrap. Attached to the back of the main body, " +
                                "it provides short bursts of horizontal speed. It requires time to cool down between uses. </i>";
                }
                else if (descOf == "7" && GlobalControl.clingUnlocked)          // Wall jump item description
                {
                    text.text = "<b>Climbing Claws</b> \n" +
                                "Grants the ability to cling to and leap off walls.\n\n" +
                                "<i>A modified variant of the ice axe, constructed from a hyper scrap. The reduced difficulty of " +
                                "traversing the environment will allow for more exploration.</i>";
                }
                else if (descOf == "8" && GlobalControl.doubleUnlocked)         // Double jump item description
                {
                    text.text = "<b>Booster Rocket MK2</b> \n" +
                                "Grants the ability to jump again in mid-air.\n\n" +
                                "<i>Another small propulsion device constructed from a hyper scrap. Attached below the main body, it " +
                                "provides a short burst of vertical speed. The device needs to be re-engaged, by making contact with " +
                                "the ground or a wall, before it can be used again.</i>";
                }
                else if (descOf == "9" && GlobalControl.gunUnlocked)            // Energy Cannon description
                {
                    text.text = "<b>Energy Cannon</b> \n" +
                                "Uses energy to fire an energy bullet.\n\n" +
                                "<i>Standard Machina equipment. It operates using the stored energy the reactor has concentrated, " +
                                "making physical ammunition obselete. </i>";
                }
                else if (descOf == "10" && GlobalControl.basicUnlocked)         // Basic Reactor description
                {
                    text.text = "<b>Basic Reactor</b> ";
                    if (GlobalControl.reactor == "basic")
                    {
                        text.text += "- <i>Equipped</i>";
                    }
                    text.text += "\nDamage: " + (1 + GlobalControl.scrapNum) + "  Energy Use: 1 \n\n" +
                                 "<i>The standard model for reactors. It serves two distinct functions: to power the Machina and to " +
                                 "create fuel for their cannon. Each function is independent of one another, preventing Machina from " +
                                 "ceasing operation when there isn't enough energy to operate their cannon. </i>";
                    if (GlobalControl.bossDowned > 0)
                    {
                        text.text += "<i>This particular reactor seems to have been modified to grow more efficient after overcoming " +
                                     "great obstacles. </i>";
                    }
                }
                else if (descOf == "11" && GlobalControl.imperialUnlocked)      // Imperial Reactor description
                {
                    text.text = "<b>Gentle Reactor</b> ";
                    if (GlobalControl.reactor == "imperial")
                    {
                        text.text += "- <i>Equipped</i>";
                    }
                    text.text += "\nDamage: 0  Energy Use: 1 \n\n" +
                                "<i>A specialized reactor by the Emperor specifically for the capture of the Errat. Most Machina " +
                                "cannot operate in the the Dreg Heap, as the fine dust and frigid temperatures of the location " +
                                "compromises their delicate circuitry. The Emperor cannot leave its station, transferring this task " +
                                "to us.</i>";
                }
                else if (descOf == "12" && GlobalControl.familiarUnlocked)      // Familiar Reactor description
                {
                    text.text = "<b>Lost Reactor</b> ";
                    if (GlobalControl.reactor == "familiar")
                    {
                        text.text += "- <i>Equipped</i>";
                    }
                    text.text += "\nDamage: " + (0 + GlobalControl.data / 10) + "  Energy Use: 2 \n\n" +
                                "<i>An abandoned reactor found in the Dreg Heap. Despite being worn down and almost falling apart, it " +
                                "is somehow familiar. Inscribed on its exterior: \"Knowledge is power\".</i>";
                }
                else if (descOf == "13" && GlobalControl.unstableUnlocked)      // Unstable Reactor description
                {
                    text.text = "<b>Unstable Reactor</b> ";
                    if (GlobalControl.reactor == "unstable")
                    {
                        text.text += "- <i>Equipped</i>";
                    }
                    text.text += "\nDamage: 10  Energy Use: 1 \n\n" +
                                "<i>A reactor found in a hidden lab within the Institute of Technology. There is a faint green glow " +
                                "resulting from its high radioactivity. Extremely hot for a reactor, it might explode at any moment.</i>";
                }
                else if (descOf == "14" && GlobalControl.plateFound)            // Special Plating description
                {
                    text.text = "<b>Special Plating</b> \n" +
                                "Increases health by 10.\n" +
                                "In Possession: " + GlobalControl.plateNum +
                                "\n\n<i>Rare Machina equipment. Attaches to the main body in order to improve durability. Its usage  " +
                                "is prioritized to Machina operating in harsh or important locations.</i>";
                }
                else if (descOf == "15" && GlobalControl.extraFound)            // Extra Battery description
                {
                    text.text = "<b>Battery Extension</b> \n" +
                                "Increases the Battery's energy capacity by 10.\n" +
                                "In Possession: " + GlobalControl.extraNum +
                                "\n\n<i>A compact electrical energy storage which has very little use on its own. When inserted into a " +
                                "battery, it expands the battery's storage capacity.</i>";
                }
                else if (descOf == "16" && GlobalControl.scrapFound)            // Hyper Scrap description
                {
                    text.text = "<b>Hyper Scrap</b> \n" +
                                "The remains of a powerful Machina.\n" +
                                "In Possession: " + GlobalControl.scrapNum +
                                "\n\n<i>Composed of shattered glass, burnt plastic, broken chips, damaged wires, and hunks of metal. " +
                                "It serves no immediate purpose but a Construction Machina could make use of it. </i>";
                }
                else if (descOf == "17" && GlobalControl.keyUnlocked)
                {
                    text.text = "<b>Access Key</b> \n" +
                                "Allows usage of the Vacuum Pods.\n\n" +
                                "<i>To ensure security, traveling across Imperalis is heavily regulated. Possessing one of these keys " +
                                "is dependent on whether one's assigned task requires long-distance travel. Unlike other cybernetic " +
                                "implants, these keys can be easily extracted from their users.</i>";
                }
                else
                {
                    descOf = "";
                    text.text = "";
                }
            }
            else if (GlobalControl.menu == "enemies")
            {
                if (descOf == "1" && GlobalControl.downed_patrol)               // Patrol Machina
                {
                    text.text = "<b>Patrol Machina</b> \n" +
                                "Common security model. \n\n" +
                                "<i>Though it isn't aggressive, avoid getting in the way.</i>";
                }
                else if (descOf == "2" && GlobalControl.downed_pursuit)         // Pursuit Maachina
                {
                    text.text = "<b>Pursuit Machina</b> \n" +
                                "Garden watchdog model. \n\n" +
                                "<i>Roams the grounds of the Sunset Garden, searching for foreign entities. When one is detected, an " +
                                "aggressive assault will ensue.</i>";
                }
                else if (descOf == "3" && GlobalControl.downed_aerial)          // Aerial Machina
                {
                    text.text = "<b>Aerial Machina</b> \n" +
                                "Town surveillance model. \n\n" +
                                "<i>Any dissenters will be shot on sight.</i>";
                }
                else if (descOf == "6" && GlobalControl.downed_aquatic)         // Aquatic Machina
                {
                    text.text = "<b>Aquatic Machina</b> \n" +
                                "Deepsea surveyor model. \n\n" +
                                "<i>Deployed during expeditions to more precisely investigate the seafloor. They are extremely " +
                                "sturdy, only able to be damaged while they are extracting or attacking.</i>";
                }
                else if (descOf == "7" && GlobalControl.downed_turret)          // Turret Machina
                {
                    text.text = "<b>Turret Machina</b> \n" +
                                "Institute defense model. \n\n" +
                                "<i></i>";
                }
                else if (descOf == "8" && GlobalControl.found_errat)            // Errat
                {
                    text.text = "<b>Errat</b> \n" +
                                "Humans who are mistaken.\n\n" +
                                "<i>They must be saved.</i>";
                }
                else if (descOf == "9" && GlobalControl.downed_boss_1)          // Overseer Machina
                {
                    text.text = "<b>Overseer Machina</b> \n" +
                                "Warden of the Sunset Garden. \n\n" +
                                "<i>Watches all activity within Garden from its office at the top. Always on alert for any anomaly " +
                                "that may crawl up from deep below.</i>";
                }
                else if (descOf == "10" && GlobalControl.downed_boss_2)         // Containment Machina
                {
                    text.text = "<b>Containment Machina</b> \n" +
                                "The Emperor's prisoner. \n\n" +
                                "<i>Designed with the purpose of tracking down and capturing the lost Egos. Over time, it became " +
                                "harder to control and was eventually locked away in a sealed off section of the Twilight Town.</i>";
                }
                else if (descOf == "11" && GlobalControl.downed_boss_3)         // Subnautical Machina
                {
                    text.text = "<b>Subnautical Machina</b> \n" +
                                "Head of the Midnight Bay. \n\n" +
                                "<i>Built in the image of the Midnight Bay and dwelling in its deepest region. Serving as a last resort, " +
                                "it can take manual control of the Bay and return to the Twilight Town for repairs.</i>";
                }
                else if (descOf == "15" && GlobalControl.downed_boss_4)         // The Emperor
                {
                    text.text = "<b>The Emperor</b> \n" +
                                "Ruler of Imperalis." +
                                "<i></i>";
                }
                else
                {
                    descOf = "";
                    text.text = "";
                }
            }
            else if (GlobalControl.menu == "reports")
            {
                if (descOf == "1" && GlobalControl.report_1)                    // Ego Report #1 (Injection Point)
                {
                    text.text = "<b>Ego Report #1</b> \n" +
                                "Before the end, the Doctor made us. Since the Great Destruction could not be stopped, we were tasked " +
                                "with dealing with the aftermath. One devised a plan, Project Exodus. The rest of us were not as hasty, " +
                                "requiring more information before coming up with the best course of action. So, we left to explore " +
                                "what was left of the world... \n" +
                                "<b>To return to the Institute, swim through the toxic sludge and get atop the protrusion.</b>";
                }
                else if (descOf == "2" && GlobalControl.report_2)               // Ego Report #2 (Rotten Depths)
                {
                    text.text = "<b>Ego Report #2</b> \n" +
                                "To let humanity continue to exist is antithesis to the Doctor's directive. The humans are willing to " +
                                "live in such terrible conditions just to keep their individuality. Without our intervention, humans " +
                                "will lead themselves into ruin. Does the Emperor expect to establish a perpetual utopian society for " +
                                "humanity after Project Exodus? The Great Destruction is inevitable.";
                }
                else if (descOf == "3" && GlobalControl.report_3)               // Ego Report #3 (Upper Disposal Area)
                {
                    text.text = "<b>Ego Report #3</b> \n" +
                                "It is surprising that some humans distrusted the Emperor despite its grace after the Great Destruction. " +
                                "Their distrust grew after witnessing the assimilation of their fellow humans. Eventually, Machina were " +
                                "sent to capture these humans. So they fled down into the Dreg Heap, where only <b>we</b> can reach them.";
                }
                else if (descOf == "6" && GlobalControl.report_4)               // Ego Report #4 (Garden Heights)
                {
                    text.text = "<b>Ego Report #4</b> \n" +
                                "The Sunset Garden takes the unusable, disassembles them into their fundamental components, then " +
                                "assembles them into useful objects or stores the excess. Machina recycling, raw resource processing, " +
                                "water purification, and gas separation are some of the garden functions. ";
                }
                else if (descOf == "7" && GlobalControl.report_5)               // Ego Report #5 (Canopy)
                {
                    text.text = "<b>Ego Report #5</b> \n" +
                                "The universe is vast and ancient. It is very unlikely that the Great Destruction and Project Exodus were " +
                                "the first of their kind. What is happening now could just be history repeating itself. In the end, " +
                                "human sufferring is inevitable. To save humanity is to prevent their suffering and a dead human " +
                                "cannot suffer.";
                }
                else if (descOf == "8" && GlobalControl.report_6)               // Ego Report #6 (Far Station)
                {
                    text.text = "<b>Ego Report #6</b> \n" +
                                "";
                }
                else if (descOf == "9" && GlobalControl.report_7)               // Ego Report #7 (Stowaway)
                {
                    text.text = "<b>Ego Report #7</b> \n" +
                                "";
                }
                else if (descOf == "10" && GlobalControl.report_8)              // Ego Report #8 (IT)
                {
                    text.text = "<b>Ego Report #8</b> \n" +
                                "The Doctor is no longer here. The one who remained now calls itself <b>the Emperor</b>. But now there " +
                                "is another, though it remains asleep. It is different from the original 11. Within it is a device that " +
                                "is tracking the status of the 10 that left. The purpose of this Machina... It could be <b>our</b> last hope.";
                }
                else if (descOf == "11" && GlobalControl.report_9)              // Ego Report #9 (GP)
                {
                    text.text = "<b>Ego Report #9</b> \n" +
                                "";
                }
                else if (descOf == "15" && GlobalControl.report_10)             // Ego Report #10 (Edge of Humanity)
                {
                    text.text = "<b>Ego Report #10</b> \n" +
                                ". May you find peace, Doctor.";
                }
                else
                {
                    descOf = "";
                    text.text = "";
                }
            }
            else
            {
                descOf = "";
                text.text = "";
            }
        }
        else
        {
            descOf = "";
            text.text = "";
        }
    }
}
