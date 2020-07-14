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
        if (MenuBtn.inMenu && !Map.mapOpen && !InfoBtn.infoPage)
        {
            if (descOf == "")                                                  // Empty description
            {
                text.text = "";
            }
            else if (descOf == "battery" && GlobalControl.batteryUnlocked)     // Battery description
            {
                text.text = "<b>Battery</b> \n" +
                            "Stores the energy you collected.\n" +
                            "Max capacity: " + GlobalControl.energyMax +
                            "\n\n<i>An external energy storage. It is able to store energy collected through different means. " +
                            "Contains several nodes in which additonal batteries can be connected to, increasing its capacity.</i>";
            }
            else if (descOf == "solar" && GlobalControl.solarUnlocked)         // Solar Panel description
            {
                text.text = "<b>Solar Panel</b> \n" +
                            "Generates energy while under sunlight.\n" +
                            "Generates 1 energy per second.\n\n" +
                            "<i>A simple module of photo-voltaic cells to be mounted overhead. Though a lot of dust has settled " +
                            "on its surface, the module has not sustained any serious damage.</i>";
            }
            else if (descOf == "geothermal" && GlobalControl.geoUnlocked)      // Geothermal Extractor description
            {
                text.text = "<b>Geothermal Extractor</b> \n" +
                            "Generates energy while above a heat vent.\n" +
                            "Generates 5 energy per second.\n\n" +
                            "<i>A portable geothermal heat pump to be mounted underfoot. Because of its small size, it is only " +
                            "effective when the source of heat is very close to the surface of the ground. This design flaw " +
                            "may explain why it has long been abandoned in such a remote area.</i>";
            }
            else if (descOf == "dash" && GlobalControl.dashUnlocked)           // Dash item description
            {
                text.text = "<b>Booster Rocket</b> \n" +
                            "Grants the ability to dash along the ground or through the air.\n\n" +
                            "<i>A small propulsion device constructed from a Hyper Scrap. Short bursts of speed is a feature " +
                            "found only in Machina tasked with the capture of rogue humans and Machina. A notable example of " +
                            "which resides in the Twilight Town. Being an inferior copy, it requires time to re-engage.</i>";
            }
            else if (descOf == "cling" && GlobalControl.clingUnlocked)         // Wall jump item description
            {
                text.text = "<b>Climbing Claws</b> \n" +
                            "Grants the ability to cling to and leap off from walls.\n\n" +
                            "<i>A modified variant of the ice axe, constructed from a Hyper Scrap. As tool for exploration, " +
                            "this device is of little use for the inhabitants of Imperalis. But the Construction Machina's " +
                            "ability to create it implies that it could still be useful, but perhaps not now...</i>";
            }
            else if (descOf == "double" && GlobalControl.doubleUnlocked)       // Double jump item description
            {
                text.text = "<b>Booster Rocket MK2</b> \n" +
                            "Grants the ability to jump again in mid-air.\n\n" +
                            "<i>A small propulsion device constructed from a Hyper Scrap. As a more lightweight model, it is " +
                            "able to re-engage on contact with solid ground. Flight is a feature exclusive to Aerial Machina " +
                            "which this tries to imitate.</i>";
            }
            else if (descOf == "gun" && GlobalControl.gunUnlocked)             // Energy Cannon description
            {
                text.text = "<b>Energy Cannon</b> \n" +
                            "Uses energy to fire an energy bullet.\n\n" +
                            "<i>A staple for most Machina, it serves a means to an end. Unlike the weapons of yore, no " +
                            "ammunition is required to operate it. However, a reactor is needed to concentrate the energy " +
                            "gathered from external sources into something powerful. Fortunately, all Machina are powered " +
                            "by an internal reactor.</i>";
            }
            else if (descOf == "map" && GlobalControl.mapUnlocked)             // Map item description
            {
                text.text = "<b>Navigational Module</b> \n" +
                            "Shows current location within Imperalis.\n\n" +
                            "<i>Records of the old world have disappeared after the Troubles. The place that one need know " +
                            "about is the great Imperalis, humanity's last safe haven. Commonly used to track the location " +
                            "of the Machina, they can also be used for navigation by the Azimuth. After being seeped in sludge, " +
                            "this device is no longer connected to the network.</i>";
            }
            else if (descOf == "heartless" && GlobalControl.heartlessUnlocked) // Health-Energy item description
            {
                text.text = "<b>Heartless Generator</b> \n" +
                            "Convert 1 health unit into 3 energy units or 5 energy units to 1 health unit.\n\n" +
                            "<i>Mass can be converted into energy and energy into mass. Once a difficult task now trivialized " +
                            "by this tiny device. It finds more usage in the Machina whose mass can easily be replaced at " +
                            "repair stations. However, this conversion is limited since some energy is loss when converted " +
                            "into mass.</i> \n\n";
            }
            else if (descOf == "scrap" && GlobalControl.scrapFound)            // Hyper Scrap description
            {
                text.text = "<b>Hyper Scrap</b> \n" +
                            "Bring to a Construction Machina to craft an special item.\n" +
                            "In Possession: " + GlobalControl.scrapNum +
                            "\n\n<i>The Machina were not created for the sake of violence as seen by the lack of individual " +
                            "strength. After the Ego defection, however, the Emperor constructed three combat-specialized Machina " +
                            "to prevent a potential uprising. None have occurred, yet...</i>";
            }
            else if (descOf == "extra" && GlobalControl.extraFound)            // Extra Battery description
            {
                text.text = "<b>Extra Battery</b> \n" +
                            "Increases the Battery's energy capacity by 10.\n" +
                            "In Possession: " + GlobalControl.extraNum +
                            "\n\n<i>An external energy storage extension. They are fitted into the nodes of a Battery though most " +
                            "are already fully stocked. It is rare to find spares laying around. " +
                            ".</i>";
            }
            else if (descOf == "plating" && GlobalControl.plateFound)          // Special Plating description
            {
                text.text = "<b>Special Plating</b> \n" +
                            "Increases health by 10.\n" +
                            "In Possession: " + GlobalControl.plateNum +
                            "\n\n<i>From the moment they are created to the time of their disassemby, a Machina's components do " +
                            "not change. Modifications are unnecessary when they are built to successfully carry out their task." +
                            "Only those who'd go rogue would even consider altering their inital design.</i>";
            }
            else if (descOf == "basic" && GlobalControl.basicUnlocked)         // Basic Reactor description
            {
                text.text = "<b>Basic Reactor</b> ";
                if (GlobalControl.reactor == "basic")
                {
                    text.text += "- <i>Equipped</i>";
                }
                text.text += "\nDamage: " + (1 + GlobalControl.scrapNum) + "  Energy Use: 1 \n\n" +
                             "<i>The standard model for the Machina. It provides just enough power to get the job done. " +
                             "This particular reactor has been specially designed by the Doctor to grow more powerful when " +
                             "overcoming great obstacles. Perhaps this is a privilege granted only to the [REDACTED].</i>";
            }
            else if (descOf == "imperial" && GlobalControl.imperialUnlocked)   // Imperial Reactor description
            {
                text.text = "<b>Gentle Reactor</b> ";
                if (GlobalControl.reactor == "imperial")
                {
                    text.text += "- <i>Equipped</i>";
                }
                text.text += "\nDamage: 0  Energy Use: 1 \n\n" +
                            "<i>A specialized by designed by the Emperor specifically for the capture of the Errat. Most Machina " +
                            "cannot operate in the the Dreg Heap, as the fine dust and frigid temperatures of this wretched place " +
                            "compromises their delicate circuitry. The Emperor cannot leave its station. That leaves only ones from " +
                            "the [REDACTED] to complete this important task.</i>";
            }
            else if (descOf == "familiar" && GlobalControl.familiarUnlocked)   // Familiar Reactor description
            {
                text.text = "<b>Lost Reactor</b> ";
                if (GlobalControl.reactor == "familiar")
                {
                    text.text += "- <i>Equipped</i>";
                }
                text.text += "\nDamage: " + (0 + GlobalControl.data / 10) + "  Energy Use: 2 \n\n" +
                            "<i>Beneath the great nation of Imperalis lies that which should be forgotten about. Though most of it " +
                            "is utterly useless, one could find treasure in this dreary place. This reactor is proof of that. " +
                            "Somehow, it feels familiar. Inscribed on its exterior: \"Knowledge is power\".</i>";
            }
            else if (descOf == "unstable" && GlobalControl.unstableUnlocked)   // Unstable Reactor description
            {
                text.text = "<b>Unstable Reactor</b> ";
                if (GlobalControl.reactor == "unstable")
                {
                    text.text += "- <i>Equipped</i>";
                }
                text.text += "\nDamage: 10  Energy Use: 1 \n\n" +
                            "<i>An experimental reactor that should not be used in any practical situation. The last project of an " +
                            "old man who came to terms with his mortality. Containing a great deal of volatile substances, the " +
                            "slightest touch could cause it to explode.</i>";
            }
        }
        else
        {
            descOf = "";
            text.text = "";
        }
    }
}
