"""
different method to calculate similarity between users
1. euclidean distance
"""
import math as mt

def item_inverted_list(trainData) :
#   create item-user list
#   input : trainData - [user item rating] as transctions
#   output : IL - item-user list
    IL = dict()
    for ts in trainData :
        if ts[1] not in IL.keys() :
            IL[ts[1]] = []
        IL[ts[1]].append([ts[0], ts[2]])

    return IL
# --- end of item_inverted_list ---

def euclidean_similarity(trainData, numUser) :
#   calculate euclidean similarty between users
#   input : trainData - [user item rating] as transctions
#   ouput : user_sim - numUser*numUser matrix to show similarty between uses
    # UL = user_list(trainData)
    IL = item_inverted_list(trainData)
    user_sim = [[0]*numUser]*numUser
    
    for item in IL.keys() :
        for i in range(len(IL[item])) :
            for j in [tmp for tmp in range(len(IL[item])) if tmp>i] :
                user_sim[IL[item][i][0]-1][IL[item][j][0]-1] += (IL[item][i][1]-IL[item][j][1])**2

    return user_sim
# --- end of euclidean_similarity function ---

def pearson_similarity(trainData, numUser) :
#   calculate pearson similarty between users
#   input : trainData - [user item rating] as transctions
#   ouput : user_sim - numUser*numUser matrix to show similarty between uses
    # UL = user_list(trainData)
    IL = item_inverted_list(trainData)
    user_sim = [[0]*numUser]*numUser
    sum_a = [[0]*numUser]*numUser
    sum_b = [[0]*numUser]*numUser
    
    for item in IL.keys() :
        for i in range(len(IL[item])) :
            for j in [tmp for tmp in range(len(IL[item])) if tmp>i] :
                user_sim[IL[item][i][0]-1][IL[item][j][0]-1] += (IL[item][i][1]*IL[item][j][1])
                sum_a[IL[item][i][0]-1][IL[item][j][0]-1] += IL[item][i][1]**2
                sum_b[IL[item][i][0]-1][IL[item][j][0]-1]+= IL[item][j][1]**2
    for i in range(numUser) :
        for j in range(numUser) :
            if user_sim[i][j] != 0 :
                user_sim[i][j] = 1.0*user_sim[i][j]/mt.sqrt(sum_a[i][j])*mt.sqrt(sum_b[i][j])

    return user_sim
# --- end of euclidean_similarity function ---
