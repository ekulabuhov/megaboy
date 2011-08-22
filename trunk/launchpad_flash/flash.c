#include <msp430g2231.h>
#include "common.h"
#include "flash.h"

// Serial input for shifters
#define SET_SI(x) P1OUT&=~BIT0; if(x) P1OUT|=BIT0;
// Serial clock for shifters
#define SET_SCLK(x) P1OUT&=~BIT1; if(x) P1OUT|=BIT1;
// Latch for shifters
#define SET_RCLK(x) P1OUT&=~BIT2; if(x) P1OUT|=BIT2;
// Mode selector for 299. Set to 1 for parallel load, set to 0 for right shifting.
#define SET_S1(x) if (!x) P1OUT&=~BIT3; else P1OUT|=BIT3
// LED2 on Launchpad
#define SET_LED2(x) P1OUT&=~BIT6; if(x) P1OUT|=BIT6;
// Button on Launchpad. Shared pin with S1.
#define BUTTON !(P1IN & BIT3)
// Write Enable (inverted) on Flash. Used as a clock between commands.
#define SET_WE(x) if (!x) P1OUT&=~BIT4; else P1OUT|=BIT4
// Output Enable (inverted) on Flash. Controls data lines.
#define SET_OE(x) if (!x) P1OUT&=~BIT5; else P1OUT|=BIT5
// Input from 299 shifter, used for reading input data from Flash Q0-Q7
#define Q7 (P1IN & BIT6)

#define USING_74HC299

void Flash_Setup()
{
	P1SEL = 0;
	
	P1DIR = 0xBF;	// Everything is output, except P1.6 0b10111111 = 0xBF
	P1OUT = 0;
	
	SET_WE(0);
	SET_OE(1);
	
	SET_S1(1); // Put 299 in parallel mode to make flash rom outputs active
	SET_WE(1);
	SET_OE(0);
	
	SET_S1(0);
	SET_OE(1);	
}

void outputParallel(byte value)
{
	#ifdef USING_74HC299
		// 299 has !OE1 and !OE2 that do the same as RCLK.
		// Setting these pins to high will disable outputs.
		SET_RCLK(1); 
	#endif
	
	byte bitNumber = 0;
	for (bitNumber = 0; bitNumber<8; bitNumber++)
	{
		SET_SCLK(0);
		
		byte byteMask = 0x80 >> bitNumber;
		if (value & byteMask)
		{
			SET_SI(1);
		}
		else
		{
			SET_SI(0);
		}
		
		SET_SCLK(1);
	}	
	
	SET_SCLK(0);
	SET_RCLK(1);
	SET_RCLK(0);	
}

byte readParallel(void)
{
	SET_S1(1);	// Enable parallel loading on 299
	SET_OE(0);	// Enable Flash ROM data outputs
	
	// Trigger clock to read Flash data to 299
	SET_SCLK(1);
	SET_SCLK(0);
	
	SET_S1(0);	// Enable right shifting on 299
	
	// Shift out the value stored in 299 (Flash data)
	byte bitNumber = 0;
	byte value = 0;
	for (bitNumber = 0; bitNumber<8; bitNumber++)
	{
		SET_SCLK(0);
		
		byte byteMask = 0x80 >> bitNumber;
		if (Q7)
		{
			value |= byteMask;
		}
		
		SET_SCLK(1);
	}
	
	return value;	
}

/*
 * Send read silicon ID command to Flash and return answer. 
 * For Macronix MX29L3211:
 * Manufacturer Code: 0xC2
 * Device Code: 0xF9
 */
byte Flash_ReadSiliconId(enum SiliconIdReadModes readMode)
{
	outputParallel(0x55); 	// A0-A7
	outputParallel(0x55); 	// A8-A15
	outputParallel(0xAA); 	// D0-D7
	SET_WE(0);				// Write Enable on FLASH is used as a clock
	_delay_cycles(20000);
	SET_WE(1);
	
	outputParallel(0xAA);
	outputParallel(0x2A);
	outputParallel(0x55);
	SET_WE(0);
	_delay_cycles(20000);
	SET_WE(1);
	
	outputParallel(0x55);
	outputParallel(0x55);
	outputParallel(0x90);
	SET_WE(0);
	_delay_cycles(20000);
	SET_WE(1);
	
	outputParallel(readMode);
	outputParallel(0x00);
	outputParallel(0x00);
	SET_WE(0);
	_delay_cycles(20000);
	SET_WE(1);
		
	return readParallel();
}

void resetToReadMode()
{
	outputParallel(0x55); 	// A0-A7
	outputParallel(0x55); 	// A8-A15
	outputParallel(0xAA); 	// D0-D7
	SET_WE(0);
	_delay_cycles(20000);
	SET_WE(1);
	
	outputParallel(0xAA);
	outputParallel(0x2A);
	outputParallel(0x55);
	SET_WE(0);
	_delay_cycles(20000);
	SET_WE(1);
	
	outputParallel(0x55);
	outputParallel(0x55);
	outputParallel(0xF0);
	SET_WE(0);
	_delay_cycles(20000);
	SET_WE(1);
}

byte Flash_buffer[10];

/*
 * Reads pages of 10 bytes;
 */
void Flash_ReadData(int page)
{
	int i = 0;
	page = page * 10;
	resetToReadMode();
	
	for (; i < 10; i++)
	{			
		outputParallel(page+i); 	// A0-A7
		outputParallel(0x00); 	// A8-A15
		outputParallel(0x00); 	// D0-D7
		
		byte value = readParallel();
		
		Flash_buffer[i] = value;
	}	
}

