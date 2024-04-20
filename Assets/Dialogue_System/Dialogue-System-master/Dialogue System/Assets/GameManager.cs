using UnityEngine;

public class GameManager : MonoBehaviour
{
    public NPCDialogTrigger npcDialogTrigger;

    void Start()
    {
        // Change the dialog scene dynamically
        npcDialogTrigger.SetDialogScene("NewDialogScene");
    }
}
