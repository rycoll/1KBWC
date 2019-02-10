using System;
using System.Collections.Generic;

[System.Serializable]
public class RE_SetDrawSize : RegularEffect {

    private RunTimeValue Player { get; set; }
    private RunTimeValue Size { get; set; }

    public RE_SetDrawSize (RunTimeValue player, RunTimeValue size) {
        Player = player;
        Size = size;
    }

    public override void Run(GameController gameController) {
        GamePlayer player = Player.Evaluate() as GamePlayer;
        if (CheckTypeError(Player, player)) return;
        player.SetDrawPerTurn((int) Size.Evaluate());

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
            name = "Set Player's Draw Size",
            desc = "Change how many cards a player draws at the start of their turn (cannot be less than 1).",
            fields = new List<FieldData>(){
                FieldLibrary.PlayerFieldData, 
                FieldLibrary.NumberFieldData
            },
            takesSubEffects = false
        };
    }
}