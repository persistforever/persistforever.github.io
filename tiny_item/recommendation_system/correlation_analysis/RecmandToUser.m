function guess_result = RecmandToUser(sim, buy_number, buy_item, user_id)
[m,n] = size(buy_number) ;
guess_result = zeros(884,50) ;
guess_result(:,1) = user_id ;
for i=1:m
    vecItem = buy_item(i,3:buy_item(i,2)+2) ;
    recmandBase = zeros(1,2) ;
    [row col] = size(vecItem) ;
    for j=1:col
        [a b] = size(sim(find(sim(:,1)==vecItem(1,j)),2:3)) ;
        if 0 == a
            continue ;
        end
        recmandBase = [recmandBase ; sim(find(sim(:,1)==vecItem(1,j)),2:3)] ;
    end
    recmandBase = recmandBase(2:end,:) ;
    [row col] = size(recmandBase) ;
    if 0 == row
        continue ;
    end
    recmandBase = sortrows(recmandBase,2) ;
    if row > buy_number(i)
        recmandBase = recmandBase(end-buy_number(i)+1:end,:) ;
    end
    [row col] = size(recmandBase) ;
    guess_result(i,2) = row ;
    guess_result(i,3:row+2) = recmandBase(:,1)' ;
end
end