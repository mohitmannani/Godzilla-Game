using UnityEngine;
using UnityEngine.UI;  // Make sure to include this line

public class QuitButton : MonoBehaviour
{
    private Button quitButton;

    private void Start()
    {
        // Find the Button component on the same GameObject
        quitButton = GetComponent<Button>();

        // Add a listener to the button click event
        quitButton.onClick.AddListener(QuitToDesktop);
    }

    public void QuitToDesktop()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
