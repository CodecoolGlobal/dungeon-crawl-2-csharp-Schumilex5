using UnityEngine;
using TMPro;
using DungeonCrawl.Actors.Characters;
using DungeonCrawl.Actors.Items;

public class EpopUpController : MonoBehaviour
{
    public TextMeshProUGUI myText;


    // Update is called once per frame
    void Update()
    {
        Player player = GameObject.FindObjectOfType<Player>();
        Item[] items = GameObject.FindObjectsOfType<Item>();
        myText.text = "";

        foreach(Item item in items)
        {
            if (item.Position == player.Position)
            {
                myText.text = $"Use 'E' to pick up {item.DefaultName}";
            }
        }
    }
}
