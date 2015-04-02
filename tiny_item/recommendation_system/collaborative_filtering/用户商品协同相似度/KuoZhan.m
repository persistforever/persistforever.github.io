function res_rate = KuoZhan(new_brand_id,brand_id, real_rate)
%½«½µÎ¬µÄ¾ØÕóÀ©Õ¹
[m n] = size(real_rate);
[mm nn] = size(brand_id);
res_rate = zeros(m,mm);
for j=1:n
    index = find(brand_id == new_brand_id(j));
    res_rate(:,index) = real_rate(:,j);
end

end

