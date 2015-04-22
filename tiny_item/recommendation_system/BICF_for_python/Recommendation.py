import random as rd

"""
Recommendation class
different recommendation methods
1. outline method(as many as testData)
2. random method
"""
class Recommendation :
    """attributes"""
    rec_type = 1 # type of recommendation

    """functions"""
    def __init__(self, rec_type) :
    #   the construction function of Recommendation
        self.rec_type = rec_type
    # --- end of __init__ ---
    
    def recommend(self, item_neigh, UL, IL) :
    #   recommend with rec_type
    #   input : UL - user-item list
    #           IL - item-user list
    #           item_neigh - each item for k neighbours
    #   output : recData - dataSet that recommendation provides
        if self.rec_type == 1 :
            return self.max_recommend(item_neigh, UL, IL)
    # --- end of recommend ---
    
    def max_recommend(self, item_neigh, UL, IL) :
    #   recommand 10 max rated items
    #   input : UL - user-item list
    #           IL - item-user list
    #           item_neigh - each item for k neighbours
    #   output : recData - dataSet that recommendation provides
        numRec = 10
        recData = []
        for user in UL.keys() :
            interest = dict()
            for j in IL.keys() :
                if j not in UL[user] :
                    interest[j] = 0
            for i in UL[user] : 
                for j in range(len(item_neigh[i])) :
                    if item_neigh[i][j][0] not in UL[user] :
                        interest[item_neigh[i][j][0]] += item_neigh[i][j][1]
            rec = []
            for i in interest.keys() :
                rec.append((i, interest[i]))
            tmp = sorted(rec, cmp=lambda x,y:cmp(x[1], y[1]), reverse=True)[0:numRec]
            for i in range(numRec) :
                if tmp[i][1] > 0 :
                    recData.append([user, tmp[i][0]])
        return recData
    # --- end of all_recommend ---
    
# --- end of Recommendation class ---
