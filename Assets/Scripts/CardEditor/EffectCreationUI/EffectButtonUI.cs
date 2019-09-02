using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectButtonUI : EffectUIElement
{
    public Text questionText;
    public Text answerText;

    public void SetQuestionText (string text) {
        this.questionText.text = text;
    }

    public void SetAnswerText (string text) {
        this.answerText.text = text;
    }
}
