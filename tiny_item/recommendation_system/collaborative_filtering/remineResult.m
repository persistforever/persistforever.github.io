function res = remineResult(finalrate2, finalrate, rate, user_buy, brand_id)
num = 0 ;
example = zeros(600,5) ;
for i=1:884
    for j=3:100
        if user_buy(i,j) ~= 0
            num = num + 1 ;
            id = find(brand_id == user_buy(i,j)) ;
            example(num,1) = user_buy(i,1) ;
            example(num,2) = user_buy(i,j) ;
            example(num,3) = finalrate2(i,id) ;
            example(num,4) = finalrate(i,id) ;
            example(num,5) = rate(i,id) ;
        end
    end
end
res = example ;
end