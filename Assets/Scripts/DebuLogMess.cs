using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuLogMess : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Log();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Log()
    {
#if Debug
        Debug.Log("fdsnmfds");
#endif
    }
}
