using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDashBoard : MonoBehaviour
{
    public Image playerImg;
    public TextMeshProUGUI myBestFitnessTxt;
    public TMP_InputField decisionsInput;

    public void Set_Me(Color birdCol, string bestFitness, string decisionSeq)
    {
        playerImg.color = birdCol;
        myBestFitnessTxt.text = bestFitness;
        decisionsInput.text = decisionSeq;
    }


}
