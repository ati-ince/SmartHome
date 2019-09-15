

//#include "....."  pic16f913
#include "definitions.h"
#include "modbus.h"

eModBusState modBusState ; 


void ev_charReceived( char recvChar )
{
	switch (modBusState)
	{
		case STATE_NOT_INITIALIZED :
			restartTimers ( TIMER_T35 ) ; /*NORMALLY NOT VISITED*/
			//setError( ) ;
			break ;
		case STATE_INIT :
			restartTimers ( TIMER_T35 ) ;
			break ;
		case STATE_IDLE :
			saveChar (recvChar) ; /*Store first received byte*/
			enableTimers (TIMER_T15 | TIMER_T35) ;
			modBusState = STATE_RECEPTION ;
			break ;
		case STATE_EMISSION :
			//setError()
			/*Cannot receive character during emission state*/
			/*Inform master control if necessary*/
			/*!!!!!!!!!!!!*/
			break ;
		case STATE_RECEPTION :
			saveChar (recvChar) ; /*Store received byte*/
			restartTimers (TIMER_T15 | TIMER_T35) ; /*Restart both timers*/
			break ;
		case STATE_CONTROL :
			//setError()
			/*Cannot receive character during control state*/
			/*Inform master control if necessary*/
			/*!!!!!!!!!!!!*/
			break ;
		default:
			break ;
	}	
}

/*T15 Timer expired*/
void ev_timerT15Expired ( )
{
	switch (modBusState)
	{
		case STATE_NOT_INITIALIZED :
			break ;
		case STATE_INIT :
			//setError( ) ;
			break ;
		case STATE_IDLE :
			//setError( ) ;
			break ;
		case STATE_EMISSION :	
			//setError( ) ;
			break ;
		case STATE_RECEPTION :
			modBusState = STATE_CONTROL ;
			break ;
		case STATE_CONTROL :
			//setError( ) ;
			break ;
		default:
			break ;
	}	
}

/*T35 Timer expired*/
void ev_timerT35Expired ( )
{
	switch (modBusState)
	{
		case STATE_NOT_INITIALIZED :
			modBusState = STATE_INIT ;
			ENABLE_PERIPHERAL_INT() ;
			break ;
		case STATE_INIT :
			modBusState = STATE_IDLE ;
			disableTimers( TIMER_T35 ) ;
			break ;
		case STATE_IDLE :
			//setError( ) ;
			break ;
		case STATE_EMISSION :	
			modBusState = STATE_IDLE ;
			break ;
		case STATE_RECEPTION :
			//setError( ) ;
			break ;
		case STATE_CONTROL :
			modBusState = STATE_IDLE ;
			ReadyFlag = 1 ;
			break ;	
		default:
			break ;
	}	
}

void initModBus ()
{
	modBusSate = STATE_NOT_INITIALIZED ;
	
	/*First wait t35 and then go to STATE_INIT*/
	DISABLE_PERIPHERAL_INT() ;
	enableTimers( TIMER_T35 ) ;
	
}
void init_sys()
{
	/*INITIALIZATIONS OF REGISTERS AND PINS*/
	/*************
	/************
	*/	
	initTimers ( ) ;
	initModBus ()  ;
}

int main()
{
	init_sys() ;
	
	for(;;)
	{
		/*If frame is ready, process it*/
		if(ReadyFlag)
		{
			checkFrameAndResponse() ;
			ReadyFlag = 0 ;
		}
	}
}