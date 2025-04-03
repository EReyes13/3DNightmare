using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public TextMeshProUGUI Timer;
    public float Seconds = 0;

    public static bool Begin;
    // Update is called once per frame
    void Start()
    {
        Begin = true;
    }
    void Update()
    {
        if(Begin)
        {
            Seconds += Time.deltaTime;

            Timer.text = ("Time Elapsed: " + Seconds.ToString("F2"));
        }
        else
        {
            Seconds = 0;
        }
    }
}
