
%     grade_vector = [0.5 1 1 1] ;
%     [month1 month2 month3] = PreTreat(training_data) ;
%     pre_rate_1 = GiveGrade(month1,user_id,brand_id,grade_vector) ;
%     pre_rate_2 = GiveGrade(month2,user_id,brand_id,grade_vector) ;
%     pre_rate_3 = GiveGrade(month3,user_id,brand_id,grade_vector) ;
%     u2usim1 = CalSimilarity(pre_rate_1, pre_rate_2) ;
%     u2usim2 = CalSimilarity(pre_rate_2, pre_rate_3) ;
%     u2usim3 = CalSimilarity(pre_rate_1, pre_rate_3) ;
%     u2usim4 = u2usim1+u2usim2+u2usim3*0.5 ; % 计算用户用户相似度
%     pre_rate = GiveGrade(data,user_id,brand_id,1,90,grade_vector) ;
%     post_rate = GiveGrade(data,user_id,brand_id,91,120,grade_vector) ;
%     u2usim4 = CalSimilarity(pre_rate, post_rate) ;
%     u2usim5 = u2usim3*0.4+u2usim4*0.6 ; % 计算用户用户相似度
%     rate = rateMatrix(data, brand_id, user_id,4,[1 9 3 5]) ;
    % avguserscore = avgUserScore(rate) ;
    % u2usim = u2uSim_pearson(rate, avguserscore) ;
    % u2usim = zeros(884,884)+1 ;
%     buy_number = buyNumber(data,user_id,4);
% for k=5:5:100
%     [rate_bili rate_temp] = DiscretRate(new_rate) ;
%     [u,s,v] = svd(rate_bili) ;
%     b2bsim = SvdU2uSim(v,40) ;
%     scale_factor = 0.5;
%     sim_count=20 ;
    %  for i=1:11
%      i
%     scale_factor = (i-1)/10 ;
%     real_rate = update_rate(rate,u2usim,scale_factor,sim_count);
    [f1, hb, pb, bb, p, r, guess_result] = week4(real_rate, user_id, brand_id, buy_number, user_buy) ;
%     rmse(k/5) = f1;
%     hb_sum(k/5)=hb;
%  end
