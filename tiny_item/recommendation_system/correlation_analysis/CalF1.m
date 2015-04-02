function [f1 bb pb hb] = CalF1(guess_result, user_buy)
[m,n] = size(guess_result) ;
hb = 0 ;
pb = 0 ;
bb = 0 ;
for i=1:m
    for j=2:n
        if guess_result(i,j)~= 0
            pb = pb + 1 ;
        end
    end
end
for i=1:m
    for j=3:n
        if user_buy(i,j) ~= 0
            bb = bb + 1 ;
        end
    end
end
for i=1:m
    for j=3:n
        if user_buy(i,j)==0 
            break ;
        else
            if any(guess_result(i,2:n) == user_buy(i,j))
                hb = hb+1 ;
            end
        end
    end
end
p = hb/pb ;
r = hb/bb ; 
f1 = 100*2*p*r/(r+p) ;
end