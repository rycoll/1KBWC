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
    BOOL_COMPARISON         = 048,
    NUM_COMPARISON          = 049,
    TARGET_PLAYER           = 050,
    TARGET_CARD             = 051,
    READ_COUNTER            = 052,

    // enums
    ENUM_DECK_POSITION          = 060,
    ENUM_CONDITION_OPERATOR     = 061,
    ENUM_CONDITION_TYPE         = 062,

    // external primitives (setters)
    SET_PLAYER_POINTS           = 070,
    INCREMENT_PLAYER_POINTS     = 071,
    SET_PLAYER_DRAW             = 072,
    SET_PLAYER_MAX_HAND         = 073,
    PLAYER_DRAW_CARD            = 074,
    SET_COUNTER                 = 075,
    MOVE_TO_DISCARD             = 076,
    MOVE_TO_DECK                = 077,



    ERROR = 255
}
