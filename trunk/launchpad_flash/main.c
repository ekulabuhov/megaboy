#include <msp430g2231.h>
#include "common.h"
#include "flash.h"
#include "UART.h"

void configureUartAndSendOneByte(unsigned short txByte)
{
	// This should set the frequency of DCO to 21 Mhz
	//DCOCTL |= DCO0 + DCO1 + DCO2;	// DCO = 7
	//BCSCTL1 |= RSEL0 + RSEL1 + RSEL2 + RSEL3; // RSEL = 15;
	
/*	// Blink LED1 once every second with 21Mhz DCO
	while (1)
	{
		P1OUT = 1;
		_delay_cycles(10000000); // 100000k - wait for 10Mhz while it's on
		P1OUT = 0;
		_delay_cycles(10000000); // another 10Mhz it's off
	}
*/
}

void main(void)
{
	int i = 0;
	 
	WDTCTL = WDTPW + WDTHOLD;	// Disable Watchdog
	
	UART_Setup();
	
	while(1)
	{		
		UART_gets();	// Read the data from UART to UART_buffer.
		
		if ((UART_buffer[0] == 'I') && (UART_buffer[1] == 'D'))
		{
			Flash_Setup();
			
			byte manufacturerCode = Flash_ReadSiliconId(MANUFACTURER_CODE);
			byte deviceId = Flash_ReadSiliconId(DEVICE_CODE);
			
			UART_Setup();
			byte buffer[5] = {0xd, 0, 0, 0xd, 0};
			buffer[1] = manufacturerCode;
			buffer[2] = deviceId;
			
			i = 0;
			while(buffer[i] != 0)
				UART_putc(buffer[i++]);
		}
		else if (UART_buffer[0] == 'R')	// R - is for Read. R0 will return 10 bytes from 0 to 9.
		{
			Flash_Setup();
			Flash_ReadData(UART_buffer[1]-0x30);
			
			UART_Setup();
			for (i=0; i<10; i++)
			{
				UART_putc(Flash_buffer[i]);
			}
		}
		else	
			while(1);
	}
}
