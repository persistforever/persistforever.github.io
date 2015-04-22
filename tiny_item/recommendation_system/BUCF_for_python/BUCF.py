import string as st
import numpy as np
import ImportData as im
import UserSimilarity as usm
import Recommendation as rm
import Performance as pf
import Ploting as pl

"""
BUCF class
basic user collaborative flitering
"""
class BUCF() :
    trainData = [] # train data set
    testData = [] # test data set
    recData = [] # recommendation data set
    UL = dict() # user list
    IL = dict() # item list
    user_sim = dict() # user similarity
    user_neigh = dict() # k nearest neighbours user

    def __init__(self, train_name, test_name) :
    #   the construction function of BUCF
    #   input : train_name - name of training file
    #           test_name - name of test file
        print "construct BUCF ..."
        imp = im.ImportData(train_name, test_name)
        [self.trainData, self.testData] = imp.train_and_test()
    # --- end of __init__ ---

    def user_list(self) :
    #   create user-item list {user : [item, item, ...], ...}
        print "create user list ..."
        for ts in self.trainData :
            if ts[0] not in self.UL.keys() :
                self.UL[ts[0]] = []
            self.UL[ts[0]].append(ts[1])
    # --- end of user_list ---

    def item_list(self) :
    #   create item-user list {item : [user, user, ...], ...}
        print "create item list ..."
        for ts in self.trainData :
            if ts[1] not in self.IL.keys() :
                self.IL[ts[1]] = []
            self.IL[ts[1]].append(ts[0])
    # --- end of item_list ---

    def cal_similarity(self) :
    #   calculate similarity between users user_sim {user:{user:sim, user:sim, ...}, ...}
        print "create user_sim ..."
        usim = usm.UserSimilarity(1)
        self.user_sim = usim.cal_sim(self.UL, self.IL)
    # --- end of cal_similarity ---

    def kNN_user(self, k) :
    #   find k neareast neightbour users {user: [(user, sim), ...], ...}
    #   input : k - number of neighbours
        print "create user_neigh ..."
        for i in self.user_sim.keys() :
            neighbour = []
            for j in self.user_sim.keys() :
                if self.user_sim[i][j] > 0 :
                    neighbour.append((j, self.user_sim[i][j]))
            self.user_neigh[i] = sorted(neighbour, cmp=lambda x,y:cmp(x[1], y[1]), reverse=True)[1:k+1]
    # --- end of kNN_user ---

    def recommend(self) :
    #   recommend 10 items for each user [[user item], [user, item], ...]
        print "create recData ..."
        rcm = rm.Recommendation(1)
        self.recData = rcm.recommend(self.user_neigh, self.UL, self.IL)
    # --- end of recommend ---
    
    def test_performance(self) :
    #   test performance of recommendation algorithm
        print "create performance ..."
        p = pf.Performance()
        [rc, prec, cover, pop] = p.performance(self.trainData, self.recData, self.testData)

        print 'recall\t', rc
        print 'precision\t', prec
        print 'coverage\t', cover
        print 'popularity\t', pop
        return [rc, prec, cover, pop]
    # --- end of test_performance ---
    
# --- end of BICF class ---


"""main start"""
if __name__ == '__main__':
    """
    bucf = BUCF('data/ua.base', 'data/ua.test')
    bucf.user_list()
    bucf.item_list()
    bucf.cal_similarity()
    perform = []
    K = [5, 10, 20, 40, 80, 160]
    for i in K :
        print "knn number is ", i
        bucf.kNN_user(i)
        bucf.recommend()
        perform.append(bucf.test_performance())
    imp = im.ImportData('', '')
    imp.write_data(perform, 'result/perform.txt')
    """
    K = [5, 10, 20, 40, 80, 160]
    imp = im.ImportData('', '')
    perform = imp.import_data('result/perform.txt')
    plt = pl.Ploting(perform, K)
    plt.ploting()
"""main end"""

