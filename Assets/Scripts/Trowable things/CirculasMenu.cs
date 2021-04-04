using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CirculasMenu : MonoBehaviour
{
    public Image[] circlePieces; 
    public List<MenuButton> buttons = new List<MenuButton>();
    private Vector2 _mousePosition;
    private readonly Vector2 _fromVector2M = new Vector2(.5f, 1f);
    private readonly Vector2 _centerCircle = new Vector2(.5f, .5f);
    private Vector2 _toVector2M;
    public int menuItems;
    public int curMenuItem;
    private int _oldMenuItem;

    private void Awake() 
    {
        curMenuItem = 0;
        _oldMenuItem = 0;
    }

    private void Update()
    {
        GetCurrentMenuItem();
        if(Input.GetKeyUp("l"))
            AddItem();
        if(Input.GetKey("k"))
            ButtonAction();
    }

    private void GetCurrentMenuItem()
    {
        _mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        _toVector2M = new Vector2(_mousePosition.x/Screen.width, _mousePosition.y/Screen.height);
        var angle = (Mathf.Atan2(_fromVector2M.y - _centerCircle.y, _fromVector2M.x - _centerCircle.x) - Mathf.Atan2(_toVector2M.y - _centerCircle.y, _toVector2M.x - _centerCircle.x)) * Mathf.Rad2Deg;
        if(angle < 0)
            angle += 360;
        if(menuItems > 0)
            curMenuItem = (int)(angle / (360 / menuItems));
        if (curMenuItem == _oldMenuItem) return;
        buttons[_oldMenuItem].sceneimage.color = buttons[_oldMenuItem].NormalColor;
        _oldMenuItem = curMenuItem;
        buttons[curMenuItem].sceneimage.color = buttons[curMenuItem].HighlightedColor;
    }
    

    public void AddItem()
    {
        if (menuItems >= circlePieces.Length) return;
        circlePieces[menuItems].gameObject.SetActive(true);
        buttons.Add(new MenuButton(menuItems));
        menuItems = buttons.Count;
        foreach (var piece in circlePieces)
        {
            piece.fillAmount = 1.0f / menuItems;
        }

        var rotZ = 180;
        for (int i = 0; i < circlePieces.Length; i++)
        {
            circlePieces[i].gameObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, rotZ);
            rotZ -= 360 / menuItems;
            buttons[i].sceneimage = circlePieces[i];
        }
        
        foreach(var button in buttons)
        {
            button.sceneimage.color = button.NormalColor;
        }
        
    }

    private void ButtonAction()
    {
        buttons[curMenuItem].sceneimage.color = buttons[curMenuItem].PressedColor;
        if(curMenuItem == 0)
            print("You have pressed the first button");
    }
}
[System.Serializable]
    public class MenuButton
    {
        public MenuButton(int name)
        {
            this.name = name.ToString();
        }
        
        public string name;
        public Image sceneimage;
        public Color NormalColor = Color.black;
        public Color HighlightedColor = Color.red;
        public Color PressedColor = Color.white;
    }
