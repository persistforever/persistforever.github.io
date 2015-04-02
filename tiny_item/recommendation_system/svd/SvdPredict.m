function [f1 hb] = SvdPredict(rate, user_id, brand_id, buy_number, user_buy, k)
[u, s, v] = svds(rate,k);
final_rate = zeros(884,9531) ;
for i=1:884
    i
    for j=1:9531
        final_rate(i,j) = 1/sqrt(sum((u(i,:)-v(j,:)).^2)) ;
    end
end
[f1, hb, pb, bb, p, r, guess_result] = week4(final_rate, user_id, brand_id, buy_number, user_buy) ;
end