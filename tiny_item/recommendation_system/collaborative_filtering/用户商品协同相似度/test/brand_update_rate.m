function [real_rate] = brand_update_rate(rate,e_dist,scale_factor,sim_count)
[n,m] = size(rate);
real_rate = zeros(m,n);
refer_rate = zeros(size(rate));
for i=1:m
    i
    sim_matrix = [(1:m)',e_dist(i,:)'];
    sim_sort = sortrows(sim_matrix,2);
    sim_top = sim_sort(2:sim_count+1,:);
    for j=1:n
        refer_rate(i,j) = mean(rate(j,sim_top(:,1)));
%         real_rate(j,i) = scale_factor*rate(j,i)+(1-scale_factor)*refer_rate;
%         rate(i,j)
%         refer_rate
%         real_rate(i,j)
%         pause
    end
end
end

