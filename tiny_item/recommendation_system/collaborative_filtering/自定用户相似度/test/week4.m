function [f1, guess_result, hb, pb, bb, right] = week4(final_rate, user_id, brand_id, buy_number, user_buy)
[m,n] = size(final_rate) ;
k = 100 ; % 预测商品个数
guess_result = user_id ;
guess_result(:,2:k) = zeros(m,k-1) ;
right = zeros(100,3) ;
for i=1:m
    vecItem = zeros(n,2) ;
    vecItem(:,1) = brand_id(1:n) ;
    vecItem(:,2) = final_rate(i,:)' ;
    vecItem = sortrows(vecItem,2) ;
    guess_result(i,2) = buy_number(i,1) ;
    if buy_number(i,1) ~= 0
        guess_result(i,3:buy_number(i,1)+2) = vecItem(end-buy_number(i,1)+1:end,1)' ;
    end
end

%计算F1(如果要看f1值变化的话直接改f1，别的不用改)
hb = 0 ;
pb = 0 ;
bb = 0 ;
num = m ;
for i=1:num
    for j=2:65
        if guess_result(i,j)~= 0
            pb = pb + 1 ;
        end
    end
end
for i=1:num
    for j=3:100
        if user_buy(i,j) ~= 0
            bb = bb + 1 ;
        end
    end
end
for i=1:num
    for j=3:100
        if user_buy(i,j)==0 
            break ;
        else
            if any(guess_result(i,2:65) == user_buy(i,j))
                hb = hb+1 ;
                right(hb,1) = i ;
                right(hb,2) = find(brand_id == user_buy(i,j)) ;
                right(hb,3) = final_rate(right(hb,1),right(hb,2));
            end
        end
    end
end
p = hb/pb ;
r = hb/bb ; 
f1 = 100*2*p*r/(r+p) ;
end