using System;
using System.Collections.Generic;

[System.Serializable]
public class RE_SetMaxHandSize : RegularEffect {

    private RunTimeValue<GamePlayer> Player { get; set; }
    private RunTimeValue<int> Size { get; set; }

    public RE_SetMaxHandSize (RunTimeValue<GamePlayer> player, RunTimeValue<int> size) {
        Player = player;
        Size = size;
    }

    public override void Run(GameController gameController) {
        GamePlayer player = Player.Evaluate(gameController);
        player.Hand.MaxHandSize = Size.Evaluate(gameController);

        Done(gameController);
    }

    public static EffectData GetEffectData () {
        return new EffectData() {
            name = "Set Player's Max Hand Size",
            desc = "Set a player's maximum hand size.",
            fields = new List<FieldData>(){
                FieldLibrary.GetPlayerFieldData(), 
                FieldLibrary.GetNumberFieldData()
            },
            takesSubEffects = false,
            //effect = new RE_SetMaxHandSize()
        };
    }
}