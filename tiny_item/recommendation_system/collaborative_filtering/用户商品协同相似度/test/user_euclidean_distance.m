function [e_dis] = user_euclidean_distance(rate)
    [m,n] = size(rate);
    e_dis = zeros(m,m);
    for i=1:m
        i
        for j=1:m
            e_dis(i,j) = norm(rate(i,:)-rate(j,:));
        end
    end
end

