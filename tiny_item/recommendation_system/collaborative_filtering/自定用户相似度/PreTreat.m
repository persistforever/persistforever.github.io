function [month1 month2 month3] = PreTreat(training_data)
[m,n] = size(training_data) ;
month1 = zeros(1,4) ;
month2 = zeros(1,4) ;
month3 = zeros(1,4) ;
month4 = zeros(1,4) ;
for i=1:m
    i
    if training_data(i,4)>=1 && training_data(i,4)<=30 
        month1 = [month1 ; training_data(i,:)] ;
    end
    if training_data(i,4)>=31 && training_data(i,4)<=60 
        month2 = [month2 ; training_data(i,:)] ;
    end
    if training_data(i,4)>=61 && training_data(i,4)<=90
        month3 = [month3 ; training_data(i,:)] ;
    end
end
month1 = month1(2:end,:) ;
month2 = month2(2:end,:) ;
month3 = month3(2:end,:) ;
end