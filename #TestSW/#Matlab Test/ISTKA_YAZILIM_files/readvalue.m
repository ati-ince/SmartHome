
function value=readvalue(device_adrs, parameter_adrs);
%global s; 
s=serial('COM1','BaudRate', 19200, 'Timeout', 1)
fopen(s);
message(1)=device_adrs;
message(2)=3; % function type(only read)
message(3)=bitshift(bitand(parameter_adrs,hex2dec('ff00')),-8)       
message(4)= bitand(parameter_adrs,hex2dec('ff')); 
message(5)=0; %quantitiy of high byte of input
message(6)=2;%quantitiy of low byte of input
[crc lowByte highByte ]= CRCREAL(message);

message(7)=lowByte;
message(8)=highByte;

message

fwrite(s,message)

pause(0.1);

reply=fread(s)

fclose(s);
delete(s);
typecast(uint8(reply(4:7)),'single') %Alýnan deðerlerin gerçek deðere dönüþümü