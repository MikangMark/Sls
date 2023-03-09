using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject MainView_Pnl;
    public GameObject btn_Pnl;
    public GameObject gameStart_Pnl;
    public GameObject charSelect_Pnl;
    public Image pickView_Img;

    public Button gameStart_Btn;
    public Button ingame_Btn;

    public Sprite[] charPick_Img;

    public TextMeshProUGUI selectChar_Tmp;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("CharType", -1);
        btn_Pnl.SetActive(true);
        gameStart_Pnl.SetActive(false);
        charSelect_Pnl.SetActive(false);
        pickView_Img.gameObject.SetActive(false);
        ingame_Btn.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick_GameStart()
    {
        gameStart_Pnl.SetActive(true);
        btn_Pnl.SetActive(false);
    }
    public void OnClick_Back()
    {
        gameStart_Pnl.SetActive(false);
        btn_Pnl.SetActive(true);
    }

    public void OnClick_Nomal()
    {
        pickView_Img.gameObject.SetActive(false);
        gameStart_Pnl.SetActive(false);
        charSelect_Pnl.SetActive(true);
        selectChar_Tmp.gameObject.SetActive(true);
    }

    public void OnClick_CharPick(int charNum)
    {
        pickView_Img.gameObject.SetActive(true);
        pickView_Img.sprite = charPick_Img[charNum];
        selectChar_Tmp.gameObject.SetActive(false);
        PlayerPrefs.SetInt("CharType", charNum);
        ingame_Btn.gameObject.SetActive(true);
    }

    public void OnClick_BackCharPick()
    {
        pickView_Img.gameObject.SetActive(false);
        gameStart_Pnl.SetActive(true);
        charSelect_Pnl.SetActive(false);
        selectChar_Tmp.gameObject.SetActive(false);
        PlayerPrefs.SetInt("CharType", -1);
        ingame_Btn.gameObject.SetActive(false);
    }

    public void OnClick_Ingame()
    {
        SceneManager.LoadScene(1);
    }
}
