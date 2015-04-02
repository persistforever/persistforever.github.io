function print_result(guess_result)
% [m,n] = size(final_rate) ;
% k = 100 ; % 预测商品个数
% guess_result = user_id ;
% guess_result(:,2:k) = zeros(m,k-1) ;
% for i=1:m
%     vecItem = zeros(n,2) ;
%     vecItem(:,1) = brand_id(1:n) ;
%     vecItem(:,2) = final_rate(i,:)' ;
%     vecItem = sortrows(vecItem,2) ;
%     guess_result(i,2) = buy_number(i,1) ;
%     if buy_number(i,1) ~= 0
%         guess_result(i,3:buy_number(i,1)+2) = vecItem(end-buy_number(i,1)+1:end,1)' ;
%     end
% end
% 打印结果
fid = fopen('E:\b.txt','wt'); 
% fprintf(fid,'%g\n',a);       
% fclose(fid);
for i=1:884
    str = num2str(guess_result(i,1)) ;
    fprintf(fid,'%s\t',str) ;
    for j=3:51
        if guess_result(i,j) ~= 0
            if j<=51
                if guess_result(i,j+1) ~= 0
                    fprintf(fid,'%g,',guess_result(i,j)) ;
                else
                    fprintf(fid,'%g',guess_result(i,j)) ;
                end
            else
                fprintf(fid,'%g',guess_result(i,j)) ;
            end
        else
            fprintf(fid,'\n') ;
            break ;
        end
    end
    if(j==51) 
        fprintf(fid,'\n') ;
    end
end
fclose(fid) ;      
end