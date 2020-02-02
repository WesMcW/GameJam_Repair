using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerCount : MonoBehaviour
{
    public int players = 2;
    public Slider numPlay;
    private void Update()
    {
        players = (int)numPlay.value;
    }
    public void NumPlayers(int players)
    {
        Debug.Log("Number of players" + players);
    }
}
