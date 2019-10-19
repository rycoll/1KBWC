---
title: Auxiliary Bytes
---
*e.g. the `ENDIF` byte that needs to follow an `IF` instruction*

### How it works

Auxiliary bytes are specified in the `fields` of an effect definition, in `effects.json`. This is parsed and built into the effect object.

```js
fields: {
    "type": "BYTE_ENDLOOP",
    "desc": "Signals end of loop"
}
```

*Each auxiliary byte needs a corresponding ReturnType enum value*, so it can be correctly parsed.

### How to add

* Create a corresponding entry in the ReturnType enum. This should be prefixed with "BYTE_".
* Add the auxiliary byte as a field on the relevant effect(s) in `effects.json`.
* Add handling in `EnumRepresentation.EnumLookup`. This should return an EnumRepresentation with the correct Instruction and an empty string array.

### References in the code

* `effects.json`: field definitions
* ReturnType: one ReturnType enum value for each kind of auxiliary byte
* `EnumRepresentation.EnumLookup`: lookup for instruction
* `QuestionPanelController`: handling for auxiliary bytes during card creation (i.e. lookup and add instruction without presenting any unneeded information to the player).
