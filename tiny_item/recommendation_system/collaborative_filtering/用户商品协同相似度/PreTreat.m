function res = PreTreat(training_data, begin_day, end_day)
[m,n] = size(training_data) ;
res = zeros(1,4) ;
for i=1:m
    i
    if training_data(i,4)>=begin_day && training_data(i,4)<=end_day 
        res = [res ; training_data(i,:)] ;
    end
end
res = res(2:end,:) ;
end