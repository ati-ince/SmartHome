%%
% I'm using All_Data_Cell
% {'AC';'CH';'Refrigrator';'SKY';'TMP';'Kattle';'PP';'TV';'HS';'WM';'MW';'ST';'OVEN';'DW';'KITCHEN1';}

%% what we use
% 1:16 -> 15 
% 17:59 -> 14
% 60-76 -> 13
% 77:92 -> 12
% 93:124 -> 11
% 125:148 -> 10
% 149:165 -> 9
% 166:183 -> 8
% 184:205 -> 7
% 206:220 -> 6
% 221:236 -> 5 ve son bu
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%% 15->5 noda giderken 15'er dakika inceleme
%5
local_top(1:5)=0;
general_top=0;
for i=1:15
    for j=6:10
    local_top(i)= All_Data_Cell{i,1}{j,2} + local_top(i);
    end
    general_top=general_top+local_top(i);
end
result_5(1)= (general_top/length(1:15));

%14
local_top(1:14)=0;
general_top=0;
for i=1:14
    for j=6:10
    local_top(i)= All_Data_Cell{i,1}{j,2} + local_top(i);
    end
    general_top=general_top+local_top(i);
end
result_5(2)= (general_top/length(1:14));

%13
local_top(1:13)=0;
general_top=0;
for i=1:13
    for j=6:10
    local_top(i)= All_Data_Cell{i,1}{j,2} + local_top(i);
    end
    general_top=general_top+local_top(i);
end
result_5(3)= (general_top/length(1:13));

%12
local_top(1:12)=0;
general_top=0;
for i=1:12
    for j=6:10
    local_top(i)= All_Data_Cell{i,1}{j,2} + local_top(i);
    end
    general_top=general_top+local_top(i);
end
result_5(4)= (general_top/length(1:12));

%11
local_top(1:11)=0;
general_top=0;
for i=1:11
    for j=6:10
    local_top(i)= All_Data_Cell{i,1}{j,2} + local_top(i);
    end
    general_top=general_top+local_top(i);
end
result_5(5)= (general_top/length(1:11));

%10
local_top(1:10)=0;
general_top=0;
for i=1:10
    for j=6:10
    local_top(i)= All_Data_Cell{i,1}{j,2} + local_top(i);
    end
    general_top=general_top+local_top(i);
end
result_5(6)= (general_top/length(1:10));

%9
local_top(1:9)=0;
general_top=0;
for i=1:9
    for j=6:10
    local_top(i)= All_Data_Cell{i,1}{j,2} + local_top(i);
    end
    general_top=general_top+local_top(i);
end
result_5(7)= (general_top/length(1:9));

%8
local_top(1:8)=0;
general_top=0;
for i=1:8
    for j=6:10
    local_top(i)= All_Data_Cell{i,1}{j,2} + local_top(i);
    end
    general_top=general_top+local_top(i);
end
result_5(8)= (general_top/length(1:8));

%7
local_top(1:7)=0;
general_top=0;
for i=1:7
    for j=6:10
    local_top(i)= All_Data_Cell{i,1}{j,2} + local_top(i);
    end
    general_top=general_top+local_top(i);
end
result_5(9)= (general_top/length(1:7));

%6
local_top(1:6)=0;
general_top=0;
for i=1:6
    for j=6:10
    local_top(i)= All_Data_Cell{i,1}{j,2} + local_top(i);
    end
    general_top=general_top+local_top(i);
end
result_5(10)= (general_top/length(1:6));

%5
local_top(1:5)=0;
general_top=0;
for i=1:5
    for j=6:10
    local_top(i)= All_Data_Cell{i,1}{j,2} + local_top(i);
    end
    general_top=general_top+local_top(i);
end
result_5(11)= (general_top/length(1:5));
