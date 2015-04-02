% load('rate.mat')
% load('e_dist')
% load('user_id')
% load('brand_id')
% load('buy_number')
% load('user_buy')
% % load('real_rate')
% scale_factor = 0.5;
% sim_count = 20;
% rmse = zeros(11,1);
% for i =1:11
%     sim_count = i*10
%     real_rate = update_rate(rate,e_dist,scale_factor,sim_count);
%     [f1 guess_result hb pb bb right] = week4(real_rate, user_id, brand_id, buy_number, user_buy) ; % 预测的结果
%     f1
%     rmse(i)=f1;
% end

% load('rate.mat')
% load('e_dist')
% load('user_id')
% load('brand_id')
% load('buy_number')
% load('user_buy')
% load('u2usim')
% % load('real_rate')
% scale_factor = 0.5;
% sim_count = 20;
% % rmse = zeros(11,1);
% %  for i =1:11
% %      sim_count = i*10
%     real_rate = update_rate(rate,u2usim,scale_factor,sim_count);
%     [f1 guess_result hb pb bb right] = week4(real_rate, user_id, brand_id, buy_number, user_buy) ; % 预测的结果
% %     f1
% %     rmse(i)=f1;
% %  end



% e_dist = euclidean_distance(rate);

% rate = zeros(884,9531) ;
% % a = importdata('train_and_remine.mat');
% % training_data = a.training_data;
% % b = importdata('brand_user_id.mat');
% % brand_id = b.brand_id;
% % user_id = b.user_id;
% rate = rateMatrix(data, brand_id, user_id) ; % 获得评分矩阵
% % save rate.mat rate;
% % [rate_bili res2] = DiscretRate(rate) ;
% % % avguserscore2 = avgUserScore(rate) ;% 获得用户连续值平均评分
% % avguserscore = avgUserScore(rate) ; % 获得用户离散值平均评分
% % save avguserscore.mat avguserscore
% u2usim = u2uSim_pearson(rate, avguserscore) ; % 用户与用户之间的相似度矩阵(pearson方法)   
% % % u2usim = u2uSim_easy(rate) ; % 用户与用户之间的相似度矩阵(简单方法)
% buy_number = buyNumber(data,user_id,4) ; % 预测用户购买的商品个数
% scale_factor = 0.5;
% sim_count = 50;
% e_dis = euclidean_distance(rate) ;
% real_rate = update_rate(rate,e_dis,scale_factor,sim_count);
% final_rate = CFR(rate, u2usim, avguserscore) ; % 用户最终的评分
% [f1 guess_result hb pb bb right] = week4(final_rate, user_id, brand_id, buy_number, user_buy) ; % 预测的结果
print_result(real_rate, user_id, brand_id, buy_number) ; % 打印结果
% 
