%  % Bu sistem için 2.45 GHz de çalýþacak rectifier devresi için 7 deðerde
%  % ölçümler alýnmýþtýr.
%  % r = [-50 0.4 0.6  %% 0.24E-9 Watt
%    % -40 0.45 0.7    %% 0.315E-9 Watt
%    % -30 0.6 1.3     %% 0.78E-9 Watt
%    % -20 13.2 6.5    %% 85.8E-9 Watt
%    % -10 120 48      %% 5760E-9 Watt
%    % 0 610 240       %% 146400E-9 Watt
%    % 10 2000 860]    %% 480000E-9 Watt
% % 1. deðer dBm, 2. deðer mV cinsi çýkýþ gerilimi ve 3. deger uA cinsi çýkýþ
% % akýmý, 4. ise Watt cinsi güç deðerleri
% %%% 
% % Bu ölçümler ve "Polynomial Regression" formul uydurma yöntemi kullanýlarak
% % diðer dBm deðerlerindeki çýkýþ güç deðerlerini tahmin etmek üzere 6.
% % mertebeden formül uydurulmuþtur. 
% % (x: (Watt cinsi giriþ, lineer olmasý için Watt'a çevirdim), dBm -> Watt
% %  y: Watt cinsi güç çýkýþ deðeri)
% %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% 
% %% y= (-5.658603604*(10^12)*(x^6)) + (2.849283309*(10^10)*(x^5)) 
% %%    +(362687657.7*(x^4)) - (880362.4496*(x^3)) + (636.4007277*(x^2))
% %%    -(3.346832624*(10^-2)*x)) + (3.342824772*(10^-5));
% % Bu formüle göre elimizdeki deðerler % 100-(10^-5) doðrulukta çýkmaktadýr.
% %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% % x: giriþ, (1e-8)W == -50 dBm, (0.01)W == 10 dBm
% %x_buff= -50:10:10; % giriþ deðerleri Watt cinsinden
% %x = db2pow(x_buff-30);
% x= -50:10:10;
% %
% length_x= length(x);
% for i=1:length_x
% y(i) = -9.284813625*(10^-8)* (x(i)^6) - 9.91918455*(10^-6)*(x(i)^5) - 3.2077701*(10^-4)*(x(i)^4) - 3.163765672*(10^-3)*(x(i)^3) - 1.146473927*(10^-2)*(x(i)^2) + 1.375977789*(x(i)) + 21.65541076;
% end;
% %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% %%%%%%%%%%%%%%%%%%%%%%%
% %%%%%%%%%%%%%%%%
% x_dBm= [-50:10];
% length_x_dBm=length(x_dBm);
% for i=1:length_x_dBm
%  y_dBm(i)=-4.357027084*(10^-13)*(x_dBm(i)^6) - 5.548444501*(10^-11)*(x_dBm(i)^5) - 2.200243584*(10^-9)*(x_dBm(i)^4) - 1.095397625*(10^-8)* (x_dBm(i)^3) + 1.189181385*(10^-6)* (x_dBm(i)^2) + 2.536224208*(10^-5)* x_dBm(i) + 0.0001464   ; 
% end  
%     
 
%% Constands
% Inputs, Outputs
Input_dBm=[-11,-10,-9,-8,-7,-6,-5,-4,-3,-2,-1,0,1,2,3,4,5,6,7,8,9,10]; 
Output_available_uPower=[3.78206478122059,5.75999999438698,7.96038996034141,11.0013555966224,15.2040070356233,21.0121223615618,29.0390082760932,40.1322620889368,55.4632735753951,76.6509176303600,105.932499018282,146.400000093588,187.300878345750,239.628545127481,306.575389005463,392.225680349766,501.804743116857,641.997739643223,821.357516764296,1050.82639499653,1344.40373392028,1720.00000036615];
Pharv=Output_available_uPower;
% Xbee constands (T:ms, and P: micro Power(uPower) )
Tb=12.98;Tbet=13.1;Tnbt=22; % ms
Ps=15;%Ps=3.3;
Pb=56.3069E+3;Pbet=63.303E+3;Pnbt=91.7252E+3; % micro power (uP)
%%%%%
%%%%%
%% NoN-Beacon-Enable
for i=1:length(Pharv)
       Tnbs(i)= Tnbt*(Pnbt-Pharv(i))./( Pharv(i) - Ps ); %non-beacon sleep times, ms
       Tnbp(i) = Tnbt + Tnbs(i) ;% Non-beacon period (ms)
       Comm_nb_minute(i)=1000/Tnbp(i);
       Comm_nb_hour(i)=(1000*60)/Tnbp(i);
       Comm_nb_day(i)=(1000*60*24)/Tnbp(i);
       Comm_nb_week(i)=(1000*60*24*7)/Tnbp(i);
       Comm_nb_month(i)=(1000*60*24*30)/Tnbp(i);
       Comm_nb_year(i)=(1000*60*24*365)/Tnbp(i);
end;

%% Beacon - EnableNetwork

for i=1:length( Pharv ) % harvesting values
     for n=0:999        % beacon quantity in a period
         % T beacon enable total sleep (ms) = n*Ts1 + Ts2
         Tbets((n+1),i)= ( n*Tb*(Pb-Pharv(i))+Tbet*(Pbet-Pharv(i) ) )./( Pharv(i)-Ps );
         Tbep ((n+1),i) = n*Tb+Tbets((n+1),i)+Tbet;
         Comm_be_minute((n+1),i)=1000/Tbep ((n+1),i);
         Comm_be_hour((n+1),i)=(1000*60)/Tbep ((n+1),i);
         Comm_be_day((n+1),i)=(1000*60*24)/Tbep ((n+1),i);
         Comm_be_week((n+1),i)=(1000*60*24*7)/Tbep ((n+1),i);
         Comm_be_month((n+1),i)=(1000*60*24*30)/Tbep ((n+1),i);
         Comm_be_year((n+1),i)=(1000*60*24*365)/Tbep ((n+1),i);
     end
end

%% Burada dakikada saatte felan ne kadar data haberleþmesi yapabileceðini yazalým, sadece sleeping time dan bir þey anlaþýlmamakta







    
    
