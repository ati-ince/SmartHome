 k=1; %deðer sayýsý
m=1;
i=4;%{1,2}den baþlamakta
uzunluk=1;
for x=1:length(x4Noks_data_18_07)
   if ( isempty(    x4Noks_data_18_07{x,38}))
    else
    uzunluk=uzunluk+1;
   end
end

while (i<=length(x4Noks_data_18_07))
     data= x4Noks_data_18_07{i,38};
    
       while ( strcmp(data , x4Noks_data_18_07{(i+k),38}) )    
       k=k+1;
       end
     
     i=i+k;
       if (k==1)
       else
          % if ( strcmp ( CookerHood_data{(i-k),2},'0,00') )
          WM_data_18{m,1}=data;%ilk numara, saniye ve tarih aslýnda
           WM_data_18{m,2}=k;
           m=m+1;
           k=1;
         %  end
       end     
end