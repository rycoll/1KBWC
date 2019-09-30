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
    GET_ACTIVE_PLAYER       = 040,
    GET_ALL_PLAYERS         = 041,
    GET_ALL_OPPONENTS       = 042,
    GET_CARDS_IN_DECK       = 043,
    GET_CARDS_IN_DISCARD    = 044,
    GET_CARDS_IN_HAND       = 045,
    GET_PLAYER              = 046,
    GET_PLAYER_POINTS       = 047,
    MAKE_CONDITION_BOOL     = 048,
    MAKE_CONDITION_NUMBER   = 049,
    TARGET_PLAYER           = 050,
    TARGET_CARD             = 051,
    READ_COUNTER            = 052,

    // external primitives (setters)
    SET_PLAYER_POINTS           = 060,
    INCREMENT_PLAYER_POINTS     = 061,
    SET_PLAYER_DRAW             = 062,
    SET_PLAYER_MAX_HAND         = 063,
    PLAYER_DRAW_CARD            = 064,
    SET_COUNTER                 = 065,
    MOVE_TO_DISCARD             = 066,
    MOVE_TO_DECK                = 067,

    // enums
    ENUM_DECK_POSITION          = 200,
    ENUM_CONDITION_OPERATOR     = 201,

    ERROR = 255
}
