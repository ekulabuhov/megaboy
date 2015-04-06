## Zero Flag (Z) ##
Is set only if last operation evaluates to zero.
## Carry Flag (C) ##
Overflow flag on other CPU`s, is set only when result wrapped.
Example:
```
u16 var=0xFFFF; // or u8 var=0xFF;
int value=1;
if ((var+value) < var) SetCarryFlag();
```
## Add/Sub Flag (N) ##
Indicates whether last instruction was substraction or addition.
## Half Carry Flag (H) ##
The same as Carry (C), but only for lower part of a number (last 4 bits).
```
u8 var=0xFF;
int value=1;
if ((var&0xF) + value) < (var&0xF)) SetHalfCarry();
```