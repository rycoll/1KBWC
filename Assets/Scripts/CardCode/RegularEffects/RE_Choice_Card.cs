using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RE_Choice_Card : RegularEffect {

    RunTimeValue<List<Card>> ChoiceSet { get; set;}
    Q_Dynamic_Card LinkedQuery { get; set; }

    public RE_Choice_Card (RunTimeValue<List<Card>> list, Q_Dynamic_Card query) {
        this.ChoiceSet = list;
        this.LinkedQuery = query;
    }

    public override void Run(GameController gameController) {
        ChoiceCallback callback = AcknowledgeChoice;
        List<Card> list = ChoiceSet.Evaluate(gameController);
        if (list != null) {
            gameController.PresentChoiceOfCards(list, callback);
        } else {
            AcknowledgeChoice(null, gameController);
        }
    }

    public void AcknowledgeChoice (object chosenObj, GameController gameController) {
        Card chosenCard = chosenObj as Card;
        if (chosenCard != null) {
            LinkedQuery.DynamicCard = chosenCard;
        }
        Done(gameController);
    }

    public static EffectData GetEffectData () {
        return new EffectData() {
            name = "Choose...",
            desc = "Let the player of this card choose an item from a list.",
            fields = new List<FieldData>(){
                FieldLibrary.GetListFieldData()
            },
            takesSubEffects = true,
            //effect = new RE_Choice()
        };
    }
}