using UnityEngine;
using TMPro;
using DungeonCrawl.Actors.Characters;


public class HealthBar : MonoBehaviour
{
    public TextMeshProUGUI myText;

    void Update()
    {
        myText.text = $"HP: {GameObject.FindObjectOfType<Player>().Health} ";
    }
}
