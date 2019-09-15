
s=serial('COM16');
set(s, 'BaudRate', 9600, 'DataBits', 8);

fopen(s);
fprintf(s,'*IDN?'); % sending querry manufacturer's name,model name, serial number and firmware version
out=fscanf(s) % reading querry
fprintf(s,'REMOTE;')

 fprintf(s,'MEAS:VOLT A?') % voltage read

volt=fscanf(s,'%f') 
% 
% 
fprintf(s,'LOAD 1'); %open
% 
fprintf(s,'CURR:A?')
 curr=fscanf(s,'%f') 
%  
 fprintf(s,'CURR:A 2.0'); % current setting 10.1
% 
 fclose(s);
 
 for i=1:length(current_profile_with_errors)     
     cmd=sprintf('CURR:A %d',current_profile_with_errors(i));
      fprintf(s,cmd);
      pause(1.2);
      second=i
 end
 
 
 