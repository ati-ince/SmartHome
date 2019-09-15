k=1; %deðer sayýsý
m=1;
i=1;%{1,2}den baþlamakta


while (i<=length(SKY_data))
     data= SKY_data{i,2};
       if ((i+k)<length(SKY_data))
         while ( strcmp(data , SKY_data{(i+k),2}) )    
         k=k+1;
         end
       end
     i=i+k;
       if (k==1)
       else
          % if ( strcmp ( CookerHood_data{(i-k),2},'0,00') )
           SKY_data{m,4}=data;%ilk numara, saniye ve tarih aslýnda
           SKY_data{m,5}=k;
           m=m+1;
           k=1;
         %  end
       end     
end