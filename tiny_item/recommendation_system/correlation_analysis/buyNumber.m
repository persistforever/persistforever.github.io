function buy_number = buyNumber(training_data,user_id,month)
%计算平均每月要买的商品数 data是原始矩阵，month表示月数
[m n] = size(training_data);
scoreVector=[0 1 0 0] ; % 每项的权重
buy_number = zeros(size(user_id));
for i = 1:m
    temp = find(user_id == training_data(i,1));
    buy_number(temp) = buy_number(temp) + scoreVector(training_data(i,3)+1);
end
buy_number = buy_number./month;
buy_number = ceil(buy_number);
end

