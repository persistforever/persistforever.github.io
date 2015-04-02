function guess_result = GuessResult(buy_item, buy_number, b2bsim, new_brand_id, user_id)
guess_result = user_id ;
guess_result(:,2:51) = zeros(884,50) ;
[m,n] = size(buy_item) ;
for i=1:m
    i
    recmVec = zeros(1,2) ;
    for j=3:buy_item(i,2)+2 
        if any(new_brand_id==buy_item(i,j))
            pos = find(new_brand_id == buy_item(i,j));
            recmVec = [recmVec ; [new_brand_id b2bsim(pos,:)'] ] ;
        end
    end
    recmVec = recmVec(2:end,:) ;
    recmVec = unique(recmVec,'rows') ;
    [row col] = size(recmVec) ;
    if 0 == row
        continue ;
    end
    recmVec = sortrows(recmVec,2) ;
    if row > buy_number(i)
        recmVec = recmVec(end-buy_number(i)+1:end,:) ;
    end
    [row col] = size(recmVec) ;
    guess_result(i,2) = row ;
    guess_result(i,3:row+2) = recmVec(:,1)' ;
end
end