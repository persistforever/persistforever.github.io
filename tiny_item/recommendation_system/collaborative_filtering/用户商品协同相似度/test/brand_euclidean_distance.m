function [e_dis] = brand_euclidean_distance(rate)
    [m,n] = size(rate);
    e_dis = zeros(m,m);
    for i=1:n
        i
        for j=1:n
            e_dis(i,j) = norm(rate(:,i)-rate(:,j));
        end
    end
end