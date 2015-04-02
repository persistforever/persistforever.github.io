function avguserscore = avgUserScore(rate) 
[m,n] = size(rate) ;
avguserscore = zeros(m,1) ;
for i=1:m
    num = 0 ;
    for j=1:n
        if rate(i,j) ~= 0 
            num = num + 1 ;
        end
    end
    avguserscore(i,1) = sum(rate(i,:))/(num+1) ;
end
end