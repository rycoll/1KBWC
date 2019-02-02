using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RE_Choice : RegularEffect {

    RunTimeValue ChoiceSet { get; set;}
    RunTimeValue Effect { get; set; } 

    public RE_Choice (RunTimeValue list, RunTimeValue cardEffect) {
        this.ChoiceSet = list;
        this.Effect = cardEffect;
    }

    public override void Run(GameController gameController) {
        ChoiceCallback callback = AcknowledgeChoice;
        List<object> list = ChoiceSet.Evaluate() as List<object>;
        if (list != null) {
            gameController.PresentChoice(list, callback);
        } else {
            AcknowledgeChoice(null, gameController);
        }
    }

    public void AcknowledgeChoice (object chosenObj, GameController gameController) {
        CardEffect effect = Effect.Evaluate() as CardEffect;
        if (effect != null) {
            effect.HandleInput(chosenObj);
        }
        Done(gameController);
    }

    public override void HandleInput(object obj) {
        if (!IgnoreInput) {
            CardEffect effectObj = obj as CardEffect;
            if (effectObj != null) {
                Effect = new RunTimeValue(effectObj);
                return;
            }
            List<object> listObj = obj as List<object>;
            if (listObj != null) {
                ChoiceSet = new RunTimeValue(listObj);
                return;
            }
        }
    }

    public static EffectData GetEffectData () {
        return new EffectData() {
            name = "Choose...",
            desc = "Let the player of this card choose an item from a list.",
            fields = new List<FieldType>(){FieldType.LIST},
            takesSubEffects = true
        };
    }
}