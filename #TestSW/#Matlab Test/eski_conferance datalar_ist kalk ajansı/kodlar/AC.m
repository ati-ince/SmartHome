k=1; %de�er say�s�
m=1;
i=1;%{1,2}den ba�lamakta


while (i<=length(AC_data))
     data= AC_data{i,2};
     if (length(AC_data{(i+k),2})>4)
       while (data == AC_data{(i+k),2})     
       k=k+1;
       end
     end
     i=i+k;
       if (k==1)
       else
       AC_data{m,4}=data;%ilk numara, saniye ve tarih asl�nda
       AC_data{m,5}=k;
       m=m+1;
       k=1;
       end     
end
