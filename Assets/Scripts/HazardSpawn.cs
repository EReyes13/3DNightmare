using UnityEngine;

public class HazardSpawn : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float Timer = 3;
    public GameObject prefab;

    void Update()
    {
        Timer -=Time.deltaTime;
        if(Timer <= 0.01)
        {
            Instantiate(prefab, transform.position,Quaternion.identity);
            Timer = 3;
        }
    }
}
