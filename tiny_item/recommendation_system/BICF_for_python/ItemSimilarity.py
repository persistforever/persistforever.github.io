import math as mt

"""
ItemSimilarity class
calculate different similarity between items
1. setting distance
2. euclidean distance
3. pearson distance
"""
class ItemSimilarity :
    sim_type = 1 # type of similarity

    def __init__(self, sim_type) :
    #   the construction function of ItemSimilarity
    #   input : sim_type - type of similarity
        self.sim_type = sim_type
    # --- end of __init__ ---

    def cal_sim(self, UL, IL) :
    #   calculate similarity use sim_type
    #   input : UL - user list
    #           IL - item list
        if self.sim_type == 1 :
            return self.setting(UL, IL)
        elif self.sim_type == 2 :
            return self.euclidean(trainData, numUser)
        elif self.sim_type == 3 :
            return self.pearson(trainData, numUser)
    # --- end of cal_sim ---

    def setting(self, UL, IL) :
    #   calculate setting similarity between items
    #   input : UL - user list
    #           IL - item list
        item_sim = dict()
        item_sum = dict()
        for i in IL.keys() :
            item_sim[i] = dict()
            item_sum[i] = 0
            for j in IL.keys() :
                item_sim[i][j] = 0
        for user in UL.keys() :
            for i in UL[user] :
                item_sum[i] += 1
                for j in UL[user] :
                    item_sim[i][j] += 1
        for i in IL.keys() :
            for j in IL.keys() :
                item_sim[i][j] = 1.0*item_sim[i][j]/(mt.sqrt(item_sum[i]*item_sum[j]))
        return item_sim
    # --- end of setting ---

    def euclidean(self, trainData, numUser) :
    #   calculate euclidean similarty between item
    #   input : trainData - [user item rating] as transctions
    #   ouput : user_sim - numUser*numUser matrix to show similarty between uses
        IL = item_inverted_list(trainData)
        user_sim = []
        for i in range(numUser) :
            user_sim.append([0]*numUser)
        for item in IL.keys() :
            for i in range(len(IL[item])) :
                for j in range(i+1, len(IL[item])) :
                    user_sim[IL[item][i][0]-1][IL[item][j][0]-1] += (IL[item][i][1]-IL[item][j][1])**2
        for i in range(numUser) :
            for j in range(numUser) :
                if user_sim[i][j] != 0 :
                    user_sim[i][j] = 1.0/user_sim[i][j]
        return user_sim
    # --- end of euclidean_similarity function ---

    def pearson(self, trainData, numUser) :
    #   calculate pearson similarty between users
    #   input : trainData - [user item rating] as transctions
    #   ouput : user_sim - numUser*numUser matrix to show similarty between uses
        # UL = user_list(trainData)
        IL = item_inverted_list(trainData)
        user_sim = []
        sum_a = []
        sum_b = []
        for i in range(numUser) :
            user_sim.append([0]*numUser)
            sum_a.append([0]*numUser)
            sum_b.append([0]*numUser)
        
        for item in IL.keys() :
            for i in range(len(IL[item])) :
                for j in range(i+1, len(IL[item])) :
                    user_sim[IL[item][i][0]-1][IL[item][j][0]-1] += (IL[item][i][1]*IL[item][j][1])
                    sum_a[IL[item][i][0]-1][IL[item][j][0]-1] += IL[item][i][1]*IL[item][i][1]
                    sum_b[IL[item][i][0]-1][IL[item][j][0]-1] += IL[item][j][1]*IL[item][j][1]
        for i in range(numUser) :
            for j in range(numUser) :
                if user_sim[i][j] != 0 :
                    user_sim[i][j] = 1.0*user_sim[i][j]/(mt.sqrt(sum_a[i][j])*mt.sqrt(sum_b[i][j]))
                if user_sim[i][j] >= 1 :
                    user_sim[i][j] = 0
        return user_sim
    # --- end of euclidean_similarity function ---
