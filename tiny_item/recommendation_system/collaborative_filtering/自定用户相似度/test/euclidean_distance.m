function [e_dis] = euclidean_distance(rate)
    [m,n] = size(rate);
    e_dis = zeros(m,m);
    for i=1:20
        i
        for j=1:20
            e_dis(i,j) = sqrt((rate(i,:)-rate(j,:))*(rate(i,:)-rate(j,:))');
        end
    end
end

