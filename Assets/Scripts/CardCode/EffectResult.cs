/* Set information for a card to determine what happens after it is played */
public class EffectResult {

    /* Destination: what happens to the card after it is played? */
    public enum Destination {FIELD, DISCARD, DESTROY};
    public Destination CardDestination { get; set; }
}