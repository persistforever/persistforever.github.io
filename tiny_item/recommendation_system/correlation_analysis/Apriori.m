function [itemSet new_itemSet] = Apriori(buy_item, brand_id)
itemSet = zeros(9531,2) ;
itemSet(:,1) = brand_id ;
[m,n] = size(buy_item) ;
for i=1:m
    for j=3:buy_item(i,2)+2 
        itemSet(find(brand_id==buy_item(i,j)),2) = itemSet(find(brand_id==buy_item(i,j)),2) + 1 ;
    end
end
itemSet( find(itemSet(:,2)<2) , :) = [] ;
new_itemSet = itemSet ;
new_itemSet = combntns(itemSet(:,1),2) ;
[row,temp] = size(new_itemSet) ;
for i=1:row
    i
    new_itemSet(i,3) = CalSupport(buy_item, new_itemSet(i,1:2)) ;
end
end