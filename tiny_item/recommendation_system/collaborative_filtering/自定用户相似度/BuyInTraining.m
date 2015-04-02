function res = BuyInTraining(training_data, user_id)
[m,n] = size(training_data) ;
pre_user_buy = zeros(884,100) ;
pre_user_buy(:,1) = user_id ;
for i=1:m
    i
    if 1 == training_data(i,3)
        x = find(user_id == training_data(i,1)) ;
        if ~any(pre_user_buy(x,2:end) == training_data(i,2))
            pre_user_buy(x,2) = pre_user_buy(x,2) + 1 ;
            pre_user_buy(x,pre_user_buy(x,2)+2) = training_data(i,2) ;
        end
    end
end
res = pre_user_buy ;
end