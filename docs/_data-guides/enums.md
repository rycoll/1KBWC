---
title: Enums
---
*e.g. the `DECK_POSITION` enum used for `MOVE_TO_DECK`*

### How to add

* Add a new `Instruction`. Make sure its value falls in the right range for `ByteManager.NextInstructionIsAccessor`.
* Add instruction to `ByteManager.enumInstructions`, for use in CheckType()
* Add handling in `EnumRepresentation.EnumLookup`.