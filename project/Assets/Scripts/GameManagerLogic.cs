using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerLogic : MonoBehaviour
{
    public float total;
    public Text total_Text;
    public Text remainingShots;
    public Text totalShots;
    public Player player;

    private void Update()
    {
        total_Text.text = total.ToString();
        remainingShots.text = player.shots.ToString();

        if (total == 0)
        {
            StageClear();
        }
    }

    void StageClear()
    {
        
    }

    public void NoMoreShots()
    {

    }
}
