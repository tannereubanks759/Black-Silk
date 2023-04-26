using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imageSc : MonoBehaviour
{
    public MainMenu menu;
    public UI ui;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseOver()
    {
        if(this.gameObject.name == "Play")
        {
            menu.TitleImage.SetActive(false);
            menu.PlayImage.SetActive(true);
        }
        else if(this.gameObject.name == "Exit")
        {
            menu.TitleImage.SetActive(false);
            menu.ExitImage.SetActive(true);
        }
    }
    private void OnMouseExit()
    {
        if (this.gameObject.name == "Play")
        {
            menu.TitleImage.SetActive(true);
            menu.PlayImage.SetActive(false);
        }
        else if (this.gameObject.name == "Exit")
        {
            menu.TitleImage.SetActive(true);
            menu.ExitImage.SetActive(false);
        }
    }
    private void OnMouseDown()
    {
        if (this.gameObject.name == "Play")
        {
            ui.LoadScene("SampleScene");
        }
        else if (this.gameObject.name == "Exit")
        {
            ui.Quit();
        }
    }
}
