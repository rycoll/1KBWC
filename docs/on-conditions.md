---
title: On Conditions
layout: default
order_number: 5
---
## Condition Instructions

### Dilemma

For card creation and clear effect definitions, we want **multiple kinds of Condition instructions**. This allows us to present sensible options during card creation. For example:

* Compare Numbers
* Compare Bools

Other times, we want **a single, unified Condition instruction**. For example, an instruction like `IF` should be able to take any condition as a field, regardless of the type of condition, which is hard if there are multiple possible condition instructions. Here the condition type is best abstracted away in the effect definition.

### Solution

* Have a unified `Condition` instruction, that is used by effects like `IF`
* The unified `Condition` is never presented as an option in card creation, and never appears in compiled bytecode.
* Have multiple instructions for different kinds of conditions, whose corresponding effects *return a Condition*. These can be used nicely in card creation, and turn into `Condition` when interpreted, solving both use cases.
    * e.g. `BOOL_COMPARISON`, `NUM_COMPARISON`

