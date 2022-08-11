using UnityEngine;
using TMPro;
using DungeonCrawl.Actors.Characters;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI myText;

    void Update()
    {
        myText.text = $"Score: {Player.Score}";
    }
}
