function [e_dis] = euclidean_distance(rate)
    [m,n] = size(rate);
    e_dis = zeros(m,m);
    for i=1:m
        i
        for j=1:m
            e_dis(i,j) = rate(i,:)*rate(j,:)'/(sqrt((rate(i,:)*rate(i,:)'))*sqrt((rate(j,:)*rate(j,:)')));
        end
    end
end

