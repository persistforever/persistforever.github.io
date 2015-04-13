"""
main system of recommandation
"""
import string as st
import numpy as np
import matplotlib
import matplotlib.pyplot as plt
import import_data as im
import evaluation_index as ei
import user_similarity as us
import recommendation_method as rm

def user_list(trainData) :
#   create user-item list
#   input : trainData - [user item rating] as transctions
#   output : UL - user list
    UL = dict()
    for ts in trainData :
        if ts[0] not in UL.keys() :
            UL[ts[0]] = []
        UL[ts[0]].append([ts[1], ts[2]])

    return UL
# --- end of user_list ---

def kNN_user(user_sim, k) :
#   find k neareast users
#   input : user_sim - user similarity
#           trainData - train data examples
#           k - number of neighbours
#   output : user_neigh - (user*k) matrix as each user for k neighbours

    user_neigh = dict()
    for i in range(len(user_sim)) :
        neighbour = dict()
        for j in range(len(user_sim)) :
            neighbour[j+1] = user_sim[i][j]
        neighbour = sorted(neighbour.iteritems(), key=lambda d:d[1], reverse = False)
        tmp = []
        for j in range(k) :
            tmp.append([neighbour[j+1][0], neighbour[j+1][1]])
        user_neigh[i+1] = tmp

    return user_neigh
# --- end of kNN_user ---

def test_performance(trainData, recData, testData) :
#   test performance of recommendation algorithm
#   input : trainData - train data examples
#           recData - recommended data examples
#           testData - test data examples
#   output : recall_rate - rate of recall
#            pre_rate - rate of precision
#            coverage - coverage of precision
#            popularity - popularity of precision

    trainData = user_list(trainData)
    recData = user_list(recData)
    testData = user_list(testData)
    recall_rate = ei.recall(recData, testData)
    pre_rate = ei.precision(recData, testData)
    coverage = ei.coverage(trainData, recData)
    popularity = ei.popularity(trainData, recData)

    print 'recall\t', recall_rate
    print 'precision\t', pre_rate
    print 'coverage\t', coverage
    print 'popularity\t', popularity
    return [recall_rate, pre_rate, coverage, popularity]
# --- end of test_performance ---

def plot_data(performance, K) :
    # plot data in dataSet
    # output : performance - correspond to y
    #          K - correspond to x

    fig = plt.figure()
    ax = fig.add_subplot(111)
    p1 = ax.plot(K, [tmp[3] for tmp in performance][0:5], 'mo--')
    bx = fig.add_subplot(111)
    p3 = bx.plot(K, [tmp[3] for tmp in performance][5:10], 'ms--')
    plt.title('eurclidean vs pearson for users-CF on popularity', fontsize=16)
    plt.xlabel('number of nearest neighbour')
    plt.ylabel('performance')
    plt.legend((p1[0], p3[0]), ('eur-d Pop', 'pears-d Pop'), 'best', numpoints=1)
    plt.show()
    
    return 
# --- end of plot_data ---

def main() :
#   main entry of programm

    [trainData, testData] = im.train_and_test('data/ua.base', 'data/ua.test')
    numUser = len(set([tmp[0] for tmp in trainData]))
    numItem = len(set([tmp[1] for tmp in trainData]))
    UL = user_list(trainData)
    K = [10, 20, 40, 80, 160]
    performance = []

    # use euclidean similarity
    user_sim = us.euclidean_similarity(trainData, numUser)
    print 'euclidean user_sim calculation finished!'
    for i in K :
        print 'K = ', i
        user_neigh = kNN_user(user_sim, i)
        print 'user_neigh calculation finished!'
        recData = rm.all_recommend(UL, user_neigh, numUser, numItem, testData)
        print 'recommendation finished!'
        performance.append(test_performance(trainData, recData, testData))
    # use pearson similarity
    user_sim = us.pearson_similarity(trainData, numUser)
    print 'pearson user_sim calculation finished!'
    for i in K :
        print 'K = ', i
        user_neigh = kNN_user(user_sim, i)
        print 'user_neigh calculation finished!'
        recData = rm.all_recommend(UL, user_neigh, numUser, numItem, testData)
        print 'recommendation finished!'
        performance.append(test_performance(trainData, recData, testData))
    im.write_data(performance, 'result/perfor.txt')
    # plot_data(performance, K)
# --- end of main ---

main()
