using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject deathMenu;
    public CharacterControllerScript controller;
    // Start is called before the first frame update
    void Start()
    {
        deathMenu.SetActive(false);
    }
    public void Die()
    {
        controller.cursorEnable();
        Time.timeScale = 0f;
        GameObject.Find("Canvas").GetComponent<pausescript>().enabled = false;
        Destroy(pauseMenu);
        controller.enabled = false;
        deathMenu.SetActive(true);
    }
}
