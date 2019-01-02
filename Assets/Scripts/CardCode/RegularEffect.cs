using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularEffect : CardEffect {

    public RunTimeValue Target_Ref { get; set; }
    public ActionEnum Action { get; set; }

    public RegularEffect(ActionEnum effectType, RunTimeValue effectTarget) {
        Action = effectType;
        Target_Ref = effectTarget;
    }
}

public enum ActionEnum {
    // player
    ADD_TO_HAND, SET_POINTS, SET_HAND_CAP, SET_DRAW_SIZE, SET_WIN_CONDITION,
    // card, playedcard
    ADD_TAG, COPY_TO, DESTROY, DISCARD, SEND_TO_HAND,
    // playedcard
    CHANGE_OWNER, DUPLICATE,
    // null
    SET_FLAG, SET_COUNTER, SET_VAR,
    TOGGLE_TURN_ORDER, SET_PLAYER_TURN,
    ADD_LISTENER, ROLL_DICE, SHUFFLE_DECK, OOE
};