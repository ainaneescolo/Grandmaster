using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{

    public static UI_Manager instance;

    [Header("----- Panels -----")]
    [SerializeField] private GameObject closed_envelope;
    [SerializeField] private GameObject open_envelope;
    [SerializeField] private GameObject intro_panel;
    [SerializeField] private GameObject main_menu_panel;
    [SerializeField] private GameObject options_panel;
    [SerializeField] private GameObject store_panel;

    private bool menu_isopen;
    private bool options_isopen;
    private bool store_isopen;
    
    [SerializeField] private TMP_Text postion_cost;

    public Texture2D cursor;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void ChangeSceneBtn()
    {
        StartCoroutine(ChangeScene());
    }
    
    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(0.5f);
        if (SceneManager.GetActiveScene().name.Equals("Main_Menu"))
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
    
    public void OpenMainMenu()
    {
        StartCoroutine(OpenMenu());
    }
    
    IEnumerator OpenMenu()
    {
        yield return new WaitForSeconds(0.2f);
        closed_envelope.SetActive(false);
        open_envelope.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        open_envelope.SetActive(false);
        intro_panel.SetActive(true);
    }

    public void NextMenuText()
    {
        intro_panel.SetActive(false);
        main_menu_panel.SetActive(true);
    }

    public void OpenCloseOptionsMenu()
    {
        if (options_isopen)
        {
            options_panel.SetActive(false);
            options_isopen = false;
        }
        else
        {
            options_panel.SetActive(true);
            options_isopen = true;
        }
    }
    
    public void OpenCloseMainMenu()
    {
        if (menu_isopen)
        {
            Time.timeScale = 1;
            main_menu_panel.SetActive(false);
            menu_isopen = false;
        }
        else
        {
            Time.timeScale = 0;
            main_menu_panel.SetActive(true);
            menu_isopen = true;
        }
    }
    
    public void OpenCloseStore()
    {
        if (store_isopen)
        {
            store_panel.SetActive(false);
            store_isopen = false;
        }
        else
        {
            store_panel.SetActive(true);
            store_isopen = true;
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(1);
        OpenCloseMainMenu();
    }
    
    public void ExitButton()
    {
        Application.Quit();
    }

    #region Potions

    public void QueenPotion()
    {
        postion_cost.text = $"4";
    }

    #endregion
}
