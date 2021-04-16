using UnityEngine;

namespace Player
{
    public class GUIController : MonoBehaviour
    {
        private void Update()
        { 
            ActivateKunaiBar(Input.GetKey("k"));
            if (!Input.GetKey("k"))
            {
                Time.timeScale = 1.0f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false; 
            }
        }

        [SerializeField] private GameObject kunaiBar;
        private void ActivateKunaiBar(bool isPressed)
        {
            kunaiBar.SetActive(isPressed);
            Time.timeScale = 0.2f;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            ScFPSController.CanMove = !isPressed;
        }
    }
}
