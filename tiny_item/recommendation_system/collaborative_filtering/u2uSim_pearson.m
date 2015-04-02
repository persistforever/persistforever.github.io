function res = u2uSim_pearson(rate, avguserscore)
[m,n] = size(rate) ; 
u2usim = zeros(m,m) ;
for i=1:m
    i
    u2usim(i,i) = 1 ;
    for j=i+1:m
        vecA = zeros(1,1) ;
        vecB = zeros(1,1) ;
        vecC = rate(1,1) ;
        vecD = rate(1,1) ;
        for k=1:n
            if rate(i,k) ~= 0
                vecC = [vecC rate(i,k)] ;
            end
            if rate(j,k) ~= 0
                vecD = [vecD rate(j,k)] ;
            end
            if rate(i,k)*rate(j,k) ~= 0
                vecA = [vecA rate(i,k)] ;
                vecB = [vecB rate(j,k)] ;
            end
        end
        vecA = vecA(:,2:end) - avguserscore(i) ;
        vecB = vecB(:,2:end) - avguserscore(j) ;
        vecC = vecC(:,2:end) - avguserscore(i) ;
        vecD = vecD(:,2:end) - avguserscore(j) ;
        [r,c] = size(vecA) ;
        if 0 ~= c
            u2usim(i,j) = (vecA*vecB') / sqrt((vecC*vecC')*(vecD*vecD')) ;
        end
    end
end
for i=2:m
    for j=1:i-1
        u2usim(i,j) = u2usim(j,i) ;
    end
end
res = u2usim ;
end