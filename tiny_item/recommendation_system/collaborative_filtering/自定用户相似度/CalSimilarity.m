function res = CalSimilarity(pre_rate, post_rate)
[m,n] = size(pre_rate) ;
pre_user_buy = zeros(9531,100) ; % 商品被哪些用户买了
post_user_buy = zeros(9531,100) ; % 商品被哪些用户买了
for i=1:m
    for j=1:n
        if 1 == pre_rate(i,j)
            pre_user_buy(i,1) = pre_user_buy(i,1) + 1 ;
            pre_user_buy(i,pre_user_buy(i,1)+1) = j ;
        end
        if 1 == post_rate(i,j)
            post_user_buy(i,1) = post_user_buy(i,1) + 1 ;
            post_user_buy(i,post_user_buy(i,1)+1) = j ;
        end
    end
end   
u2usim = zeros(n,n) ; % 用户相似度
for i=1:n
    for j=2:pre_user_buy(i,1)+1
        for k=2:post_user_buy(i,1)+1
            u2usim(pre_user_buy(i,j),post_user_buy(i,k)) = u2usim(pre_user_buy(i,j),post_user_buy(i,k)) + 1 ;
        end
    end
end
res = u2usim ;
end