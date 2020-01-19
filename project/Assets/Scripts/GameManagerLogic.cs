using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerLogic : MonoBehaviour
{
    public float total;
    public Text total_Text;

    private void Update()
    {
        total_Text.text = total.ToString();

        if (total == 0)
        {
            StageClear();
        }
    }

    void StageClear()
    {
        
    }
}
