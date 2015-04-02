function res = CalB2BSimilarity(pre_rate, post_rate)
[m,n] = size(pre_rate) ;
pre_user_buy = zeros(884,100) ; % 商品被哪些用户买了
post_user_buy = zeros(884,100) ; % 商品被哪些用户买了
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
b2bsim = zeros(n,n) ; % 商品相似度
for i=1:n
    for j=2:pre_user_buy(i,1)+1
        for k=2:post_user_buy(i,1)+1
            b2bsim(pre_user_buy(i,j),post_user_buy(i,k)) = b2bsim(pre_user_buy(i,j),post_user_buy(i,k)) + 1 ;
        end
    end
end
res = b2bsim ;
end