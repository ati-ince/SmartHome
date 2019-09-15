%  Type: CRC Detector
%           Polynomial: 0xF
%         InitialState: 0xF
%         ReflectInput: true
%     ReflectRemainder: false
%             FinalXOR: 0x0

% Create a CRC-16 CRC generator, then use it to generate
% a checksum for the
% binary vector represented by the
% ASCII sequence '123456789'.
% Introduce an error, then detect it
% using a CRC-16 CRC detector.
gen = crc.generator('Polynomial', '0x8005', 'ReflectInput', ...
true, 'ReflectRemainder', true);
det = crc.detector('Polynomial', '0x8005', 'ReflectInput', ...
true, 'ReflectRemainder', true);
% The message below is an ASCII representation
% of the digits 1-9
msg = reshape(de2bi(49:57, 8, 'left-msb')', 72, 1);
encoded = generate(gen, msg);
encoded(1) = ~encoded(1);                % Introduce an error
[outdata error] = detect(det, encoded);  % Detect the error
noErrors = isequal(msg, outdata)         % Should be 0
error                                    % Should be 1