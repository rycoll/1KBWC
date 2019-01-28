using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCardEditDetails : MonoBehaviour
{
    private string cardName = "";
    private string cardRules = "";

    public void SetRulesText (string s) {
        cardRules = s;
    }

    public void SetCardName (string s) {
        if (s.Length == 0) {
            ErrorMessage("Card name can't be empty!");
            return;
        }
        // TODO: check card name already in use
        cardName = s;
    }

    private void ErrorMessage (string e) {
        Debug.Log(e);
    }
}
