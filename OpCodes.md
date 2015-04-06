# Introduction #

Some details that I haven't found covered.


# Details #

## BIT b,r ##
> ### Description ###
> > Test bit b in register r.

> ### Use with ###
> > b = 0 - 7, r = B,C,D,E,H,L,(HL),A

> ### Flags affected ###
> > Z - Set if bit b of register r is 0.<br>
<blockquote>N - Reset.<br>
H - Set.<br>
C - Not affected.<br>
</blockquote><blockquote><h3>Opcodes</h3>
<blockquote>0xCB - first byte - is the identifier of all bit operations. <br>
Next byte in range 0x40 - 0x7F carries b and r parameter.<br>
Bit description (e.g. 0x40 - 0b01000000):<br>
01 (2 bits) - test bit command;<br>
000 (3 bits) - bit parameter;<br>
000 (3 bits) - register parameter (ordered: B,C,D,E,H,L,(HL),A); <br>