function res = u2bScore(rate, b2bsim, user, brand)
[m,n] = size(rate) ;
k = 25 ; % 与商品p最相似的k个项目
Mp = zeros(n,3) ;
for i=1:n
    Mp(i,1) = i ;
    Mp(i,2) = b2bsim(i,brand) ;
end
Mp = sortrows(Mp,2) ;
score = 0 ; % 用户对商品的打分
sim = 0 ; % 相似度之和
for i=n-k-1:n-1
    Mp(i,3) = rate(user,Mp(i,1)) ;
end
score = Mp(n-k-1:n-1,3)'*Mp(n-k-1:n-1,2) ;
sim = sum(abs(Mp(n-k-1:n-1,2))) ;
res = score/sim ;
end