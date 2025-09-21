using TMPro;
using UnityEngine;

public class DiamondCountManager : MonoBehaviour
{
    [SerializeField] TMP_Text diamondText;

    private int count;
    public void IncrementCount()
    {
        this.count++;
        diamondText.text = "Diamond : " + count.ToString();
    }
   
    void Start()
    {
        count = 0;
    }

}
