 k=1; %de�er say�s�
m=1;
i=4;%{1,2}den ba�lamakta
uzunluk=1;
for x=1:length(x4Noks_data_18_07)
   if ( isempty(    x4Noks_data_18_07{x,23}))
    else
    uzunluk=uzunluk+1;
   end
end

while (i<=length(x4Noks_data_18_07))
     data= x4Noks_data_18_07{i,23};
    
       while ( strcmp(data , x4Noks_data_18_07{(i+k),23}) )    
       k=k+1;
       end
     
     i=i+k;
       if (k==1)
       else
          % if ( strcmp ( CookerHood_data{(i-k),2},'0,00') )
          DW_data_18{m,1}=data;%ilk numara, saniye ve tarih asl�nda
           DW_data_18{m,2}=k;
           m=m+1;
           k=1;
         %  end
       end     
end