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

    // control
    IF                  = 020,
    UNLESS              = 021,
    ENDIF               = 022,
    LOOP                = 023,
    FOR_LOOP            = 024,
    ENDLOOP             = 025,

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
    GET_PLAYER_POINTS       = 046,
    BOOL_COMPARISON         = 047,
    IS_TRUE                 = 048,
    IS_FALSE                = 049,
    NUM_COMPARISON          = 050,
    TARGET_PLAYER           = 051,
    TARGET_OPPONENT         = 052,
    TARGET_CARD_IN_DECK     = 053,
    TARGET_CARD_IN_DISCARD  = 054,
    TARGET_CARD_IN_HAND     = 055,
    READ_COUNTER            = 056,
    PLACEHOLDER             = 057,
    CARD_HAS_TAG            = 058,
    PLAYER_IS_WINNING       = 059,
    PLAYER_IS_LOSING        = 060,
    RANDOM_PLAYER           = 061,
    RANDOM_OPPONENT         = 062,
    RANDOM_CARD_IN_DECK     = 063,
    RANDOM_CARD_IN_DISCARD  = 064,
    RANDOM_CARD_IN_HAND     = 065,

    // enums
    ENUM_DECK_POSITION          = 080,
    ENUM_CONDITION_OPERATOR     = 081,
    ENUM_CONDITION_TYPE         = 082,
    ENUM_LIST_TYPE              = 083,

    // external primitives (setters)
    SET_PLAYER_POINTS           = 100,
    INCREMENT_PLAYER_POINTS     = 101,
    SET_PLAYER_DRAW             = 102,
    SET_PLAYER_MAX_HAND         = 103,
    PLAYER_DRAW_CARD            = 104,
    SET_COUNTER                 = 105,
    MOVE_TO_DISCARD             = 106,
    MOVE_TO_DECK                = 107,
    FLAVOUR_TEXT                = 108,

    // for internal logic (hidden)
    ADD_TO_REGISTER             = 200,

    ERROR = 255
}
