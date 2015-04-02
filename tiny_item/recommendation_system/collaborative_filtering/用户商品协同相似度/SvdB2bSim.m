function res = SvdB2bSim(u, k)
u = u(:,1:k) ;
[m,n] = size(u) ;
u2usim = zeros(m,m) ;
for i=1:m
    i
    for j=1:m
        u2usim(i,j) = sqrt((u(i,:)-u(j,:))*(u(i,:)-u(j,:))') ;
    end
end
res = u2usim ;
end