using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pointsPopup : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private Color posColor, negColor;

    public int score = 0;

    private void Update()
    {
        if (score != 0)
        {
            if(score > 0)
            {
                scoreText.text = "+" + score.ToString();
                scoreText.color = posColor;
            }
            else
            {
                scoreText.text = score.ToString();
                scoreText.color = negColor;
            }

            Invoke("selfDestruct", 0.6f);
        }
    }

    void selfDestruct()
    {
        Destroy(this.gameObject);
    }
}
