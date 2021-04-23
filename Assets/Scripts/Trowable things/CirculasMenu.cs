using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Trowable_things
{
    public class CirculasMenu : MonoBehaviour
    {
        public List<MenuButton> buttons = new List<MenuButton>();
        public GameObject[] kunais;
        private Vector2 _mousePosition;
        private readonly Vector2 _fromVector2M = new Vector2(.5f, 1f);
        private readonly Vector2 _centerCircle = new Vector2(.5f, .5f);
        private Vector2 _toVector2M;
        public int menuItems;
        public static int CurMenuItem;
        private int _oldMenuItem;
    
        /*[Temporary zone]*/
        [SerializeField] private Image kunaiSelected;
        [SerializeField] private Color[] kunaiColorUI;

        private void Awake() 
        {
            CurMenuItem = 0;
            _oldMenuItem = 0;
            FightSystem.Kunai = kunais[0];
            gameObject.SetActive(false);
        }

        private void Update()
        {
            GetCurrentMenuItem();
            if(Input.GetMouseButton(0))
                ButtonAction();
        
        
        }

        private void GetCurrentMenuItem()
        {
            _mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            _toVector2M = new Vector2(_mousePosition.x/Screen.width, _mousePosition.y/Screen.height);
            var angle = (Mathf.Atan2(_fromVector2M.y - _centerCircle.y, _fromVector2M.x - _centerCircle.x) 
                         - Mathf.Atan2(_toVector2M.y - _centerCircle.y, _toVector2M.x - _centerCircle.x)) * Mathf.Rad2Deg;
            if(angle < 0)
                angle += 360;
            if(menuItems > 0)
                // ReSharper disable once PossibleLossOfFraction
                CurMenuItem = (int)(angle / (360 / menuItems));
            if (CurMenuItem == _oldMenuItem) return;
            buttons[_oldMenuItem].sceneimage.color = buttons[_oldMenuItem].normalColor;
            _oldMenuItem = CurMenuItem;
            buttons[CurMenuItem].sceneimage.color = buttons[CurMenuItem].highlightedColor;
        }


        private void AddItem()
        {
            /* TO-DO
         place image of new kunai or smth*/
        
        }
    
        // Click response
        private void ButtonAction()
        {
            buttons[CurMenuItem].sceneimage.color = buttons[CurMenuItem].pressedColor;  // Color change
            FightSystem.Kunai = kunais[CurMenuItem]; // The action
        
        
            //[Temporary zone] showing the color of selected kunai
            kunaiSelected.color = kunaiColorUI[CurMenuItem];
        
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
        [FormerlySerializedAs("NormalColor")] public Color normalColor = Color.black;
        [FormerlySerializedAs("HighlightedColor")] public Color highlightedColor = Color.red;
        [FormerlySerializedAs("PressedColor")] public Color pressedColor = Color.white;
    }
}