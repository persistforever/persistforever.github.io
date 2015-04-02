function res = rateMatrix(trainData, brandId, userId, month, scoreVector)
[m,n] = size(trainData) ;
rate = zeros(884,9531) ;
for i=1:m
    i
    x = find(userId==trainData(i,1)) ;
    y = find(brandId==trainData(i,2)) ;
    rate(x,y) = rate(x,y) + scoreVector(trainData(i,3)+1)*((trainData(i,4)/(month*30))^2) ;
end
res = mapminmax(rate,0,1) ;
end