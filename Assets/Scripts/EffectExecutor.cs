using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectExecutor : MonoBehaviour
{
    private static GameController GameController;
    private const int MAX_EXECUTION_LOOPS = 1000;

    void Awake()
    {
        GameController = GetComponent<GameController>();
    }

    public static void BeginExecution (List<CardEffect> cardEffects) {
        if (cardEffects.Count == 0) {
            GameController.Next();
            return;
        }
        CardEffect curr = cardEffects[0];
        curr.SetNextEffect(null);
        for (int i = 1; i < cardEffects.Count; i++) {
            CardEffect next = cardEffects[i];
            curr.SetNextEffect(next);
            if (i == cardEffects.Count - 1) {
                next.SetNextEffect(null);
            } else {
                curr = next;
            }
        }
        Execute(cardEffects[0]);
    }
 
    public static void Execute (CardEffect effect) {
        if (typeof(ControlEffect).IsInstanceOfType(effect)) {
            // control effect
            ControlEffect control = (ControlEffect) effect;
            List<CardEffect> compiledEffects = control.Compile(GameController);
            CardEffect curr = effect;
            CardEffect final = effect.GetNextEffect();
            for (int i = 0; i < compiledEffects.Count; i++) {
                CardEffect next = compiledEffects[i];
                curr.SetNextEffect(next);
                if (i == compiledEffects.Count - 1) {
                    next.SetNextEffect(final);
                } else {
                    curr = next;
                }
            }
            effect.Done(GameController);
        } else {
            // regular effect
            ((RegularEffect) effect).Run(GameController);
        }
    } 

}
