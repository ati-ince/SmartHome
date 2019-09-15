

function crc=CRCSTR(str);
crc=uint16(hex2dec('FFFF'));
a001=uint16(hex2dec('1021'));

str='123456789';

for i=1:length(str)
    Byteofmessage=(str(i));
    crc=bitxor(crc,real(Byteofmessage));
    for j=1:8
        droppedbit=bitget(crc,16);
        crc=bitshift(crc,-1);
        if droppedbit;
            crc=bitxor(crc,a001)
        end
    end
end
crc
disp(dec2hex(crc))
