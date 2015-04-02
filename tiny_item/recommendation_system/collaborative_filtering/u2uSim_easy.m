function res = u2uSim_easy(rate)
rate = rate' ;
[m,n] = size(rate) ;
u2usim = zeros(n,n) ; % 用户之间的相似度
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
        u2usim(i,j) = sum(rate(:,i) .* rate(:,j)) ;
    end
end
for i=2:n
    for j=1:i-1
        u2usim(i,j) = u2usim(j,i) ;
    end
end
res = u2usim ;
end