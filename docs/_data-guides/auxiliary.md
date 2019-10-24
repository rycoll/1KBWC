---
title: Auxiliary Bytes
---
*e.g. the `ENDIF` byte that needs to follow an `IF` instruction*

### How it works

Auxiliary bytes are specified in the `fields` of an effect definition, in `effects.json`. This is parsed and built into the effect object.

Define an auxiliary byte by adding the "auxiliary" attribute. The type field needs to match the name of a corresponding instruction (case-insensitive).

```js
fields: {
    "type": "ENDLOOP",
    "desc": "Signals end of loop",
    "attributes": ["auxiliary"]
}
```

*Each auxiliary byte needs a corresponding ReturnType enum value*, so it can be correctly parsed.

### How to add

* Create a corresponding entry in the ReturnType enum.
* Add the byte as a field, with the `auxiliary` attribute, on the relevant effect(s) in `effects.json`.

### References in the code

* `effects.json`: field definitions
* ReturnType: one ReturnType enum value for each kind of auxiliary byte
* `QuestionPanelController`: handling for auxiliary bytes during card creation (i.e. lookup and add instruction without presenting any unneeded information to the player).
