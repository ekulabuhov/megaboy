#ifndef UART_H_
#define UART_H_

// UART declarations;
void UART_Setup();
byte UART_getc();
void UART_gets();
void UART_putc(unsigned char txByte);

extern byte UART_buffer[];

#endif /*UART_H_*/
