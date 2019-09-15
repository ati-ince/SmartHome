using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class ZigBee4Noks
    {
    }
}

#region Gateway Serial SETUP
//Speed: 9600 Bps (DIP4=off) / 19200 Bps (DIP4=on)
//Data bits: 8
//Parity: None
//Stop bits: 2
//Flow control: None
///// Burada biz genellikle 9600 kullandik bundan onceki calismalarda, simdi de boyle devam edecegiz
#endregion

#region ModBus Spec and Register Explenation
//InputRegister (16 bit variable read only)
//InputStatus (1 bit variable read only)
//HoldingRegister (16 bit variable generally non volatile) 
//CoilStatus (1 bit variable)
#endregion

#region ModBus Function
//The function codes implemented into the Gateway are:
//01 - READ COIL STATUS
//02 - READ INPUT STATUS
//03 - READ HOLDING REGISTER
//04 - READ INPUT REGISTER
//05 - FORCE SINGLE COIL
//06 - PRESET SINGLE REGISTER
#endregion

#region Gateway Data
//24(+16)  InputRegister
//128    InputStatus
//14    HoldingRegister
//16    CoilStatus
#endregion

#region GATEWAY DEVICE INPUT REGISTERS (Sadece Okuma yapabiliyoruz, cesitli bilgileri)
//InputRegister[0]  Device Type (=112)
//InputRegister[1]  Firmware Version (Major/Minor)
//InputRegister[2]  Transmission Power (dB+100)
//InputRegister[3]  Network Channel (11-:-26)
//InputRegister[4]  Network PanId (0 -:- 32767)
//InputRegister[5]  Seconds from reset 
//InputRegister[6]  Counter of messages received from reset
//InputRegister[7]  Number of used agent slots (number of sensors)
//InputRegister[8]  Gateway Address
//InputRegister[9]  Gateway EUI64 (bytes 0,1)
//InputRegister[10]  Gateway EUI64 (bytes 2,3)
//InputRegister[11]  Wireless Signal Level of the last message received from Gateway (dB+100)
//InputRegister[12]  Number of Device connected through Router-Bridge
//InputRegister[13]  Number of End-Device children of Gateway
//InputRegister[14]  Number of Resets
//InputRegister[15]  Reset Type
//InputRegister[16]  Number of free Packet Buffer
//InputRegister[17]  Extended panID
//InputRegister[18]  Extended panID
//InputRegister[19]  Extended panID
//InputRegister[20]  Extended panID
//InputRegister[21]  Total Number of Routers present in the network
//InputRegister[22]  Total Number of Routers neighbours 
//InputRegister[23]  Number of good Routers neighbours 
//InputRegister[24]  Copy of InputStatus[00…15]
//InputRegister[25]  Copy of InputStatus[16…31] – Presence Sensors 16-31
//InputRegister[26]  Copy of InputStatus[32…47] – Presence Sensors 32-47
//InputRegister[27]  Copy of InputStatus[48…63] – Presence Sensors 48-63
//InputRegister[28]  Copy of InputStatus[64…79] – Presence Sensors 64-79
//InputRegister[29]  Copy of InputStatus[80…95] – Presence Sensors 80-95
//InputRegister[30]  Copy of InputStatus[96…111] – Presence Sensors 96-111
//InputRegister[31]  Copy of InputStatus[112…127] – Presence Sensors 112-127
//InputRegister[32]  Copy of InputStatus[128…143] – Data validity sensors 16-31
//InputRegister[33]  Copy of InputStatus[144…159] – Data validity sensors 32-47
//InputRegister[34]  Copy of InputStatus[160…175] – Data validity sensors 48-63
//InputRegister[35]  Copy of InputStatus[176…191] – Data validity sensors 64-79
//InputRegister[36]  Copy of InputStatus[192…207] – Data validity sensors 80-95
//InputRegister[37]  Copy of InputStatus[208…223] – Data validity sensors 96-111
//InputRegister[38]  Copy of InputStatus[224…239] – Data validity sensors 112-127
#endregion

#region GATEWAY DEVICE HOLDING REGISTERS (Bunu plug adresi degistirirken kullanmistik, ama tum ozellikleri simdilik bilinmemekte)
//HoldingRegister[0]  Command password (1)
//HoldingRegister[1]  Command password (2)
//HoldingRegister[2]  Command password (3)
//HoldingRegister[3]  Gateway working mode. Default value 21.
//HoldingRegister[4]  Absolute Time (100*hour + minutes). Resetted each 24 hours.
//HoldingRegister[5]  Period of transmission of regeneration routes message [sec] (default value=20sec)
//HoldingRegister[6]  Command password (4)
//HoldingRegister[7]  Command password (5)
//HoldingRegister[8]  Command password (6)
//HoldingRegister[9]  Command password (7)
//HoldingRegister[10]  Gateway Alternate Address (used if dip-switch=0, default value=1)
//HoldingRegister[11]  Minimum Address allowed for devices connected via Router-Bridge (default value =1)
//HoldingRegister[12]  Maximum Address allowed for devices connected via Router-Bridge (default value =247)
//HoldingRegister[13]  Password Z-HandZer special functions 
#endregion

#region GATEWAY DEVICE COIL STATUSES
//CoilStatus[0]  Activation of command password
//CoilStatus[1]  Not used
//…  …
//CoilStatus[15]  Not used
#endregion

