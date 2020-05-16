require ./lcd.fs

: c!video ( c c-addr -- )
  lcd-wait-vblank c! ;

code v! ( c c-addr -- )
  lcd-wait-vram,
  ps-over-de-nip,
  E [HL] ld,
  ps-drop,
end-code

code cmovemono ( c-from c-to u -- )
  [C] ->A-> E ld,  C inc,
  [C] ->A-> D ld,  C inc,

  C inc, BC push,
  [C] ->A-> B ld,  C dec,
  [C] ->A-> C ld,

  begin, H|L->A, #NZ while,
    [BC] ->A-> [DE] ld,
    DE inc,
    [BC] ->A-> [DE] ld, \ duplicate copied byte
    BC inc,
    DE inc,

    HL dec,
  repeat,

  BC pop, C inc,
  ps-drop,
end-code

code cmovevideo ( c-from c-to u -- )
  [C] ->A-> E ld,  C inc,
  [C] ->A-> D ld,  C inc,

  C inc, BC push,
  [C] ->A-> B ld,  C dec,
  [C] ->A-> C ld,

  begin, H|L->A, #NZ while,
    di,
    lcd-wait-vram,          \ cmove but with di, waitvram, ei,
    [BC] ->A-> [DE] ld,
    ei,
    BC inc,
    DE inc,

    HL dec,
  repeat,

  BC pop, C inc,
  ps-drop,
end-code
