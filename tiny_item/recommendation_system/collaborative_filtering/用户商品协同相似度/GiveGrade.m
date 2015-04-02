function mat0_1 = GiveGrade(training_data,user_id,brand_id,begin_day,end_day,grade_vector)
%计算打分矩阵，返回0-1化后的商品用户矩阵
[m n] = size(user_id);
[mm n] = size(brand_id);
mat0_1 = zeros(m,mm); %用户商品884*9531矩阵
training_data = PreTreat(training_data,begin_day,end_day);
[m n] = size(training_data);
for i = 1:m
    i
    u_id = find(user_id == training_data(i,1));
    b_id = find(brand_id == training_data(i,2));
    mat0_1(u_id,b_id) = mat0_1(u_id,b_id) + grade_vector(training_data(i,3)+1);
end
mat0_1 = floor(mat0_1);
mat0_1 = logical(mat0_1);
end

