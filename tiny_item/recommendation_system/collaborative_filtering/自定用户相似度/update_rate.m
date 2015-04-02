function [real_rate] = update_rate(rate,e_dist,scale_factor,sim_count)
[m,n] = size(rate);
real_rate = zeros(m,n);
for i=1:m
    i
    sim_matrix = [(1:m)',e_dist(i,:)'];
    sim_sort = sortrows(sim_matrix,2);
%     sim_sort =  flipud(sim_sort) ;
    sim_top = sim_sort(1:sim_count,:);
    for j=1:n
%         sum=0;
%         count = 0;
%         for k= 1:sim_count
%             if (rate(sim_top(k,1),j)~=0)
%                 sum = sum+ rate(sim_top(k,1),j);
%                 count = count+1;
%             end
%         end
%         if count == 0
%             refer_rate = 0;
%         else
%             refer_rate = sum/count;
%         end
        refer_rate = 0 ;
        for k=1:sim_count
            refer_rate = refer_rate + rate(sim_top(k,1),j);
        end
        refer_rate = refer_rate/sim_count ;
        real_rate(i,j) = scale_factor*rate(i,j)+(1-scale_factor)*refer_rate;
    end
end
end

