"""
different recommendation method
1. outline method(as many as testData)
2. random method
3. most popular method
"""

def user_rating_item(UL, user_neigh, user, item) :
#   user 'user' rate to item 'item' under trainData and user_neigh
#   input : UL - user_item list
#           user_neigh - (user*k) matrix as each user for k neighbours
#           user - sequence of user
#           item - sequence of item
#   output : rate - rate of user rating item

    neigh = user_neigh[user]
    neigh_rate = [0]*len(neigh)
    neigh_sim = [tmp[1] for tmp in user_neigh[user]]
    sum_rate = 0
    dist = 0
    for i in range(len(neigh)) :
        if item in [tmp[0] for tmp in UL[neigh[i][0]]] :
            neigh_rate[i] = [tmp[1] for tmp in UL[neigh[i][0]] if tmp[0] == item][0]
            sum_rate += neigh_sim[i] * neigh_rate[i]
            dist += neigh_sim[i]
    if dist == 0 :
        rate = 0
    else :
        rate = 1.0*sum_rate/dist

    return rate
# --- end of user_rating_item ---

def outline_recommend(UL, user_neigh, numUser, numItem, testData) :
#   recommand as many as testData
#   input : UL - user_item list
#           user_neigh - (user*k) matrix as each user for k neighbours
#           numUser - number of user
#           numItem - number of item
#   output : recData - dataSet that recommendation provides

    recData = []
    for i in range(len(testData)) :
        user = testData[i][0]
        item = testData[i][1]
        recData.append([user, item, round(user_rating_item(UL, user_neigh, user, item))])
        
    return recData
# --- end of outline_recommend ---
