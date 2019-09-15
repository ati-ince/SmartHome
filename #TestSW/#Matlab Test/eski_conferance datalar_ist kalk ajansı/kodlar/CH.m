k=1; %deðer sayýsý
m=1;
i=1;%{1,2}den baþlamakta


while (i<=length(CookerHood_data))
     data= CookerHood_data{i,2};
    
       while ( strcmp(data , CookerHood_data{(i+k),2}) )    
       k=k+1;
       end
     
     i=i+k;
       if (k==1)
       else
          % if ( strcmp ( CookerHood_data{(i-k),2},'0,00') )
           CookerHood_data{m,4}=data;%ilk numara, saniye ve tarih aslýnda
           CookerHood_data{m,5}=k;
           m=m+1;
           k=1;
         %  end
       end     
end