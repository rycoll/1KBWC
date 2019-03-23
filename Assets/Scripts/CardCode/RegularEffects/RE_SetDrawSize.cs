using System;
using System.Collections.Generic;

[System.Serializable]
public class RE_SetDrawSize : RegularEffect {

    private RunTimeValue<GamePlayer> Player { get; set; }
    private RunTimeValue<int> Size { get; set; }

    public RE_SetDrawSize (RunTimeValue<GamePlayer> player, RunTimeValue<int> size) {
        Player = player;
        Size = size;
    }

    public override void Run(GameController gameController) {
        GamePlayer player = Player.Evaluate(gameController);
        player.SetDrawPerTurn(Size.Evaluate(gameController));

        Done(gameController);
    }

    public static EffectData GetEffectData () {
        return new EffectData() {
            name = "Set Player's Draw Size",
            desc = "Change how many cards a player draws at the start of their turn (cannot be less than 1).",
            fields = new List<FieldData>(){
                FieldLibrary.GetPlayerFieldData(), 
                FieldLibrary.GetNumberFieldData()
            },
            takesSubEffects = false,
            //effect = new RE_SetDrawSize()
        };
    }
}