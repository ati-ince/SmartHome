

function [crc lowByte highByte]=CRCREAL(str);
crc=uint16(hex2dec('FFFF'));
polynomial=uint16(hex2dec('a001'));
%str=[202 3 11 185 0 2];


for i=1:length(str);
    %Byteofmessage=(str(i));
    crc=bitxor(crc,str(i));
    
    for j=1:8;
        
        if bitand(crc,1)
            crc=bitshift(crc,-1);
            crc=bitxor(crc,polynomial);
            
        else
            crc=bitshift(crc,-1);
            
              
        end
        
    end
end

lowByte=bitand(crc,hex2dec('ff'))
highByte=bitshift(bitand(crc,hex2dec('ff00')),-8)


crc
disp(dec2hex(crc)) 
