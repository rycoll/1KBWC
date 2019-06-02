public enum Instruction {
    // literals
    EFFECT_DELIMITER    = 0x00,
    INT                 = 0x01,
    STRING              = 0x02,
    BOOL                = 0x03,
    LIST                = 0x04,
    LIST_ITEM           = 0x05,
    PLAYER              = 0x06,
    CARD                = 0x07,
    CONDITION           = 0x08,
    LISTENER            = 0x09,
    COLOUR              = 0x10,
    CHUNK               = 0x18,
    PLACEHOLDER         = 0x19,

    // internal primitives
    ADD                 = 0x20,
    MULTIPLY            = 0x21,
    LIST_LENGTH         = 0x22,
    IF                  = 0x23,
    ENDIF               = 0x24,
    LOOP                = 0x25,
    FOR_LOOP            = 0x26,
    ENDLOOP             = 0x27,

    // external primitives (getters)
    GET_ACTIVE_PLAYER   = 0x40,
    GET_PLAYER          = 0x41,
    TARGET_PLAYER       = 0x42,
    TARGET_CARD         = 0x43,
    READ_COUNTER        = 0x44,

    // external primitives (setters)
    SET_PLAYER_POINTS           = 0x60,
    INCREMENT_PLAYER_POINTS     = 0x61,
    SET_PLAYER_DRAW             = 0x62,
    SET_PLAYER_MAX_HAND         = 0x63,
    PLAYER_DRAW_CARD            = 0x64,
    SET_COUNTER                 = 0x65,
    MOVE_TO_DISCARD             = 0x66,
    MOVE_TO_DECK                = 0x67,


    ERROR = 0xFF
}
