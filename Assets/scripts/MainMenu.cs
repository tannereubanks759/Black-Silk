using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject TitleImage;
    public GameObject PlayImage;
    public GameObject ExitImage;


    // Start is called before the first frame update
    void Start()
    {
        TitleImage.SetActive(true);
        PlayImage.SetActive(false);
        ExitImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
