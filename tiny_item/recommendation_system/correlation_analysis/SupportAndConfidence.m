function res = SupportAndConfidence(boolBuy)
[m,n] = size(boolBuy) ;
b2bsim = zeros(n,n) ;
for i=1:n
    i
    for j=i:n
        support = 0 ;
        temp = boolBuy(:,i) + boolBuy(:,j) ;
        for k=1:m
            if temp(k,1)==2
                support = support + 1 ;
            end
        end
        b2bsim(i,j) = support / sum(boolBuy(:,i)) ;
        b2bsim(j,i) = support / sum(boolBuy(:,j)) ;
    end
end
res = b2bsim ;
end