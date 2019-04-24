public enum CardEffectType {
    // control effects
    ForCardLoop, ForPlayerLoop, NumberLoop, If,

    // effects
    DrawCards,
    AddWinCondition, RemoveWinCondition,
    SetPoints, SetDrawSize, SetMaxHand,
    GiveTurn, SkipNextTurn,
    AddGlobalListener, 
    SetVariable, SetCounter, SetFlag,
    SendCard, CopyCard, SendToHand, CopyToHand,
    OOE
}