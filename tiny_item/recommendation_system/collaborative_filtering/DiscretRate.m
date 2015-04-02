function [res1 res2] = DiscretRate(data)
% 将评分矩阵离散化，分为五个等级分别为1 2 3 4 5
%res1表示按人数平均分成5个等级
%res2表示按0.2 0.4 0.6 0.8 来分成5段
[m n] = size(data);
sort_vector = zeros(1,m*n);
jj = 1;
for ii = 1:m
%     data(ii,:)
    sort_vector(jj:jj+n-1) = data(ii,:);
    jj = jj + n;
end
sort_vector = sort(sort_vector);
res = sort_vector;
index = find(res ~= 0);
res = res(index:m*n);
[mm nn] = size(res);
step = round(nn / 5);
judge_vector = zeros(1,4);
for ii = 1:4
    judge_vector(ii) = res(ii*step);
end
res = data;
for ii = 1:m
    for jj = 1:n
        if data(ii,jj) == 0
            continue;
        elseif data(ii,jj) <= judge_vector(1) 
            res(ii,jj) = 1;
        elseif data(ii,jj) <= judge_vector(2)
            res(ii,jj) = 2;
        elseif data(ii,jj) <= judge_vector(3)
            res(ii,jj) = 3;
        elseif data(ii,jj) <= judge_vector(4)
            res(ii,jj) = 4;
        else
            res(ii,jj) = 5;
        end
    end
end
res1 = res;   
judge_vector = [0.2 0.4 0.6 0.8];
res = data;
for ii = 1:m
    for jj = 1:n
        if data(ii,jj) == 0
            continue;
        elseif data(ii,jj) <= judge_vector(1) 
            res(ii,jj) = 1;
        elseif data(ii,jj) <= judge_vector(2)
            res(ii,jj) = 2;
        elseif data(ii,jj) <= judge_vector(3)
            res(ii,jj) = 3;
        elseif data(ii,jj) <= judge_vector(4)
            res(ii,jj) = 4;
        else
            res(ii,jj) = 5;
        end
    end
end
res2 = res;
end