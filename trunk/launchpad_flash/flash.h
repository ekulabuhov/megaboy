#ifndef FLASH_H_
#define FLASH_H_

enum SiliconIdReadModes {MANUFACTURER_CODE = 0, DEVICE_CODE = 1};

void Flash_Setup();
byte Flash_ReadSiliconId(enum SiliconIdReadModes readMode);
void Flash_ReadData(int offset);

extern byte Flash_buffer[];

#endif /*FLASH_H_*/
