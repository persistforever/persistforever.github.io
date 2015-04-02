function res = buyNumber(train_data,user_id,month)
%计算平均每月要买的商品数 data是原始矩阵，month表示月数
[m n] = size(train_data);
if 4 == month
    scoreVector = [1 15 4 4] ; % 每项的权重
else
    scoreVector = [0 16 5 5] ;
end
res = zeros(size(user_id));
for i = 1:m
    temp = find(user_id == train_data(i,1));
    res(temp) = res(temp) + scoreVector(train_data(i,3)+1)/sum(scoreVector);
end
res = res./month;
res = ceil(res);
end

