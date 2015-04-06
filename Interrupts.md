# Details #
When implementing timing, you`ll probably divide real cycles by 4. In this document, if not specified different, than the value is divided.

## Screen renderer ##
  1. Line rendering starts from mode 1, sprite memory processing happens at that time, mode longs 80 CPU cycles.
  1. Renderer goes in mode 2 and reads data from video memory, time spent in this mode depends on sprite count in current line, and will vary from 169 to 376 CPU cycles.
  1. Interrupt signal indicating end of line draw (horizontal blank) is sent every 456 CPU cycles.

Modes cycle from 1 to 3, until screen renderer goes off the screen (indicated by LY register when it is equal to 144), then mode 0 is enabled. LY is still increased every 114 cycles.

## LCD is OFF ##
When LCD is switched OFF, probably when something writes 0 to 7th bit of LCD control register, some actions that would happen normally, don`t happen anymore. Includes:
**LY updates (LY = 0 at that moment)** Modes don`t change (stays at H-Blank?)


# Period Lengths #
| **Mode** | **Divided** | **Real** |
|:---------|:------------|:---------|
| 0 **V-Blank** |1140|4560|
| 1 **OAM** |19.25-20.75|77-83|
| 2 **VRAM** |42.25-43.75|169-175|
| 3 **H-Blank** |50.25-51.75|201-207|

114 ( thats 456 in real ) cycles needed to render one line. After H-Blank LY increases by 1, then loops to OAM again.