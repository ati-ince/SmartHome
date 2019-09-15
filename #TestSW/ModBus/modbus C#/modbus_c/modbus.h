
#ifndef _MODBUS_DK_H
#define _MODBUS_DK_H


/*Device Address*/
/* NOTE: 0       --> Broadcast
         1-247   --> Slave Adresses
		 248-255 --> Reserved
*/
#define DEVICE_ADDRESS	0x10

/*Timer Type Definitions*/
#define TIMER_T15	0x01 /*To be used in enable and disableTimers functions*/
#define TIMER_T35	0x02 /*To be used in enable and disableTimers functions*/

VARIABLE ControlRegister = 0;
#define ReadyFlag 				ControlRegister.bits.bit0
//#define AnotherFlag 			ControlRegister.bits.bit1...7



/*ModBus States*/ 
typedef enum
{
	STATE_NOT_INITIALIZED,
    STATE_INIT,
    STATE_IDLE,
    STATE_EMISSION,
	STATE_RECEPTION,
	STATE_CONTROL
}	eModBusState;

/*Initializes modbus system*/
void initModBus ( ) ;

/*Initializes T15 and T35 timers*/
void initTimers ( ) ;

/*
* Enables timers requested
* timerType:  TIMER_T15 -->Only TimerT15 enabled
*   		  TIMER_T35 -->Only TimerT35 enabled
*             TIMER_T35 | TIMER_T15 --> Both timers enabled 
*/
void enableTimers  ( char timerType ) ;

/*
* Restarts timers requested
* timerType:  TIMER_T15 -->Only TimerT15 restarted
*   		  TIMER_T35 -->Only TimerT35 restarted
*             TIMER_T35 | TIMER_T15 --> Both timers restarted 
*/
void restartTimers ( char timerType ) ;

/*
* Disables timers requested
* timerType:  TIMER_T15 -->Only TimerT15 disabled
*   		  TIMER_T35 -->Only TimerT35 disabled
*             TIMER_T35 | TIMER_T15 --> Both timers disabled 
*/
void disableTimers ( char timerType ) ;

/*EVENTS to be used in state machine and ISR*/
/*A char was received*/
void ev_charReceived( char recvChar ) ;

/*T15 Timer expired*/
void ev_timerT15Expired ( ) ;

/*T35 Timer expired*/
void ev_timerT35Expired ( ) ;

#endif /*_MODBUS_DK_H*/