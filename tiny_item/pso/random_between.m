function value = random_between(a, b, p)
    % generalize vlaue between a and b randomly
    % input : a - begin value
    %         b - end value
    %         p - precision
    % output : value - random value between a and b
    % ---------------------------------------------------------------------
    step = (b - a)/p ; % step length
    numStep = round(rand(1,1)*p) ; 
    value = a + numStep * step ;
end