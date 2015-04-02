function res = CalSupport(buy_item, vector)
res = 0 ;
[m,n] = size(buy_item) ;
for i=1:m
    num = 0 ;
    for j=1:2
        if any(buy_item(i,3:2+buy_item(i,2)) == vector(j))
            num = num + 1 ;
        end
    end
    if num >= 2
        res = res+1 ;
    end
end
end