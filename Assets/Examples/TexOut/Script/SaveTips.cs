using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTips : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float time = 0;
    float destroyTime = 2f;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= destroyTime)
            Destroy(gameObject);
    }
}
