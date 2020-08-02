using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScript : MonoBehaviour
{
    public GameObject earth;
    public GameObject mars;
    
    public void OnclickEarth()
    {
        earth.SetActive(true);
        mars.SetActive(false);
    }
    public void OnclickMars()
    {
        earth.SetActive(false);
        mars.SetActive(true);
    }
}
