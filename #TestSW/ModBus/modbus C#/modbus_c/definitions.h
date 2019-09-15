
/*General project wide definitions*/
#ifndef _DEFINITIONS_DK_H
#define _DEFINITIONS_DK_H

#ifndef TRUE
	#define TRUE	1
#endif

#ifndef FALSE
	#define FALSE	0
#endif

#ifndef ON
	#define ON	1
#endif

#ifndef OFF
	#define OFF	0
#endif

#define ENTER_CRITICAL_SECTION()	GIE = 0 ;
#define	EXIT_CRITICAL_SECTION()		GIE = 1 ;

#define ENABLE_PERIPHERAL_INT()		PEIE = 1 ;
#define DISABLE_PERIPHERAL_INT()	PEIE = 0 ;

typedef  volatile union {
  volatile struct{
	       unsigned bit0 :1;
	       unsigned bit1 :1;
	       unsigned bit2 :1;
	       unsigned bit3 :1;
	       unsigned bit4 :1;
	       unsigned bit5 :1;
	       unsigned bit6 :1;
	       unsigned bit7 :1;
	} bits ;
	volatile unsigned char allbits  ;
} VARIABLE ;

/*Initializes system*/
void init_sys() ;

#endif /*_DEFINITIONS_DK_H*/