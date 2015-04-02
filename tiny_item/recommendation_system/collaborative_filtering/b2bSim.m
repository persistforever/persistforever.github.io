function res = b2bSim(rate)
[m,n] = size(rate) ;
b2bsim = zeros(n,n) ; % 商品之间的相似度
for i=1:m
    for j=1:n
        if rate(i,j)~=0
            rate(i,j) = 1 ;
        end
    end
end
for i=1:n
    i
    for j=i:n
        b2bsim(i,j) = sum(rate(:,i) .* rate(:,j)) ;
    end
end
for i=2:n
    for j=1:i-1
        b2bsim(i,j) = b2bsim(j,i) ;
    end
end
res = b2bsim ;
end