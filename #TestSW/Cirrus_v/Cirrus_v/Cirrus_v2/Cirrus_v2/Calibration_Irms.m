function [ output_args ] = Calibration_Irms( input_args )
%UNTÝTLED2 Summary of this function goes here
%   Detailed explanation goes here

output_args=2.84088*(10^(-13))*(input_args^2) +4.8954810*(10^(-6))*input_args+1.2601130473*(10^(-2));
end

