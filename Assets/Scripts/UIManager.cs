using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class UIManager : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI RemainingRef = null;

    [SyncVar(hook = nameof(UIUpdate))]
    public int Score = 0;

    //Hook so when score is changed the UI is updated
    public void UIUpdate(int OldScore, int NewScore)
    {
       RemainingRef.text = "Score: " + NewScore;      
    }
}
