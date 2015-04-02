function [boolBuy new_brand_id new_rate] = CalBoolBuy(training_data, brand_id, user_id, rate)
[m,n] = size(training_data) ;
boolBuy = zeros(886,9531) ;
new_brand_id = zeros(9531,1) ;
boolBuy(1,:) = 1:9531 ;
for i=1:m
    i
    if training_data(i,3) == 1
        x = find(user_id == training_data(i,1)) ;
        y = find(brand_id == training_data(i,2)) ;
        boolBuy(x+1,y) = 1 ;
    end
end
num = 0 ;
for i=1:9531
    boolBuy(886,i) = sum(boolBuy(2:885,i)) ;
end
boolBuy(:,find(boolBuy(886,:) == 0)) = [] ;
new_brand_id = brand_id(boolBuy(1,:)) ;
new_rate = rate(:,boolBuy(1,:)) ;
boolBuy = boolBuy(2:end-1,:) ;
end