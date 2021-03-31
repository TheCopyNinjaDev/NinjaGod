using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CirculasMenu : MonoBehaviour
{
    public List<MenuButton> buttons = new List<MenuButton>();
    private Vector2 _mousePosition;
    private Vector2 _fromVector2M = new Vector2(.5f, 1f);
    private Vector2 _centerCircle = new Vector2(.5f, .5f);
    private Vector2 _toVector2M;
    public int menuItems;
    public int curMenuItem;
    private int _oldMenuItem;

    private void Awake() 
    {
        menuItems = buttons.Count;
        foreach(MenuButton button in buttons)
        {
            button.sceneimage.color = button.NormalColor;
        } 
        curMenuItem = 0;
        _oldMenuItem = 0;
    }

    private void Update() 
    {
        GetCurrentMenuItem();
        if(Input.GetKey("k"))
            ButtonAction();
    }

    public void GetCurrentMenuItem()
    {
        _mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        _toVector2M = new Vector2(_mousePosition.x/Screen.width, _mousePosition.y/Screen.height);
        float angle = (Mathf.Atan2(_fromVector2M.y - _centerCircle.y, _fromVector2M.x - _centerCircle.x) - Mathf.Atan2(_toVector2M.y - _centerCircle.y, _toVector2M.x - _centerCircle.x)) * Mathf.Rad2Deg;
        if(angle < 0)
            angle += 360;
        curMenuItem = (int)(angle / (360 / menuItems));
        if(curMenuItem != _oldMenuItem)
        {
            buttons[_oldMenuItem].sceneimage.color = buttons[_oldMenuItem].NormalColor;
            _oldMenuItem = curMenuItem;
            buttons[curMenuItem].sceneimage.color = buttons[curMenuItem].HighlightedColor;
        }
    }

    public void ButtonAction()
    {
        buttons[curMenuItem].sceneimage.color = buttons[curMenuItem].PressedColor;
        if(curMenuItem == 0)
            print("You have pressed the first button");
    }
}
[System.Serializable]
    public class MenuButton
    {
        public string name;
        public Image sceneimage;
        public Color NormalColor;
        public Color HighlightedColor;
        public Color PressedColor;
    }
