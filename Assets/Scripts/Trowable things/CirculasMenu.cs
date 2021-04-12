using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CirculasMenu : MonoBehaviour
{
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


    private void AddItem()
    {
        /* TO-DO
         place image of new kunai or smth*/
        
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
