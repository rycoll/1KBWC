public enum Instruction {
    // literals
    EFFECT_DELIMITER    = 000,
    INT                 = 001,
    STRING              = 002,
    BOOL                = 003,
    LIST                = 004,
    LIST_ITEM           = 005,
    PLAYER              = 006,
    CARD                = 007,
    CONDITION           = 008,
    LISTENER            = 009,
    COLOUR              = 010,
    CHUNK               = 011,
    PLACEHOLDER         = 012,

    // control
    IF                  = 020,
    ENDIF               = 021,
    LOOP                = 022,
    FOR_LOOP            = 023,
    ENDLOOP             = 024,

    // getter primitives
    //   internal
    ADD                 = 030,
    MULTIPLY            = 031,
    LIST_LENGTH         = 032,
    RANDOM_NUMBER       = 033,

    //   external
    GET_ACTIVE_PLAYER   = 040,
    GET_PLAYER          = 041,
    GET_PLAYER_POINTS   = 042,
    TARGET_PLAYER       = 043,
    TARGET_CARD         = 044,
    READ_COUNTER        = 045,

    // external primitives (setters)
    SET_PLAYER_POINTS           = 060,
    INCREMENT_PLAYER_POINTS     = 061,
    SET_PLAYER_DRAW             = 062,
    SET_PLAYER_MAX_HAND         = 063,
    PLAYER_DRAW_CARD            = 064,
    SET_COUNTER                 = 065,
    MOVE_TO_DISCARD             = 066,
    MOVE_TO_DECK                = 067,


    ERROR = 255
}
