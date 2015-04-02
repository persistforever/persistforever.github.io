function [num, bb, res] = RecoverRate(user_buy, item_id)
[m,n] = size(user_buy) ;
bb = 0 ;
num = 0 ;
for i=1:m
    for j=3:n
        if 0 ~= user_buy(i,j)
            bb = bb + 1 ;
            if any(item_id == user_buy(i,j))
                num = num + 1 ;
            end
        end
    end
end
res = num/bb ;
end