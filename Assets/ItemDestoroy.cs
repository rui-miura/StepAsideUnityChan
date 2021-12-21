using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestoroy : MonoBehaviour
{
    private GameObject mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        this.mainCamera = GameObject.Find("Main Camera");    
    }

    // Update is called once per frame
    void Update()
    {
        if(mainCamera.transform.position.z > this.transform.position.z)
        {
            Destroy(this.gameObject);
        }    
    }
}
