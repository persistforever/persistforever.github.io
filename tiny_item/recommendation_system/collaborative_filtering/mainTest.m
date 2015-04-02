rate = zeros(884,9531) ;
rate = rateMatrix(training_data, brand_id, user_id) ; % 获得评分矩阵
% [rate_bili res2] = DiscretRate(rate) ;
% % avguserscore2 = avgUserScore(rate) ;% 获得用户连续值平均评分
avguserscore = avgUserScore(rate) ; % 获得用户离散值平均评分
u2usim = u2uSim_pearson(rate, avguserscore) ; % 用户与用户之间的相似度矩阵(pearson方法)
% % u2usim = u2uSim_easy(rate) ; % 用户与用户之间的相似度矩阵(简单方法)
buy_number = buyNumber(training_data,user_id,3) ; % 预测用户购买的商品个数
final_rate = CFR(rate, u2usim, avguserscore) ; % 用户最终的评分
[f1 guess_result hb pb bb right] = week4(final_rate, user_id, brand_id, buy_number, user_buy) ; % 预测的结果
% print_result(final_rate, user_id, brand_id, buy_number) ; % 打印结果

