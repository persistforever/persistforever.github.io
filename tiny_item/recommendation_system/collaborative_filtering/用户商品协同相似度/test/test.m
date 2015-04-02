load('rate.mat')
load('user_id')
load('brand_id')
load('buy_number')
load('user_buy')
%     u_e_dist = user_euclidean_distance(rate);
%     b_e_dist = brand_euclidean_distance(rate);
load('u_e_dist.mat');
load('b_e_dist.mat')
user_refer_rate = user_update_rate(rate,u_e_dist,scale_factor,sim_count);
brand_refer_rate = brand_update_rate(rate(:,1:1000),b_e_dist,scale_factor,sim_count);
% [f1 guess_result hb pb bb right] = week4(real_rate, user_id, brand_id, buy_number, user_buy) ; % Ô¤²âµÄ½á¹û