#region GATEWAY DEVICE INPUT STATUSES 
//InputStatus[0]  Gateway Network State (0= disconnected, 1= connected to a Network)
//InputStatus[1]  Network closing/opening state (0= Network Close, 1= Network Open)
//InputStatus[2]  Not used
//…  …
//InputStatus[15]  Not used
//InputStatus[16]  Presence Sensor 16
//InputStatus[17]  Presence Sensor 17
//InputStatus[18]  Presence Sensor 18
//InputStatus[19]  Presence Sensor 19
//InputStatus[20]  Presence Sensor 20
//InputStatus[21]  Presence Sensor 21
//InputStatus[22]  Presence Sensor 22
//InputStatus[23]  Presence Sensor 23
//…  …
//InputStatus[i]  Presence Sensor i (i = 16-:-127)
//…  …
//InputStatus[120]  Presence Sensor 120
//InputStatus[121]  Presence Sensor 121
//InputStatus[122]  Presence Sensor 122
//InputStatus[123]  Presence Sensor 123
//InputStatus[124]  Presence Sensor 124
//InputStatus[125]  Presence Sensor 125
//InputStatus[126]  Presence Sensor 126
//InputStatus[127]  Presence Sensor 127
//InputStatus[128]  Data validity Sensor 16
//InputStatus[129]  Data validity Sensor 17
//InputStatus[130]  Data validity Sensor 18
//InputStatus[131]  Data validity Sensor 19
//InputStatus[132]  Data validity Sensor 20
//InputStatus[133]  Data validity Sensor 21
//InputStatus[134]  Data validity Sensor 22
//InputStatus[135]  Data validity Sensor 23
//…  …
//InputStatus[112+i]  Data validity Sensor i (i = 16-:-127)
//…  …
//InputStatus[232]  Data validity Sensor 120
//InputStatus[233]  Data validity Sensor 121
//InputStatus[234]  Data validity Sensor 122
//InputStatus[235]  Data validity Sensor 123
//InputStatus[236]  Data validity Sensor 124
//InputStatus[237]  Data validity Sensor 125
//InputStatus[238]  Data validity Sensor 126
//InputStatus[239]  Data validity Sensor 127
#endregion

#region SPECIAL COMMANDS - COMMAND PASSWORD (GATEWAY)
//[Command Name]                      [Command Description]                       [Value(dec)] [Value(hex)]
//OPEN_NETWORK                         Opens Network (like pressing push button)      5266         1492
//CLOSE_NETWORK                        Closes Network (like pressing push button)     5267         1493
//DEVICE_RESET                         Resets Gateway                                 6512         1970
//BROADCAST_ROUTER_RESET(unicast)      reset of all Routers                           16785        4191
//BROADCAST_ROUTER_DISASSOCIATION(u)   all Routers disassociation                     16787        4193
//BROADCAST_ROUTER_REINIT-NETWORK(u)   network re-initialize Broadcast Message        16793        4199
//BROADCAST_ROUTER_PING_QUERY(u)       Launches Broadcast Ping Query Message          20482        5002
#endregion

#region WORKING MODE , non volatile variable HoldingRegister[3](working mode) 
//Bit#0 of Working Mode – Timeout communication management
//Bit#1 of Working Mode – Exception response management
//Bit#2 of Working Mode – Transmission towards Router-Bridges management
//Bit#3 of Working Mode – Holding Register modality reading
//Bit#4 of Working Mode – Enabling access to the Routers Information Table
//Bit#5 of Working Mode – Serial Response Delay
//The default value for the Working Mode parameter is equal to 21 
//(Bit#0=1, Bit#1=0, Bit#2=1, Bit#3=0, Bit#4=1, Bit#5=0).
#endregion

#region Bit#0 of Working mode – Timeout communication management
//The  Gateway  continuously  monitors  the  time  elapsed  between  successive  messages 
//of all the devices belonging to its network (except for the devices connected to 
//Router-Bridge devices).

//If  the  time  elapsed  from  the  reception  of  the  last  message  is  more  than  four 
//time  the  automatic  transmission  time  of  the  device  (HoldingRegister[1]  for  all 
//ZB-Connection  devices)  then  the  sensor  data  must  be  considered  in  timeout 
//status.

//The  Gateway  behavior  with  reference  to  the  timeout  status  of  the  sensors  is 
//managed by bit#0 of Working Mode parameter.


//Working Mode, bit#0=0:
//The Gateway lets accessing the sensor data also in timeout status. In that case 
//the  information  of  “validity”  can  be  deducted  from  the  Presence  Flag  of  the 
//sensor itself (InputStatus[64]).


//Working Mode, bit#0=1:
//The  Gateway  doesn’t  let  access  the  sensor  data  when  the  sensor  is  in  timeout 
//status. A  possible  data  request  of  a  sensor  being  in  timeout  status  doesn’t  obtain 
//answer (or an error response is given).


//NB:
//If the time elapsed from the reception of the last message of a given sensor is 
//greater than 100 minutes, the Gateway deletes the relevant agent.
//In  that  case  whichever  is  the  status  of  Working  Mode  bit#0,  a  possible  data 
//request doesn’t obtain answer.
#endregion


