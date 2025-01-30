MONTREAL, 1974  
A hallway with a single door stretches endlessly.  

-> hallway

=== hallway ===
You stand before a single wooden door. It feels familiar, as though you've stood here before.  

+ [Open the door.] -> open_door

=== open_door ===
You reach for the brass doorknob and turn it. The door creaks open, revealing...  
- ...the same hallway. -> hallway_repeat
- ...a reflection of yourself. -> reflection
- ...nothing but darkness. -> darkness

=== hallway_repeat ===
You step through, only to find yourself back in the same hallway.  
+ [Open the door again.] -> open_door

=== reflection ===
Your reflection smiles at you. "Still trying?" it asks.  
+ "What is this place?" -> hallway_repeat

=== darkness ===
The void greets you with silence. You instinctively close the door and find yourself back in the hallway.  
-> hallway

