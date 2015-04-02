function buy_item = RemoveItem(data, user_id)
[m,n] = size(data) ;
buy_item = zeros(884,100) ;
buy_item(:,1) = user_id(:,1) ;
for i=1:m
    if data(i,3) == 1
        x = find(user_id == data(i,1)) ;
        if ~any(buy_item(x,3:end) == data(i,2)) 
            buy_item(x,2) = buy_item(x,2) + 1 ;
            buy_item(x,buy_item(x,2)+2) = data(i,2) ;
        end
    end
end
end