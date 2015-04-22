import string as st
import numpy as np
import ImportData as im
import ItemSimilarity as ism
import Recommendation as rm
import Performance as pf
import Ploting as pl

"""
BICF class
basic item collaborative flitering
"""
class BICF() :
    trainData = [] # train data set
    testData = [] # test data set
    recData = [] # recommendation data set
    UL = dict() # user list
    IL = dict() # item list
    item_sim = dict() # item similarity
    item_neigh = dict() # k nearest neighbours item

    def __init__(self, train_name, test_name) :
    #   the construction function of BICF
    #   input : train_name - name of training file
    #           test_name - name of test file
        print "construct BICF ..."
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

    def cal_similarity(self, sim_type) :
    #   calculate similarity between items item_sim {item:{item:sim, item:sim, ...}, ...}
        print "create item_sim ..."
        isim = ism.ItemSimilarity(sim_type)
        self.item_sim = isim.cal_sim(self.UL, self.IL)
    # --- end of cal_similarity ---

    def kNN_item(self, k) :
    #   find k neareast neightbour items {item: [(item, sim), ...], ...}
    #   input : k - number of neighbours
        print "create item_neigh ..."
        for i in self.item_sim.keys() :
            neighbour = []
            for j in self.item_sim.keys() :
                if self.item_sim[i][j] > 0 :
                    neighbour.append((j, self.item_sim[i][j]))
            self.item_neigh[i] = sorted(neighbour, cmp=lambda x,y:cmp(x[1], y[1]), reverse=True)[1:k+1]
    # --- end of kNN_user ---

    def recommend(self) :
    #   recommend 10 items for each user [[user item], [user, item], ...]
        print "create recData ..."
        rcm = rm.Recommendation(1)
        self.recData = rcm.recommend(self.item_neigh, self.UL, self.IL)
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
    bicf = BICF('data/ua.base', 'data/ua.test')
    bicf.user_list()
    bicf.item_list()
    bicf.cal_similarity(2)
    perform = []
    K = [5, 10, 20, 40, 80, 160]
    for i in K :
        print "knn number is ", i
        bicf.kNN_item(i)
        bicf.recommend()
        perform.append(bicf.test_performance())
    imp = im.ImportData('', '')
    imp.write_data(perform, 'result/perform_IUF.txt')
    """
    K = [5, 10, 20, 40, 80, 160]
    imp = im.ImportData('', '')
    perform = imp.import_data('result/perform.txt')
    plt = pl.Ploting(perform, K)
    plt.ploting()
    
"""main end"""

