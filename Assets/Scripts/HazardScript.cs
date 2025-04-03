using UnityEngine;

public class HazardScript : MonoBehaviour
{
    
    void Update()
    {
         Vector3 pos = transform.position;
                pos.x -= (5*Time.deltaTime);
        transform.position = pos;
        if(transform.position.x <= -52)
        {
            Destroy(gameObject);
        }
    }

    
}
