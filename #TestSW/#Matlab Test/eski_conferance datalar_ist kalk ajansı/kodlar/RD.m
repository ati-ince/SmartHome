k=1; %deðer sayýsý
m=1;
i=1;%{1,2}den baþlamakta


while (i<=length(Refrigredator_Data))
     data= Refrigredator_Data{i,2};
       if ((i+k)<=length(Refrigredator_Data))
         while ( strcmp(data , Refrigredator_Data{(i+k),2}) )    
         k=k+1;
         end
       end
     i=i+k;
       if (k==1)
       else
          % if ( strcmp ( CookerHood_data{(i-k),2},'0,00') )
           Refrigredator_Data{m,4}=data;%ilk numara, saniye ve tarih aslýnda
           Refrigredator_Data{m,5}=k;
           m=m+1;
           k=1;
         %  end
       end     
end