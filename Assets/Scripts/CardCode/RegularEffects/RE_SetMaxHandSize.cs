using System;
using System.Collections.Generic;

[System.Serializable]
public class RE_SetMaxHandSize : RegularEffect {

    private RunTimeValue Player { get; set; }
    private RunTimeValue Size { get; set; }

    public RE_SetMaxHandSize (RunTimeValue player, RunTimeValue size) {
        Player = player;
        Size = size;
    }

    public override void Run(GameController gameController) {
        GamePlayer player = Player.Evaluate() as GamePlayer;
        if (CheckTypeError(Player, player)) return;
        player.Hand.MaxHandSize = (int) Size.Evaluate();

        Done(gameController);
    }

    public override void HandleInput(object obj) {
        if (!IgnoreInput) {
            GamePlayer playerObj = obj as GamePlayer;
            if (playerObj != null) {
                Player = new RunTimeValue(playerObj);
                return;
            }
            try {
                Size = new RunTimeValue((int) obj);
                return;
            } catch (InvalidCastException) {}
        }
    }

    public static EffectData GetEffectData () {
        return new EffectData() {
            name = "Set Player's Max Hand Size",
            desc = "Set a player's maximum hand size.",
            fields = new List<FieldData>(){
                FieldLibrary.GetPlayerFieldData(), 
                FieldLibrary.GetNumberFieldData()
            },
            takesSubEffects = false
        };
    }
}