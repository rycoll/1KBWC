---
title: Instructions
---
### How to add

* Add an entry to the Instruction enum in Instructions.cs.
    * Make sure to add this in the appropriate section. Don't put a setter in the 'accessor' range, for example.
* Add an entry to `effects.json` with all the appropriate details.
* If it can take fields, add a method in `InstructionFactory` or `LiteralFactory` for creating this instruction.
* Add handling in the `Interpreter`.
* Add handling in the `RulesTextInterpreter`.





