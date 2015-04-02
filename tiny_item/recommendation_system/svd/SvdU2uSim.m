function res = SvdU2uSim(user, item, rate, u2usim)
row = find(rate(:,item)~= 0) ;
rate = rate(row,:) ;
[m,n] = size(rate) ;
sim = zeros(1,m) ;
for i=1:m
    sim(1,i) = u2usim(row(i),user) ;
end
res = (1./sim)*rate(:,item)/sum(1./sim) ;
end