using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionObject : MonoBehaviour
{

    Renderer myRender;
    public float displayTime;

    private void OnEnable()
    {
        myRender = gameObject.GetComponent<Renderer>();
        displayTime = -1f;
    }
    // Update is called once per frame
    void Update()
    {
        if(displayTime > 0)
        {
            displayTime -= Time.deltaTime;
            myRender.enabled = true;
        }
        else
        {
            myRender.enabled = false;
        }
    }

    public void HitOcclude(float time)
    {
        displayTime = time;
        myRender.enabled = true;
    }
}
